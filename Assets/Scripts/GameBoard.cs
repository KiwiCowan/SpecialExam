using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject prefab;

    //GameState gameState;
    Tile[,] tiles;

    //public GameBoard(GameState _gameState)
    //{
    //    this.gameState = _gameState;
    //}

    public void GenBoard(int width, int height)
    {
        tiles = new Tile[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject tileGameObject = Instantiate(prefab);
                tileGameObject.transform.parent = transform;
                tileGameObject.transform.position = new Vector2(-height / 2.0f + x + 0.5f, -width / 2.0f + y + 0.5f);
                tileGameObject.SetActive(true);

                Tile tile = tileGameObject.GetComponent<Tile>();
                tile.SetPos(x, y);
                tile.SetType('.');
                if (x == 0 && y==0)
                {
                    tile.SetType('x');
                    Debug.Log(tile.tileType);
                }
                else if (x == width -1 && y == height - 1)
                {
                    tile.SetType('o');
                }

                tiles[x, y] = tile;
            }
        }
    }

    void UpdateBoard(GameState _gameState)
    {
        GameState gameState = _gameState;

        for (int h = 0; h < tiles.GetLength(1); h++)    //Updates board by setting types
        {
            for (int w = 0; w < tiles.GetLength(0); w++)
            {
                tiles[w, h].SetType(gameState.Board[w, h]);
            }
        }
    }
}
