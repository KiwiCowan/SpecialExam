using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    GameState gameState;
    public GameBoard gameBoard;

    public char currentPlayer;

   
    void Awake()
    {
        gameState = new GameState(width, height);
        gameBoard.GenBoard(width, height);
        currentPlayer = 'x';
        Debug.Log(gameState.ToString());
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void OnTileClicked(Vector2Int newPlayerPos)
    {

    }
}
