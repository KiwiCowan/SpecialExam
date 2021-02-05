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

    public char[,] Board { get => board;}
    public Vector2Int P1Pos { get => p1Pos;}
    public Vector2Int P2Pos { get => p2Pos;}

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
                board[w, h] = '.';
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
        Vector2Int curPlayerPos = curPlayer == 'x' ? p2Pos : p1Pos;

        int x = curPlayerPos.x;
        int y = curPlayerPos.y;

        if (IsValidMove(x +1, y, curPlayer))    //Right
        {
            possibleMoves.Add(new Vector2Int(x, y));
        }
        if (IsValidMove(x, y + 1, curPlayer))   //Up
        {
            possibleMoves.Add(new Vector2Int(x, y));
        }
        if (IsValidMove(x -1, y, curPlayer))    //Left
        {
            possibleMoves.Add(new Vector2Int(x, y));
        }
        if (IsValidMove(x, y -1, curPlayer))    //Down
        {
            possibleMoves.Add(new Vector2Int(x, y));
        }

        return possibleMoves;

    }

    public bool IsValidMove(int x, int y, char currPlayer)
    {
        //bool isValid = false;
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return false;
        }
        char enemyTile = currPlayer == 'x' ? 'o' : 'x';

        if (board[x, y] == '.' || board[x, y] == currPlayer || board[x, y] == enemyTile)
        {
            return true;
        }

        return false;
    }

    public void MakeMove(Vector2Int playerPos, char curPlayer)
    {
        if(IsValidMove(playerPos.x, playerPos.y, curPlayer))
        {
            if (board[playerPos.x, playerPos.y] == GetNextPlayerChar(curPlayer))    //set the tile to solid if already captured
            {
                board[playerPos.x, playerPos.y] = curPlayer == 'x' ? 'O' : 'X';
            }

            board[playerPos.x, playerPos.y] = curPlayer;    //sets char

            if (curPlayer == 'x')
            {
                p1Pos = playerPos;
            }
            else
            {
                p2Pos = playerPos;
            }
        }
    }

    public char GetNextPlayerChar(char currPlayer)
    {
        char enemyTile = currPlayer == 'x' ? 'o' : 'x';
        return enemyTile;
    }
}
