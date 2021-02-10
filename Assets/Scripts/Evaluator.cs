using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator
{
    public const int WIN_SCORE = 10000;

    public bool Evaluate(GameState gameState, out int score)
    {
        score = 0;

        int[] scores = new int[2];

        scores[0] += EvaluateEndGame(gameState);
        scores[1] += EvaluateScore(gameState);

        for (int i = 0; i < scores.Length; i++)
        {
            if (Mathf.Abs(scores[i]) == WIN_SCORE)
            {
                score = scores[i];
                return true;
            }
            score += scores[i];
        }

        return false;
    }

    int EvaluateScore(GameState gameState)
    {
        int score = 0;
        float xScore = 0;
        float oScore = 0;

        int xNumMoves = gameState.GetPossibleMoves(0).Count;
        int oNumMoves = gameState.GetPossibleMoves(1).Count;

        char[,] board = gameState.Board;


        // if any moves remain for either player, eval as normal
        if (xNumMoves > 0 && oNumMoves > 0)
        {
            // check for posession of corners
            if (board[0, 0] == 'x' || board[0, 0] == 'X')
            {
                xScore += 10;
            }
            else if (board[0, 0] == 'o' || board[0, 0] == 'O')
            {
                oScore += 10;
            }

            if (board[0, board.GetLength(1)-1] == 'x' || board[0, board.GetLength(1)-1] == 'X')
            {
                xScore += 10;
            }
            else if (board[0, board.GetLength(1)-1] == 'o' || board[0, board.GetLength(1)-1] == 'O')
            {
                oScore += 10;
            }

            if (board[board.GetLength(0)-1, 0] == 'x' || board[board.GetLength(0)-1, 0] == 'X')
            {
                xScore += 10;
            }
            else if (board[board.GetLength(0)-1, 0] == 'o' || board[board.GetLength(0)-1, 0] == 'O')
            {
                oScore += 10;
            }

            if (board[board.GetLength(0)-1, board.GetLength(1)-1] == 'x' || board[board.GetLength(0)-1, board.GetLength(1)-1] == 'X')
            {
                xScore += 10;
            }
            else if (board[board.GetLength(0)-1, board.GetLength(1)-1] == 'o' || board[board.GetLength(0)-1, board.GetLength(1)-1] == 'O')
            {
                oScore += 10;
            }

            // check number of available moves
            xScore += xNumMoves;
            oScore += oNumMoves;

            // check for number of tiles possesed
            for (int h = 0; h < board.GetLength(1); h++)
            {
                for (int w = 0; w < board.GetLength(0); w++)
                {
                    if (gameState.Board[w, h] == 'x')
                    {
                        xScore += 0.01f;
                    }

                    if (gameState.Board[w, h] == 'X')
                    {
                        xScore += 0.02f;
                    }

                    if (gameState.Board[w, h] == 'o')
                    {
                        oScore += 0.01f;
                    }

                    if (gameState.Board[w, h] == 'O')
                    {
                        oScore += 0.02f;
                    }
                }
            }

        }
        else    // if no moves remain the game is over, and the evaluation should be how many pieces each player has
        {
            // check for number of tiles possesed
            for (int h = 0; h < board.GetLength(1); h++)
            {
                for (int w = 0; w < board.GetLength(0); w++)
                {
                    if (gameState.Board[w, h] == 'x')
                    {
                        xScore += 1;
                    }

                    if (gameState.Board[w, h] == 'X')
                    {
                        xScore += 2;
                    }

                    if (gameState.Board[w, h] == 'o')
                    {
                        oScore += 1;
                    }

                    if (gameState.Board[w, h] == 'O')
                    {
                        oScore += 2;
                    }
                }
            }
        }
        score = Mathf.RoundToInt(xScore - oScore);
        // return difference in scores rounded to nearest int
        return score;
    }

    int EvaluateEndGame(GameState gameState)
    {
        int score = 0;

        Debug.Log("Score Start : " + score);

        for (int h = 0; h < gameState.Board.GetLength(1); h++)
        {
            for (int w = 0; w < gameState.Board.GetLength(0); w++)
            {
                if (gameState.Board[w, h] == 'x')
                {
                    score++;
                }

                if (gameState.Board[w, h] == 'X')
                {
                    score += 2;
                }

                if (gameState.Board[w, h] == 'o')
                {
                    score--;
                }

                if (gameState.Board[w, h] == 'O')
                {
                    score -= 2;
                }
            }
        }
        Debug.Log("Score end: " + score);

        //// find who won, loss or drawed
        //char outcomeChar = gameState.GetGameOutcome();
        //if (outcomeChar == 'x')
        //{
        //    score = WIN_SCORE;
        //}
        //else if (outcomeChar == 'o')
        //{
        //    score = -WIN_SCORE;
        //}
        //else if (outcomeChar == 'd')
        //{
        //    score = 0;
        //}
        //return score;
        bool isGameOver =true;
        for (int h = 0; h < gameState.Board.GetLength(1); h++)
        {
            for (int w = 0; w < gameState.Board.GetLength(0); w++)
            {
                if (gameState.Board[w, h] == '.')  // check if there are any empty tiles left 
                {
                    isGameOver = false;
                }
            }
        }

        if(isGameOver)
        {
            if (score > 0)  //check if X wins
            {
                score = WIN_SCORE;
            }
            else if (score < 0)                       //check if O wins
            {
                score = WIN_SCORE;
            }
            else if (score == 0)  //check if its a draw
            {
                score = 0;
            }
        }
       
        return 0;
    }


    //int EvaluateMobility(GameState gameState)
    //{
    //    return 1;
    //}



    //public bool IsGameOver()
    //{
    //    List<Vector2Int> xLegalMoves = GetPossibleMoves('x');
    //    if (xLegalMoves.Count == 0)  // Check if playerX has no moves left (if they aren't traped by soild tiles)
    //    {
    //        return true;
    //    }

    //    List<Vector2Int> oLegalMoves = GetPossibleMoves('o');
    //    if (oLegalMoves.Count == 0)  // Check if playerO has no moves left (if they aren't traped by soild tiles)
    //    {
    //        return true;
    //    }

    //    for (int h = 0; h < board.GetLength(1); h++)
    //    {
    //        for (int w = 0; w < board.GetLength(0); w++)
    //        {
    //            if (board[w, h] == '.')  // check if there are any empty tiles left 
    //            {
    //                return false;
    //            }
    //        }
    //    }
    //    return true;
    //}

    //public char GetGameOutcome()
    //{
    //    char outcome;
    //    p1Score = 0;
    //    p2Score = 0;

    //    Debug.Log("Score Start - P1 score: " + p1Score + " P2 score: " + p2Score);

    //    for (int h = 0; h < board.GetLength(1); h++)
    //    {
    //        for (int w = 0; w < board.GetLength(0); w++)
    //        {
    //            if (board[w, h] == 'x')
    //            {
    //                p1Score++;
    //            }

    //            if (board[w, h] == 'X')
    //            {
    //                p1Score += 2;
    //            }

    //            if (board[w, h] == 'o')
    //            {
    //                p2Score++;
    //            }

    //            if (board[w, h] == 'O')
    //            {
    //                p2Score += 2;
    //            }
    //        }
    //    }
    //    Debug.Log("Score end - P1 score: " + p1Score + " P2 score: " + p2Score);

    //    if (p1Score == p2Score)  //check if its a draw
    //    {
    //        outcome = 'd';
    //    }
    //    else if (p1Score > p2Score)  //check if X wins
    //    {
    //        outcome = 'x';
    //    }
    //    else                        //check if O wins
    //    {
    //        outcome = 'o';
    //    }
    //    return outcome;
    //}
}
