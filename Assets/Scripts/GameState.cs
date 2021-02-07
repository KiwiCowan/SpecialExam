using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public int height;
    public int width;

    char[,] board;

    Vector2Int p1Pos;   // x
    Vector2Int p2Pos;   // o

    int p1Score;    // x
    int p2Score;    // o

    public char[,] Board { get => board; }
    public Vector2Int P1Pos { get => p1Pos; }
    public Vector2Int P2Pos { get => p2Pos; }

    public GameState(int _width, int _height)
    {
        this.height = _height;
        this.width = _width;
        this.board = new char[width, height];

        Reset();

    }

    public GameState(char[,] newBoard)
    {
        this.board = newBoard;
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
        return new GameState(board.Clone() as char[,]);
    }

    public List<Vector2Int> GetPossibleMoves(char curPlayer)
    {
        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        Vector2Int curPlayerPos = curPlayer == 'x' ? p1Pos : p2Pos;

        int x = curPlayerPos.x;
        int y = curPlayerPos.y;
        //Debug.Log("currPlayerPos: " + curPlayer + " (" + x + "," + y + ")");

        //char enemyTile = curPlayer == 'x' ? 'o' : 'x';

        if (IsValidMove(x + 1, y, curPlayer) /*&& board[x, y] == enemyTile*/)    //Right
        {
            possibleMoves.Add(new Vector2Int(x + 1, y));
        }

        if (IsValidMove(x, y + 1, curPlayer))   //Up
        {
            possibleMoves.Add(new Vector2Int(x, y + 1));
        }

        if (IsValidMove(x - 1, y, curPlayer))    //Left
        {
            possibleMoves.Add(new Vector2Int(x - 1, y));
        }

        if (IsValidMove(x, y - 1, curPlayer))    //Down
        {
            possibleMoves.Add(new Vector2Int(x, y - 1));
        }

        return possibleMoves;

    }

    public bool IsValidMove(int x, int y, char currPlayer)
    {
        Vector2Int playerPos = currPlayer == 'x' ? p1Pos : p2Pos;


        

        //bool isValid = false;
        if (x < 0 || x >= width || y < 0 || y >= height /*|| board[playerPos.x, playerPos.y] == */)
        {
            //Debug.Log("IVM = false = " + x + "," + y);
            return false;
        }

        char enemyTile = currPlayer == 'x' ? 'o' : 'x';

        Vector2Int tempMove = new Vector2Int(x, y);
        if (tempMove == (enemyTile == 'x' ? p1Pos : p2Pos)) // check if the move is on the same tile as the enemy
        {
            return false;
            //(currPlayer == 'x' ? p1Pos : p2Pos) == new Vector2Int(x, y) ||
        }

        if (board[x, y] == '.' || board[x, y] == currPlayer || board[x, y] == enemyTile)
        {
            if (board[playerPos.x, playerPos.y] == (currPlayer == 'x' ? 'X' : 'O'))     //if the current player is on a soild then they cant capture a enemy tile
            {
                if(board[x, y] == enemyTile)
                {
                    return false;
                }
            }
            return true;
        }

        return false;
    }

    public bool MakeMove(Vector2Int playerPos, char curPlayer)
    {
        List<Vector2Int> legalMoves = GetPossibleMoves(curPlayer);
        if (legalMoves.Contains(playerPos))
        {
            //if (IsValidMove(playerPos.x, playerPos.y, curPlayer))
            //{
            if (board[playerPos.x, playerPos.y] == GetNextPlayerChar(curPlayer))    //set the tile to solid if already captured
            {
                board[playerPos.x, playerPos.y] = curPlayer == 'x' ? 'X' : 'O';
            }
            else
            {
                board[playerPos.x, playerPos.y] = curPlayer;    //sets char
            }
                
            

            if (curPlayer == 'x')
            {
                p1Pos = playerPos;
            }
            else
            {
                p2Pos = playerPos;
            }
            return true;
        }
        return false;
    }

    public char GetNextPlayerChar(char currPlayer)
    {
        char enemyTile = currPlayer == 'x' ? 'o' : 'x';
        return enemyTile;
    }

    public bool IsGameOver()
    {
        List<Vector2Int> xLegalMoves = GetPossibleMoves('x');
        if (xLegalMoves.Count == 0)  // Check if playerX has no moves left (if they aren't traped by soild tiles)
        {
            return true;
        }

        List<Vector2Int> oLegalMoves = GetPossibleMoves('o');
        if (oLegalMoves.Count == 0)  // Check if playerO has no moves left (if they aren't traped by soild tiles)
        {
            return true;
        }
                
        for (int h = 0; h < board.GetLength(1); h++)    
        {
            for (int w = 0; w < board.GetLength(0); w++)
            {
                if(board[w, h] == '.')  // check if there are any empty tiles left 
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

        if(p1Score == p2Score)  //check if its a draw
        {
            outcome = 'd';
        }
        else if(p1Score > p2Score)  //check if X wins
        {
            outcome = 'x';
        }
        else                        //check if O wins
        {
            outcome = 'o';
        }
        return outcome;
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
