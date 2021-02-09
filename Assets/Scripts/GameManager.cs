using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    public GameUI gameUI;
    public OpponentType opponentType;

    [SerializeField]
    int minimaxDepth = 5;

    GameState gameState;
    GameBoard gameBoard;
    Evaluator evaluator = new Evaluator();
    AIPlayer aiPlayer;

    public char currentPlayer;

    public char humanPlayer = 'o';
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
                // StartCoroutine(AiThinking());
                AiTakeTurn();

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
                    AIMinimax minimax = new AIMinimax();
                    minimax.MaxDepth = minimaxDepth;
                    aiPlayer = minimax;
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
        if (isEndGame)
        {
            return;
        }

        if (opponentType != OpponentType.HUMAN && currentPlayer != humanPlayer) // check if playing with an AI
        {
            return;
        }

        if(!gameState.IsValidMove(newPlayerPos, currentPlayer))
        {
            return;
        }

        //update the GameState
        if (gameState.MakeMove(newPlayerPos, currentPlayer)) //Returns true if legal an places move on board
        {
            Debug.Log("Tile clicked at: " + newPlayerPos.x + "," + newPlayerPos.y + " -> " + currentPlayer);
            //NextTurn();

            //update the GameBoard

            char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
            gameBoard.UpdateBoard(gameState, nextPlayer);
            gameUI.UpdateUI(gameState, nextPlayer);
            Debug.Log(gameState.ToString());
            currentPlayer = nextPlayer;

            // Check if the game is over
            if (gameState.IsGameOver())
            {
                Debug.Log("isGameOver == True");
                GameOver();
                return;
            }
        }
        
    }

    void NextTurn()
    {

        //// Check if the game is over
        //if (gameState.IsGameOver())
        //{
        //    Debug.Log("isGameOver == True");
        //    GameOver();
        //    return;
        //}

        ////update the GameBoard
        char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
        //gameBoard.UpdateBoard(gameState, nextPlayer);
        //gameUI.UpdateUI(gameState, nextPlayer);
        //Debug.Log(gameState.ToString());
        


        currentPlayer = nextPlayer;
        Debug.Log("next player: " + nextPlayer);
        if (currentPlayer != humanPlayer && opponentType != OpponentType.HUMAN)   //if its the AI turn
        {
            AiTakeTurn();
            //StartCoroutine(AiThinking());
        }
    }

    void AiTakeTurn()
    {
        //Debug.Log("AI Player: " + currentPlayer);
        Vector2Int bestMove = aiPlayer.GetMove(gameState, currentPlayer);
        //Debug.Log("AI choose -> " + bestMove.x + " " + bestMove.y);
        //List<GameState> possibleStates = gameState.GetNextPossibleState(currentPlayer);
        //Debug.Log(possibleStates.Count);

        //if minimax doesn't work yet, we will place the symbol in next available spot.
        if (bestMove == null)
        {
            Debug.LogError("ERROR: best move is null.");
        }

        if(gameState.MakeMove(bestMove, currentPlayer))
        {
            Debug.Log("AI moved -> " + bestMove.x + " " + bestMove.y);

            //update the GameBoard

            char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
            gameBoard.UpdateBoard(gameState, nextPlayer);
            gameUI.UpdateUI(gameState, nextPlayer);
            Debug.Log(gameState.ToString());
            currentPlayer = nextPlayer;

            // Check if the game is over
            if (gameState.IsGameOver())
            {
                Debug.Log("isGameOver == True");
                GameOver();
                return;
            }
           //NextTurn();
        }
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

    //IEnumerator AiThinking()
    //{
    //    //aiThinkingUI.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
    //    AiTakeTurn();
    //    //yield return null;
        
    //    //aiThinkingUI.SetActive(false);
    //}
}

public enum OpponentType
{
    HUMAN,
    DUMB_AI,
    MINIMAX_AI,
    NEURAL_NETWORK_AI
}


