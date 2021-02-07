using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    public GameUI gameUI;
    public OpponentType opponentType;

    GameState gameState;
    GameBoard gameBoard;
    
    public char currentPlayer;

    private char humanPlayer = 'x';
    //private bool isHotseat = true;


    void Awake()
    {
        gameState = new GameState(width, height);
        gameBoard = GetComponent<GameBoard>();
        gameBoard.GenBoard(width, height);

        //humanPlayer = Random.Range(0f, 1f) > 0.5f ? 'x' : 'o';  // Randomly assigns human player a turn

        // X always is player1
        currentPlayer = 'x';
                
        Debug.Log(gameState.ToString());

        if (opponentType != OpponentType.HUMAN)
        {
            // InitializeAIPlayer()

            if (humanPlayer == 'o')     // checks if the AI is playing first
            {
                //StartCoroutine(AiTurn());
                
            }
        }
    }

    void Start()
    {
        gameUI.UpdateUI(gameState, currentPlayer);
        gameBoard.UpdateBoard(gameState, currentPlayer);
        //HumanTurn();
    }

    //void Player1()
    //{
    //    if (currentPlayer = humanPlayer)
    //    {

    //    }
    //}

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
        if (opponentType != OpponentType.HUMAN) // check if playing with an AI
        {
            if (currentPlayer != humanPlayer)   //check if its not the players turn
            {
                return;
            }
        }

        //if (!gameState.GetPossibleMoves(currentPlayer).Contains(newPlayerPos))
        //{
        //    return;
        //}

        //update the GameState
        if(gameState.MakeMove(newPlayerPos, currentPlayer)) //Returns true if legal an places move on board
        {
            Debug.Log("Tile clicked at: " + newPlayerPos.x + "," + newPlayerPos.y + " -> " + currentPlayer);
            //update the GameBoard
            char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
            gameBoard.UpdateBoard(gameState, nextPlayer);
            gameUI.UpdateUI(gameState, nextPlayer);
            Debug.Log(gameState.ToString());
            currentPlayer = nextPlayer;
        }

        

        


    }

    void HumanTurn()
    {
       //gameBoard.UpdateBoard(gameState, currentPlayer);

        //List<Vector2Int> possibleMoves = gameState.GetPossibleMoves(currentPlayer);

        //string pMovesString = "Possible moves are: ";
        //foreach (Vector2Int move in possibleMoves)
        //{
        //    pMovesString += "(" + move.x + "," + move.y + ")" + "\n";
        //}
        //Debug.Log("Player " + currentPlayer + ": can move to " + pMovesString);


    }
}

public enum OpponentType
{
    HUMAN,
    DUMB_AI,
    MINIMAX_AI,
    NEURAL_NETWORK_AI
}


