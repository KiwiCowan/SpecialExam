using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDumb : AIPlayer
{
    public override int GetMove(GameState gameState, char player)
    {
        int playerInt = player == 'x' ? 1 : 2;
        List<int> legalMoves = gameState.GetPossibleMovesIndexs(playerInt);
        int randIndex = Random.Range(0, legalMoves.Count);
        return legalMoves[randIndex];
    }
}
