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
    Evaluator evaluator = new Evaluator();
    AIPlayer aiPlayer;

    public char currentPlayer;

    private char humanPlayer = 'x';
    private bool isEndGame = false;


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
            InitializeAIPlayer();

            if (humanPlayer == 'o')     // checks if the AI is playing first
            {
                StartCoroutine(AiThinking());

            }
        }
    }

    void Start()
    {
        gameUI.UpdateUI(gameState, currentPlayer);
        gameBoard.UpdateBoard(gameState, currentPlayer);
        //HumanTurn();
    }

    void InitializeAIPlayer()
    {
        switch (opponentType)
        {
            case OpponentType.DUMB_AI:
                {
                    aiPlayer = new AIDumb();
                    break;
                }                
            case OpponentType.MINIMAX_AI:
                {
                    break;
                }
            case OpponentType.NEURAL_NETWORK_AI:
                {
                    break;
                }
        }
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
        if (!isEndGame)
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
            if (gameState.MakeMove(newPlayerPos, currentPlayer)) //Returns true if legal an places move on board
            {
                Debug.Log("Tile clicked at: " + newPlayerPos.x + "," + newPlayerPos.y + " -> " + currentPlayer);
                NextTurn();
                
                ////update the GameBoard
                
                //char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
                //gameBoard.UpdateBoard(gameState, nextPlayer);
                //gameUI.UpdateUI(gameState, nextPlayer);
                //Debug.Log(gameState.ToString());
                //currentPlayer = nextPlayer;

                //// Check if the game is over
                //if (gameState.IsGameOver())
                //{
                //    Debug.Log("isGameOver == True");
                //    GameOver();
                //    return;
                //}
            }
        }
    }

    void NextTurn()
    {
        //update the GameBoard
        char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
        gameBoard.UpdateBoard(gameState, nextPlayer);
        gameUI.UpdateUI(gameState, nextPlayer);
        Debug.Log(gameState.ToString());
        

        // Check if the game is over
        if (gameState.IsGameOver())
        {
            Debug.Log("isGameOver == True");
            GameOver();
            return;
        }

        currentPlayer = nextPlayer;
        if (currentPlayer != humanPlayer)   //if its the AI turn
        {
            StartCoroutine(AiThinking());
        }
    }

    void AiTakeTurn()
    {
        Vector2Int bestMove = aiPlayer.GetMove(gameState, currentPlayer);

        //if minimax doesn't work yet, we will place the symbol in next available spot.
        if (bestMove == null)
        {
            Debug.Log("ERROR: best move is null.");
        }

        gameState.MakeMove(bestMove, currentPlayer);

        
    }

    public void GameOver()
    {
        isEndGame = true;
        string outcomeString = "";

        // find who won, loss or drawed
        char outcomeChar = gameState.GetGameOutcome();
        if (outcomeChar == 'x')
        {
            outcomeString = "X wins";
        }
        else if (outcomeChar == 'o')
        {
            outcomeString = "O wins";
        }
        else if (outcomeChar == 'd')
        {
            outcomeString = "Draw";
        }

        // show victory screen
        gameUI.GameOverUI(outcomeString);
    }

    IEnumerator AiThinking()
    {
        //aiThinkingUI.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AiTakeTurn();
        yield return null;
        NextTurn();
        //aiThinkingUI.SetActive(false);
    }
}

public enum OpponentType
{
    HUMAN,
    DUMB_AI,
    MINIMAX_AI,
    NEURAL_NETWORK_AI
}


