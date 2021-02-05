using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject prefab;

    GameState gameState;
    Tile[] tiles;

    public GameBoard(GameState _gameState)
    {
        this.gameState = _gameState;
    }

    // Start is called before the first frame update
    void GenBoard(int width)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
