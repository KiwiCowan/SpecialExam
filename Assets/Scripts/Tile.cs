using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public delegate void Clicked(Vector2Int tilePos);
    public static event Clicked OnClicked;

    public Vector2Int pos;
    public char tileType;
    SpriteRenderer sRenderer;

    public void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void SetType(char newTileType)
    {
        if (newTileType == '.')    // checks if the tile is null
        {
            tileType = newTileType;
        }
        else if (tileType == newTileType)    // checks if the tile is the same type as player
        {
            return;
        }
        else if (tileType == '.')       //checks if empty
        {
            tileType = newTileType;
            // sRenderer.material.SetColor(Color.blue)

        }
        else if (newTileType == (tileType == 'x' ? 'o' : 'x'))  //checks if the tile has been captured already set to solid
        {
            tileType = newTileType == 'x' ? 'X' : 'O';
        }

        switch (tileType)
        {
            case '.':
                {
                    sRenderer.color = Color.white;
                    break;
                }
            case 'x':
                {
                    sRenderer.color = Color.magenta;
                    break;
                }
            case 'o':
                {
                    sRenderer.color = Color.cyan;
                    break;
                }
            case 'X':
                {
                    sRenderer.color = Color.red;
                    break;
                }
            case 'O':
                {
                    sRenderer.color = Color.blue;
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


