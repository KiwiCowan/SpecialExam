using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int height;
    public int width;

    int currentPlayer;
    char[,] board;

    Vector2Int p1Pos;   // x
    Vector2Int p2Pos;   // o

    int p1Score;    // x
    int p2Score;    // o
    int lastMoveIndex;
    readonly Vector2Int[] Moves = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    int lastPlayerToMove;
    Vector2Int lastMove;
    


    public char[,] Board { get => board; }
    public Vector2Int P1Pos { get => p1Pos; }
    public Vector2Int P2Pos { get => p2Pos; }
    public Vector2Int LastMove { get => lastMove; }
    public int LastPlayerToMove { get => lastPlayerToMove; }
    
    public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
    public int LastMoveIndex { get => lastMoveIndex; set => lastMoveIndex = value; }

    public GameState(int _width, int _height)
    {
        this.height = _height;
        this.width = _width;
        this.board = new char[width, height];

        Reset();

    }

    public GameState(char[,] newBoard, Vector2Int p1Pos, Vector2Int p2Pos)
    {
        this.board = newBoard;
        this.p1Pos = p1Pos;
        this.p2Pos = p2Pos;

    }

    public void Reset()
    {
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (w == 0 && h == 0)
                {
                    board[w, h] = 'x';
                    p1Pos = new Vector2Int(w, h);

                }
                else if (w == width - 1 && h == height - 1)
                {
                    board[w, h] = 'o';
                    p2Pos = new Vector2Int(w, h);
                }
                else
                {
                    board[w, h] = '.';
                }
            }
        }
    }

    public GameState Dupilcate()
    {
        return new GameState(board.Clone() as char[,], p1Pos, p2Pos);
    }

    public List<GameState> GetNextPossibleState(int curPlayer)
    {
        List<Vector2Int> possibleMoves = GetPossibleMoves(curPlayer);
        List<GameState> possibleStates = new List<GameState>();
        Debug.Log("Moves " + possibleMoves.Count);

        foreach (Vector2Int move in possibleMoves)
        {
            Debug.Log("Possible move: " + move + "(" + curPlayer + ")");
            GameState duplicatedState = Dupilcate();
            duplicatedState.MakeMove(move, curPlayer);
            //duplicatedState.lastMove = move;

            possibleStates.Add(duplicatedState);
            Debug.Log("State " + duplicatedState);
        }
        return possibleStates;
    }

    public List<Vector2Int> GetPossibleMoves(int curPlayer) // returns the vector2Ints of Move[]
    {
        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        Vector2Int curPlayerPos = curPlayer == 1 ? p1Pos : p2Pos;

        for (int i = 0; i < Moves.Length; i++)
        {
            Vector2Int newmove = curPlayerPos + Moves[i];
            if (IsValidMove(newmove, curPlayer) /*&& board[x, y] == enemyTile*/)    //Right
            {
                possibleMoves.Add(Moves[i]);
            }
        }

        //int x = curPlayerPos.x;
        //int y = curPlayerPos.y;
        //Debug.Log("currPlayerPos: " + curPlayer + " (" + x + "," + y + ")");

        //char enemyTile = curPlayer == 'x' ? 'o' : 'x';

        //if (IsValidMove(x + 1, y, curPlayer) /*&& board[x, y] == enemyTile*/)    //Right
        //{
        //    possibleMoves.Add(new Vector2Int(x + 1, y));
        //}

        //if (IsValidMove(x, y + 1, curPlayer))   //Up
        //{
        //    possibleMoves.Add(new Vector2Int(x, y + 1));
        //}

        //if (IsValidMove(x - 1, y, curPlayer))    //Left
        //{
        //    possibleMoves.Add(new Vector2Int(x - 1, y));
        //}

        //if (IsValidMove(x, y - 1, curPlayer))    //Down
        //{
        //    possibleMoves.Add(new Vector2Int(x, y - 1));
        //}

        return possibleMoves;

    }

    public bool IsValidMove(Vector2Int move, int player)
    {
        return IsValidMove(move.x, move.y, player);
    }

    public bool IsValidMove(int x, int y, int currPlayer)
    {
        Vector2Int playerPos = currPlayer == 1 ? p1Pos : p2Pos;

        //bool isValid = false;
        if (x < 0 || x >= width || y < 0 || y >= height /*|| board[playerPos.x, playerPos.y] == */)
        {
            //Debug.Log("IVM = false = " + x + "," + y);
            return false;
        }

        char playerTile = currPlayer == 1 ? 'x' : 'o';
        char enemyTile = currPlayer == 1 ? 'o' : 'x';
        Vector2Int enemyPos = (currentPlayer == 1 ? p2Pos : p1Pos);

        if (enemyPos.x == x && enemyPos.y == y) // check if the move is on the same tile as the enemy
        {
            return false;
            //(currPlayer == 'x' ? p1Pos : p2Pos) == new Vector2Int(x, y) ||
        }

        //bool xMove = x != playerPos.x;
        //bool yMove = y != playerPos.y;

        ////check to see if we moved diagonally - illegal
        //if(xMove && yMove)
        //{
        //    return false;
        //}

        ////check if we moved more that one space - illegal.
        //if(Mathf.Abs(x - playerPos.x) > 1 || Mathf.Abs(y - playerPos.y) > 1)
        //{
        //    return false;
        //}

        if (board[x, y] == '.' || board[x, y] == playerTile || board[x, y] == enemyTile)
        {
            if (board[playerPos.x, playerPos.y] == (playerTile == 'x' ? 'X' : 'O'))     //if the current player is on a soild then they cant capture a enemy tile
            {
                if (board[x, y] == enemyTile)
                {
                    return false;
                }
            }
            return true;
        }
        //char playerSolid = currPlayer == 'x' ? 'X' : 'O';
        //if (board[playerPos.x, playerPos.y] == playerSolid && board[x, y] == enemyTile)
        //{
        //    return false;
        //}

        return false;
    }

    public bool MakeMove(int moveIndex, int curPlayer)
    {
        lastMoveIndex = moveIndex;
        return MakeMove(Moves[moveIndex], curPlayer);
    }

    public bool MakeMove(Vector2Int movePos, int curPlayer) //Takes the move Vector2Int eg: (0,1) = down
    {
        Vector2Int playerPos = curPlayer == 1 ? p1Pos : p2Pos;

        List<Vector2Int> legalMoves = GetPossibleMoves(curPlayer);

        if (legalMoves.Contains(movePos))
        {
            int x = playerPos.x + movePos.x;
            int y = playerPos.y + movePos.y;
            //if (IsValidMove(playerPos.x, playerPos.y, curPlayer))
            //{
            if (board[x, y] == GetNextPlayerChar(curPlayer))    //set the tile to solid if already captured
            {
                board[x, y] = curPlayer == 1 ? 'X' : 'O';
            }
            else
            {
                board[x, y] = curPlayer == 1 ? 'x' : 'o';    //sets char
            }
            Debug.Log("move to -> " + x + "," + y);


            if (curPlayer == 1)
            {
                p1Pos = new Vector2Int(x,y);
            }
            else
            {
                p2Pos = new Vector2Int(x, y);
            }

            lastPlayerToMove = curPlayer;
            lastMove = movePos;
            lastMoveIndex = GetMoveIndex(movePos);


            return true;
        }
        return false;
    }

    public char GetNextPlayerChar(int currPlayer)
    {
        char enemyTile = currPlayer == 1 ? 'o' : 'x';
        return enemyTile;
    }

    public bool IsGameOver()
    {
        List<Vector2Int> xLegalMoves = GetPossibleMoves(1);
        List<Vector2Int> oLegalMoves = GetPossibleMoves(2);
        if (xLegalMoves.Count > 0 && oLegalMoves.Count > 0)  // Check if playerX has any moves left (if they aren't traped by soild tiles)
        {
            return false;
        }

        
        //if (oLegalMoves.Count > 0)  // Check if playerO has any moves left (if they aren't traped by soild tiles)
        //{
        //    return false;
        //}

        for (int h = 0; h < board.GetLength(1); h++)
        {
            for (int w = 0; w < board.GetLength(0); w++)
            {
                if (board[w, h] == '.')  // check if there are any empty tiles left 
                {
                    return false;
                }
            }
        }

        return true;
    }



    public char GetGameOutcome()
    {
        char outcome;
        p1Score = 0;
        p2Score = 0;

        Debug.Log("Score Start - P1 score: " + p1Score + " P2 score: " + p2Score);

        for (int h = 0; h < board.GetLength(1); h++)
        {
            for (int w = 0; w < board.GetLength(0); w++)
            {
                if (board[w, h] == 'x')
                {
                    p1Score++;
                }

                if (board[w, h] == 'X')
                {
                    p1Score += 2;
                }

                if (board[w, h] == 'o')
                {
                    p2Score++;
                }

                if (board[w, h] == 'O')
                {
                    p2Score += 2;
                }
            }
        }
        Debug.Log("Score end - P1 score: " + p1Score + " P2 score: " + p2Score);

        if (p1Score == p2Score)  //check if its a draw
        {
            outcome = 'd';
        }
        else if (p1Score > p2Score)  //check if X wins
        {
            outcome = 'x';
        }
        else                        //check if O wins
        {
            outcome = 'o';
        }
        return outcome;
    }

    public Vector2Int GetMoveFromIndex(int index)
    {
        Vector2Int curPlayerPos = currentPlayer == 1 ? p1Pos : p2Pos;

        Vector2Int newmove = Moves[index];
        return newmove;
    }
    public Vector2Int GetTileFromIndex(int index)
    {
        Vector2Int curPlayerPos = currentPlayer == 1 ? p1Pos : p2Pos;

        Vector2Int newmove = curPlayerPos + Moves[index];
        return newmove;
    }

    public int GetMoveIndex(Vector2Int move)
    {
        //Vector2Int curPlayerPos = currentPlayer == 1 ? p1Pos : p2Pos;
        int moveIndex = -1;
        for (int i = 0; i < Moves.Length; i++)
        {
            if (Moves[i] == move)
            {
                moveIndex = i;
            }

        }

        return moveIndex;
    }
    public DataSet PrepareDataSet(int inputLayerSize, int outputLayerSize, int correctMove = -1)
    {
        double[] values = new double[inputLayerSize];
        double[] targets = new double[outputLayerSize];

        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                values[y * width + x] = board[x, y];
            }
        }

        values[values.Length - 1] = lastPlayerToMove;

        if (correctMove >= 0)
        {
            targets[correctMove] = 1;
        }

        return new DataSet(values, targets);
    }

    public override string ToString()
    {
        string gameStateInfo = ("GameState Debug: " + "\n");
        string playersPos = ("P1pos: " + p1Pos + "\n" + "P2pos: " + p2Pos);
        string boardInfo = ("Board: (" + width + "," + height + ")" + "\n");



        for (int h = 0; h < board.GetLength(1); h++)    //Updates board by setting types
        {
            string boardChar = "";
            for (int w = 0; w < board.GetLength(0); w++)
            {
                boardChar += board[w, h].ToString() + " ";
            }
            boardChar += "\n";
            boardInfo += boardChar;

        }

        gameStateInfo += playersPos + "\n" + boardInfo;
        return gameStateInfo;
    }
}
