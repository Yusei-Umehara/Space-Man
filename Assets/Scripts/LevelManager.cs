using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;  // Singleton

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;

    // public int partsOfLevel;

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock() // Problema, Aqui esta el error: 
    {
        int randomu = Random.Range(0, allTheLevelBlocks.Count); 
        
        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;
        
        if(currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);   // o aqui?
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomu] );
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position; 
        }

        block.transform.SetParent(this.transform, false);
        
        Vector3 correction = new Vector3(spawnPosition.x-block.startPoint.position.x, 
                                         spawnPosition.y-block.startPoint.position.y, 
                                         0);
        block.transform.position = correction;
        
        currentLevelBlocks.Add(block);

    }

    public void RemoveLevelBlock()
    {

    }

    public void RemoveAllLevelBlocks()
    {

    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }



}
