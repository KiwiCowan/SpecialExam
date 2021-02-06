using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    GameState gameState;
    GameBoard gameBoard;

    public char currentPlayer;

   
    void Awake()
    {
        gameState = new GameState(width, height);
        gameBoard = GetComponent<GameBoard>();
        gameBoard.GenBoard(width, height);
        currentPlayer = 'x';
        Debug.Log(gameState.ToString());
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        Tile.OnClicked += OnTileClicked;
    }

    private void OnDisable()
    {
        Tile.OnClicked -= OnTileClicked;
    }


    void OnTileClicked(Vector2Int newPlayerPos)
    {
        Debug.Log("Tile clicked at: " + newPlayerPos.x + "," + newPlayerPos.y);

        //update the GameState
        // gameState.GetPossibleMoves(currentPlayer);
        HumanTurn();
        //update the GameBoard
    }

    void HumanTurn()
    {
        List<Vector2Int> possibleMoves = gameState.GetPossibleMoves(currentPlayer);
        string pMovesString = "Possible moves are: ";
        foreach (Vector2Int move in possibleMoves)
        {
            pMovesString += "(" + move.x + "," + move.y + ")" + "\n";
        }
        Debug.Log("Player " + currentPlayer + ": can move to " + pMovesString);
    }
}
