using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AINeuralNetwork : AIPlayer
{
    System.Random random = new System.Random();
    NeuralNetwork neuralNetwork;

    public AINeuralNetwork(NeuralNetwork _neuralNetwork)
    {
        this.neuralNetwork = _neuralNetwork;
    }
    public override Vector2Int GetMove(GameState gameState, char player)
    {
        DataSet dataSet = gameState.PrepareDataSet(
            neuralNetwork.inputLayer.Count,
            neuralNetwork.outputLayer.Count
            );

        double[] result = neuralNetwork.Compute(dataSet.Values);

        double moveValue = double.MinValue;
        int move = -1;

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] > moveValue)
            {
                moveValue = result[i];
                move = i;
            }
        }

        return

    }
}
