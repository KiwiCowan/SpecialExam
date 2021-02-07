using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer
{
    public abstract Vector2Int GetMove(GameState gameState, char player);
}
