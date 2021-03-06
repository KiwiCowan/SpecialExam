﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    public delegate void Clicked(Vector2Int tilePos);
    public static event Clicked OnClicked;

    public Vector2Int pos;
    public TileType tileType;
    SpriteRenderer sRenderer;
    TextMeshPro text;

    //bool isHighlighed = false;

    public void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
    }

    public void SetPos(int x, int y)
    {
        pos.x = x;
        pos.y = y;
    }

    public void SetType(char newTileType)
    {
        TileType newType = (TileType)newTileType;

        tileType = newType;
        DrawTile();

        //isHighlighed = false;
        // if (newTileType == '.')    // checks if the tile is null
        //{
        //    tileType = newTileType;
        //}
        //else if (tileType == newTileType)    // checks if the tile is the same type as player
        //{
        //    return;
        //}
        //else if (tileType == '.')       //checks if empty
        //{
        //    tileType = newTileType;
        //    // sRenderer.material.SetColor(Color.blue)

        //}
        //else if (tileType == (newTileType == 'X' ? 'o' : 'x'))  //checks if the tile has been captured already set to solid
        //{
        //    tileType = newTileType;
        //    Debug.Log("else if -> Tile -> " + tileType);
        //}

        //DrawTile();

        //switch (tileType)
        //{
        //    case '.':
        //        {
        //            text.text = "";
        //            sRenderer.color = Color.white;
        //            break;
        //        }
        //    case 'x':
        //        {
        //            text.text = "x";
        //            sRenderer.color = Color.red;
        //            break;
        //        }
        //    case 'o':
        //        {
        //            text.text = "o";
        //            sRenderer.color = Color.blue;
        //            break;
        //        }
        //    case 'X':
        //        {
        //            text.text = "X";
        //            sRenderer.color = Color.red;
        //            break;
        //        }
        //    case 'O':
        //        {
        //            text.text = "O";
        //            sRenderer.color = Color.blue;
        //            break;
        //        }
        //}
    }

    public void HighlightTile()
    {
        //Debug.Log("hightlight tile " + pos.x + "," + pos.y);
        // isHighlighed = true;        
        //sRenderer.color = Color.clear;
        //DrawTile();
        switch (tileType)
        {
            case TileType.EMPTY:
                {
                    sRenderer.color = Color.grey;
                    break;
                }
            case TileType.P1:
                {
                    sRenderer.color = Color.magenta;
                    break;
                }
            case TileType.P2:
                {
                    sRenderer.color = Color.cyan;
                    break;
                }
            //case TileType.P1_SOILD:
            //    {
            //        sRenderer.color = Color.magenta;
            //        break;
            //    }
            //case TileType.P2_SOILD:
            //    {
            //        sRenderer.color = Color.cyan;
            //        break;
            //    }
        }
    }

    public void DrawTile()
    {
        text.color = Color.black;
        switch (tileType)
        {
            case TileType.EMPTY:
                {
                    text.text = "";
                    sRenderer.color = Color.white;
                    break;
                }
            case TileType.P1:
                {
                    text.text = "x";
                    sRenderer.color = Color.red;
                    break;
                }
            case TileType.P2:
                {
                    text.text = "o";
                    sRenderer.color = Color.blue;
                    break;
                }
            case TileType.P1_SOILD:
                {
                    text.text = "X";
                    sRenderer.color = Color.red;
                    break;
                }
            case TileType.P2_SOILD:
                {
                    text.text = "O";
                    sRenderer.color = Color.blue;
                    break;
                }
        }
    }

    public void CurrentTile()
    {
        text.color = Color.white;
    }

    private void OnMouseDown()
    {
        //if (!isHighlighed)
        //{
        //    return;
        //}

        if (OnClicked != null)
        {
            OnClicked(pos);
        }
        //isHighlighed = false;
    }
}

public enum TileType
{
    EMPTY = '.',
    P1 = 'x',
    P2 = 'o',
    P1_SOILD = 'X',
    P2_SOILD = 'O'
}


