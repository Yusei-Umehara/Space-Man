using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variables del movimiento del personaje:
    public float jumpForce = 6f;
    public float runningSpeed = 4f;
    //escribir private y nada es igual se sobrentiende
    private Rigidbody2D rb;    //rigidBody o playerRigidBody

    // Movimiento del personaje Manual
    

    Animator animator;
    // Las CONSTANTES se declaran en mayusculas con _ guion bajo asi: CONSTANTE_EJEMPLO
    private const string STATE_ALIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";


    public LayerMask groundMask;

    Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
       /* animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true); */

        startPosition = this.transform.position; 
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        Invoke("RestartPosition", 0.2f);
        
    }

    void RestartPosition()
    {

        this.transform.position = startPosition;
        this.rb.velocity = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {   //Mouse buttons = izquierdo = 0 , derecho = 1, rueda = 2
        
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        
        /* if (Input.GetButtonDown("Jump"))
        {
            Jump();
        } */

        //Inicializador de animacion
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(0);
        }

        Debug.DrawRay(this.transform.position, Vector2.down * 1.0f, Color.red);
    }

    private void FixedUpdate()
    {
        // Movimiento de fuerza constante:
        /* if (rb.velocity.x < runningSpeed )
        {
            rb.velocity = new Vector2(runningSpeed, //x
                                      rb.velocity.y //y
                                      );
        } */

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            move();
            animator.enabled = true;
        }
        else if(GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            //move();
            animator.enabled = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.enabled = false;
            // rb.Sleep();  // Usado para detener la animacion o el rigid body.
        }


    }
    

    private void move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rb.velocity.y);
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }
    

    void Jump()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (IsTouchingTheGround())
            { //Time.deltaTime
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    bool IsTouchingTheGround()  // Raycaster Function
    {
        if (Physics2D.Raycast(this.transform.position, 
                              Vector2.down, 
                              1.0f, 
                              groundMask))
        {
            // TODO: programar logica de contacto con el suelo. 
            //animator.enabled = true;
            return true;
        }
        else
        {
            //animator.enabled = false; 
            // TODO: p logica de no contacto
            return false;
        }
    }
    

    public void Die()
    {
        this.animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }


}
