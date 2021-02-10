using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMinimax : AIPlayer
{
    public int MaxDepth { get; set; } = 6;
    char isMax = 'x';

    Evaluator evaluator = new Evaluator();
    public override int GetMove(GameState gameState, char player)
    {
        //Vector2Int choosenMove = gameState.GetMoveFromIndex();
        return Minimax(gameState, MaxDepth, player, int.MinValue, int.MaxValue);
    }

    int Minimax(GameState gameState, int depth, char player, int alpha, int beta)
    {
        int eVal;
        bool isTerminal = evaluator.Evaluate(gameState, out eVal);


        if (isTerminal || depth == 0) /*|| nextGameStates.Count <= 0 *//*|| depth == 0)*/
        {
            //return player == 'x' ? gameState.P1Pos : gameState.P2Pos;
            if (isTerminal) // if the games over send back the last move
            {
                return gameState.LastMoveIndex;
            }
            
        }



        if (player == isMax)
        {

            int maxEval = int.MinValue;
            int bestMove = -1;

            List<int> PossibleMove = gameState.GetPossibleMovesIndexs(1);

            Debug.Log("Minimax - depth: " + depth + ", possible states: " + PossibleMove.Count);
            // CHaange it to nextpossible moves?????????????????????????????????????????????????????????????????????
            for (int moveIndex = 0; moveIndex < PossibleMove.Count; moveIndex++)
            {


                GameState newGameState = gameState.Dupilcate();
                newGameState.MakeMove(moveIndex, 1);
                int newMove = Minimax(newGameState, depth - 1, 'o', alpha, beta);

                int newEval;
                evaluator.Evaluate(newGameState, out newEval);


                if (newEval > maxEval)
                {
                    maxEval = newEval;
                    bestMove = newMove;
                }

                // Alpha beta pruning for maximizing player
                alpha = Mathf.Max(alpha, maxEval);
                if (alpha >= beta)
                {
                    break;
                }
            }
            return bestMove;
        }
        else
        {
            int minEval = int.MaxValue;
            int bestMove = -1;

            List<int> PossibleMove = gameState.GetPossibleMovesIndexs(2);

            Debug.Log("Minimax - depth: " + depth + ", possible states: " + PossibleMove.Count);
            // CHaange it to nextpossible moves?????????????????????????????????????????????????????????????????????
            for (int moveIndex = 0; moveIndex < PossibleMove.Count; moveIndex++)
            {
                Debug.Log("Minimax - depth: " + depth + ", move index " + moveIndex);

                GameState newGameState = gameState.Dupilcate();
                newGameState.MakeMove(moveIndex, 2);
                int newMove = Minimax(newGameState, depth - 1, 'x', alpha, beta);

                int newEval;
                evaluator.Evaluate(newGameState, out newEval);


                if (newEval < minEval)
                {
                    minEval = newEval;
                    bestMove = newMove;
                }

                // Alpha beta pruning for maximizing player
                beta = Mathf.Max(beta, minEval);
                if (alpha >= beta)
                {
                    break;
                }
            }
            return bestMove;
            //int bestEval = player == 'x' ? int.MinValue : int.MaxValue;
            //int bestMove = -1;

            //Debug.Log("Minimax - depth: " + depth + ", possible states: " + nextGameStates.Count);

            //foreach (GameState state in nextGameStates)
            //{
            //    int newMove = Minimax(state, depth - 1, player == 'x' ? 'o' : 'x', alpha, beta);

            //    GameState newGameState = gameState.Dupilcate();
            //    newGameState.MakeMove(newMove, player);

            //    int newEval = evaluator.Evaluate(state);

            //    if (player == 'x')
            //    {
            //        if (newEval > bestEval)
            //        {
            //            bestEval = newEval;
            //            bestMove = newMove;
            //        }

            //        // Alpha beta pruning for maximizing player
            //        alpha = Mathf.Max(alpha, bestEval);
            //        if (alpha >= beta)
            //        {
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        if (newEval < bestEval)
            //        {
            //            bestEval = newEval;
            //            bestMove = newMove;
            //        }

            //        // Alpha beta pruning for maximizing player
            //        alpha = Mathf.Min(beta, bestEval);
            //        if (beta <= alpha)
            //        {
            //            break;
            //        }
            //    }
            //}
            //Debug.Log(bestMove);
            //gameState.LastMoveIndex = bestMove;
            //return bestMove;
        }

    }
}
