using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMinimax : AIPlayer
{
    public int MaxDepth { get; set; } = 0;
    char isMax = 'x';

    Evaluator evaluator = new Evaluator();
    public override Vector2Int GetMove(GameState gameState, char player)
    {
        Vector2Int choosenMove = gameState.GetMoveFromIndex(Minimax(gameState, MaxDepth, player, int.MinValue, int.MaxValue));
        return choosenMove;
    }

    int Minimax(GameState gameState, int depth, char player, int alpha, int beta)
    {
        List<int> PossibleMove = gameState.GetPossibleMoves(1);

        if (gameState.IsGameOver() /*|| nextGameStates.Count <= 0 */|| depth == 0)
        {
            //return player == 'x' ? gameState.P1Pos : gameState.P2Pos;
            return gameState.LastMoveIndex;
        }

        if (player == isMax)
        {
            
            int bestEval = int.MinValue;
            int bestMove = -1;

            Debug.Log("Minimax - depth: " + depth + ", possible states: " + nextGameStates.Count);
            // CHaange it to nextpossible moves?????????????????????????????????????????????????????????????????????
            foreach (GameState state in nextGameStates)
            {
                int newMove = Minimax(state, depth - 1, 'o', alpha, beta);

                //GameState newGameState = gameState.Dupilcate();
                //newGameState.MakeMove(newMove, player);

                int newEval = evaluator.Evaluate(state);

               
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
        }
        else
        {
            int bestEval = player == 'x' ? int.MinValue : int.MaxValue;
            int bestMove = -1;

            Debug.Log("Minimax - depth: " + depth + ", possible states: " + nextGameStates.Count);

            foreach (GameState state in nextGameStates)
            {
                int newMove = Minimax(state, depth - 1, player == 'x' ? 'o' : 'x', alpha, beta);

                GameState newGameState = gameState.Dupilcate();
                newGameState.MakeMove(newMove, player);

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
            gameState.LastMoveIndex = bestMove;
            return bestMove;
        }
       
    }
}
