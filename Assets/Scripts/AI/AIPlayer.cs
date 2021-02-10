using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer
{
    public abstract int GetMove(GameState gameState, char player);
}
