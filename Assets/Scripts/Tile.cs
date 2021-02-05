using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void Clicked(Vector2Int tilePos);
    public static event Clicked OnClicked;

    public Vector2Int pos;
    public char tileType;
    SpriteRenderer renderer;

    public void SetPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void SetType(char newTileType)
    {
        if (tileType == newTileType)    // checks if the tile is the same type as player
        {
            return;
        }
        else if (tileType == '.')       //checks if empty
        {
            tileType = newTileType;
            // renderer.material.SetColor(Color.blue)

        }
        else if (tileType == (newTileType == 'x' ? 'o' : 'x'))  //checks if the tile has been captured already set to solid
        {
            tileType = newTileType == 'x' ? 'X' : 'O';
        }

        switch (tileType)
        {
            case '.':
                {
                    renderer.color = Color.white;
                    break;
                }
            case 'x':
                {
                    renderer.color = Color.magenta;
                    break;
                }
            case '0':
                {
                    renderer.color = Color.cyan;
                    break;
                }
            case 'X':
                {
                    renderer.color = Color.red;
                    break;
                }
            case 'O':
                {
                    renderer.color = Color.blue;
                    break;
                }
        }
    }

    public void OnMouseButtonDown()
    {
        if (OnClicked != null)
        {
            OnClicked(pos);
        }
    }
}


