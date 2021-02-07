using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDumb : AIPlayer
{
    public override Vector2Int GetMove(GameState gameState, char player)
    {
        List<Vector2Int> legalMoves = gameState.GetPossibleMoves(player);
        int randIndex = Random.Range(0, legalMoves.Count);
        return legalMoves[randIndex];
    }
}
