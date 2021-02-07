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
        Debug.Log("currPlayerPos: (" + x + "," + y + ")");

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
        //bool isValid = false;
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            //Debug.Log("IVM = false = " + x + "," + y);
            return false;
        }

        char enemyTile = currPlayer == 'x' ? 'o' : 'x';
        //if ( board[x, y] == enemyTile ? ')
        //{
        //    //(currPlayer == 'x' ? p1Pos : p2Pos) == new Vector2Int(x, y) ||
        //}

        if (board[x, y] == '.' || board[x, y] == currPlayer || board[x, y] == enemyTile)
        {
            return true;
        }

        return false;
    }

    public bool MakeMove(Vector2Int playerPos, char curPlayer)
    {
        if (IsValidMove(playerPos.x, playerPos.y, curPlayer))
        {
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
