using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;  //Singleton

    private PlayerController controller;

    void Awake()
    {
        if (sharedInstance == null)
        {
        sharedInstance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").
                                GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && currentGameState != GameState.inGame)  // con el && y la nueva condicion, que debe ser booleana tambien, ahora solo se puede reiniciar si se esta fuera del GameState.inGame y se presiona la tecla "T ".
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            BackToMenu();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);

    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);

    }

    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // TODO: Programar la logica del menu
        }
        else if(newGameState == GameState.inGame)
        {
            // TODO: Hay que preparar la escena para jugar
            controller.StartGame();
        }
        else if(newGameState == GameState.gameOver)
        {
            // TODO : Logica GO
        }

        this.currentGameState = newGameState;
    }

}
