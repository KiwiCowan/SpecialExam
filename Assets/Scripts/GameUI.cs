using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public GameObject curPlayerPanel;
    public GameObject gameOverPanel;

    
    private TextMeshProUGUI curPlayerText;
    private TextMeshProUGUI gameOutcomeText;

    void Awake()
    {
        curPlayerPanel = this.transform.GetChild(0).gameObject;
        curPlayerText = curPlayerPanel.GetComponentInChildren<TextMeshProUGUI>();

        gameOverPanel = this.transform.GetChild(1).gameObject;
        gameOutcomeText = gameOverPanel.GetComponentInChildren<TextMeshProUGUI>();
        gameOverPanel.SetActive(false);
    }

    public void UpdateUI(GameState gameState, char curPlayerChar)
    {
        curPlayerText.text = curPlayerChar.ToString();
    }

    public void GameOverUI(string outcome)
    {
        curPlayerPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        gameOutcomeText.text = outcome;
    }


}
