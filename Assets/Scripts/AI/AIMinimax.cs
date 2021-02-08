using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMinimax : AIPlayer
{
    public int MaxDepth { get; set; } = 5;

    Evaluator evaluator = new Evaluator();
    public override Vector2Int GetMove(GameState gameState, char player)
    {       
        return Minimax(gameState, MaxDepth,player, int.MaxValue, int.MaxValue);
    }

    Vector2Int Minimax(GameState gameState, int depth, char player,int alpha, int beta)
    {
        
        if (gameState.IsGameOver() || depth == 0)
        {
            //return gameState.lastMove;
        }
        return new Vector2Int(0, 0);
    }
}
