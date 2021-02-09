using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width, height;
    public GameUI gameUI;
    public OpponentType opponentType;
    public GameSystem gameSystem;

    [SerializeField]
    int minimaxDepth = 5;

    GameState gameState;
    GameBoard gameBoard;
    Evaluator evaluator = new Evaluator();
    AIPlayer aiPlayer;

    int currentPlayer;  // 0 = none, 1 = x = player1, 2 = o = player2

    public int humanPlayerTurn;
    public int  aiPlayerTurn;

    private bool isHotseatGame = true;
    private bool isEndGame = false;


    void Awake()
    {
        gameState = new GameState(width, height);
        gameBoard = GetComponent<GameBoard>();

       // gameSystem = GameSystem.START;

       // gameBoard.GenBoard(width, height);

       //// humanPlayerTurn = Random.Range(0f, 1f) > 0.5f ? 1 : 2;  // Randomly assigns human player a turn

       // // X always is player1
       // currentPlayer = 0;

       // //Debug.Log(gameState.ToString());

        if (opponentType != OpponentType.HUMAN)
        {
            isHotseatGame = false;
            InitializeAIPlayer();

            //if (humanPlayerTurn == 2)     // checks if the AI is playing first
            //{
            //    // StartCoroutine(AiThinking());
            //    AiTakeTurn();

            //}
        }
        
    }



    void Start()
    {
        gameSystem = GameSystem.START;
        StartCoroutine(SetUp());

        //gameUI.UpdateUI(gameState, currentPlayer);
        //gameBoard.UpdateBoard(gameState, currentPlayer);
        //HumanTurn();
    }

    IEnumerator SetUp()    // assign each player a turn 
    {
        gameBoard.GenBoard(width, height);
        

        // // X always is player1
        currentPlayer = 0;

        Debug.Log("Starting GameState " +gameState);

        //if (isHotseatGame)  // if playing against AI
        //{
        //    humanPlayerTurn = 1;
            
        //}

        // humanPlayerTurn = Random.Range(0f, 1f) > 0.5f ? 1 : 2;  // Randomly assigns human player a turn
        // for now  humanPLayer will start
        humanPlayerTurn = 1;
        aiPlayerTurn = 2;

        yield return new WaitForSeconds(2f);

        
        gameSystem = GameSystem.PLAYER1TURN;
        Player1Turn();
        

        //gameBoard.UpdateBoard(gameState);

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


    void OnTileClicked(Vector2Int newTilePos)
    {

        if (gameSystem == GameSystem.GAMEOVER || gameSystem == GameSystem.START)
        {
            return;
        }

        if (opponentType != OpponentType.HUMAN && currentPlayer != humanPlayerTurn) // check if playing with an AI
        {
            return;
        }

        if (!gameState.IsValidMove(newTilePos - (currentPlayer == 1 ? gameState.P1Pos : gameState.P2Pos), currentPlayer))
        {
            return;
        }

        StartCoroutine(MakeMoveOnState(newTilePos- (currentPlayer == 1 ? gameState.P1Pos : gameState.P2Pos)));

        //update the GameState
        //if (gameState.MakeMove(newPlayerPos, currentPlayer)) //Returns true if legal an places move on board
        //{
        //    Debug.Log("Tile clicked at: " + newPlayerPos.x + "," + newPlayerPos.y + " -> " + currentPlayer);
        //    //NextTurn();

        //    //update the GameBoard

        //    char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
        //    gameBoard.UpdateBoard(gameState, nextPlayer);
        //    gameUI.UpdateUI(gameState, nextPlayer);
        //    Debug.Log(gameState.ToString());
        //    currentPlayer = nextPlayer;

        //    // Check if the game is over
        //    if (gameState.IsGameOver())
        //    {
        //        Debug.Log("isGameOver == True");
        //        GameOver();
        //        return;
        //    }
        //}
        
    }

    void Player1Turn()
    {
        currentPlayer = 1;
        gameState.CurrentPlayer = currentPlayer;

        gameUI.UpdateUI(gameState);

        if (!isHotseatGame)
        {
            if(currentPlayer == humanPlayerTurn)
            {
                
                gameBoard.UpdateBoard(gameState);
                return;
            }

            Vector2Int bestMove = aiPlayer.GetMove(gameState, 'x');//if minimax doesn't work yet, we will place the symbol in next available spot.
            if (bestMove == null)
            {
                Debug.LogError("ERROR: best move is null.");
            }

            Debug.Log("AI moved -> " + bestMove.x + " " + bestMove.y);
            StartCoroutine(MakeMoveOnState(bestMove));
        }


    }
    void Player2Turn()
    {
        currentPlayer = 2;
        gameState.CurrentPlayer = currentPlayer;

        gameUI.UpdateUI(gameState);

        if (!isHotseatGame)
        {
            if (currentPlayer == humanPlayerTurn)
            {

                gameBoard.UpdateBoard(gameState);
                return;
            }

            Vector2Int bestMove = aiPlayer.GetMove(gameState, 'o');
            //if minimax doesn't work yet, we will place the symbol in next available spot.
            if (bestMove == null)
            {
                Debug.LogError("ERROR: best move is null.");
            }

            Debug.Log("AI moved -> " + bestMove.x + " " + bestMove.y);
            StartCoroutine(MakeMoveOnState(bestMove));
        }


    }

    IEnumerator MakeMoveOnState(Vector2Int movePos)
    {
        gameState.MakeMove(movePos, currentPlayer);

        yield return new WaitForSeconds(2f);

        if (gameState.IsGameOver())
        {
            gameSystem = GameSystem.GAMEOVER;
            GameOver();
        }
        else
        {
            //gameBoard.UpdateBoard(gameState);
            Debug.Log(gameState);

            gameSystem = currentPlayer == 1 ? GameSystem.PLAYER2TURN : GameSystem.PLAYER1TURN;

            if (gameSystem == GameSystem.PLAYER1TURN)
            {
                Player1Turn();
            }
            else
            {
                Player2Turn();
            }
        }

    }

    //void NextTurn()
    //{

    //    //// Check if the game is over
    //    //if (gameState.IsGameOver())
    //    //{
    //    //    Debug.Log("isGameOver == True");
    //    //    GameOver();
    //    //    return;
    //    //}

    //    ////update the GameBoard
    //    char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
    //    //gameBoard.UpdateBoard(gameState, nextPlayer);
    //    //gameUI.UpdateUI(gameState, nextPlayer);
    //    //Debug.Log(gameState.ToString());
        


    //    currentPlayer = nextPlayer;
    //    Debug.Log("next player: " + nextPlayer);
    //    if (currentPlayer != humanPlayer && opponentType != OpponentType.HUMAN)   //if its the AI turn
    //    {
    //        AiTakeTurn();
    //        //StartCoroutine(AiThinking());
    //    }
    //}

    //void AiTakeTurn()
    //{
    //    //Debug.Log("AI Player: " + currentPlayer);
    //    Vector2Int bestMove = aiPlayer.GetMove(gameState, currentPlayer);
    //    //Debug.Log("AI choose -> " + bestMove.x + " " + bestMove.y);
    //    //List<GameState> possibleStates = gameState.GetNextPossibleState(currentPlayer);
    //    //Debug.Log(possibleStates.Count);

    //    //if minimax doesn't work yet, we will place the symbol in next available spot.
    //    if (bestMove == null)
    //    {
    //        Debug.LogError("ERROR: best move is null.");
    //    }

    //    if(gameState.MakeMove(bestMove, currentPlayer))
    //    {
    //        Debug.Log("AI moved -> " + bestMove.x + " " + bestMove.y);

    //        //update the GameBoard

    //        char nextPlayer = gameState.GetNextPlayerChar(currentPlayer);
    //        gameBoard.UpdateBoard(gameState, nextPlayer);
    //        gameUI.UpdateUI(gameState, nextPlayer);
    //        Debug.Log(gameState.ToString());
    //        currentPlayer = nextPlayer;

    //        // Check if the game is over
    //        if (gameState.IsGameOver())
    //        {
    //            Debug.Log("isGameOver == True");
    //            GameOver();
    //            return;
    //        }
    //       //NextTurn();
    //    }
    //}

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

public enum GameSystem
{
    START,
    PLAYER1TURN,
    PLAYER2TURN,
    GAMEOVER
}


