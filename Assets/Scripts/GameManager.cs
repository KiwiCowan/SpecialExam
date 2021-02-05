using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    GameState gameState;
    GameBoard gameBoard;

   
    void Awake()
    {
        gameState = new GameState(width, height);
        fortiles = new Tile[,]
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
