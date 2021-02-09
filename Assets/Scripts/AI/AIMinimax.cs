using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMinimax : AIPlayer
{
    public int MaxDepth { get; set; }

    Evaluator evaluator = new Evaluator();
    public override Vector2Int GetMove(GameState gameState, char player)
    {
        return Minimax(gameState, MaxDepth, player, int.MinValue, int.MaxValue);
    }

    Vector2Int Minimax(GameState gameState, int depth, char player, int alpha, int beta)
    {
        List<GameState> nextGameStates = gameState.GetNextPossibleState(player);

        if (gameState.IsGameOver() || nextGameStates.Count <= 0 || depth <= 0)
        {
            //return player == 'x' ? gameState.P1Pos : gameState.P2Pos;
            return gameState.LastMove;
        }
        else
        {


            int bestEval = player == 'x' ? int.MinValue : int.MaxValue;
            Vector2Int bestMove = new Vector2Int(-1, -1);

            Debug.Log("Minimax - depth: " + depth + ", possible states: " + nextGameStates.Count);

            foreach (GameState state in nextGameStates)
            {
                Vector2Int newMove = Minimax(state, depth - 1, player == 'x' ? 'o' : 'x', alpha, beta);

                //GameState newGameState = gameState.Dupilcate();
                state.MakeMove(newMove, player);

                int newEval = evaluator.Evaluate(state);

                if (player == 'x')
                {
                    if (newEval > bestEval)
                    {
                        bestEval = newEval;
                        bestMove = newMove;
                    }

                    // Alpha beta pruning for maximizing player
                    alpha = Mathf.Max(alpha, bestEval);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                else
                {
                    if (newEval < bestEval)
                    {
                        bestEval = newEval;
                        bestMove = newMove;
                    }

                    // Alpha beta pruning for maximizing player
                    alpha = Mathf.Min(beta, bestEval);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            Debug.Log(bestMove);
            return bestMove;
        }
    }
}
