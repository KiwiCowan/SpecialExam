using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public GameObject curPlayerPanel;

    // Variables for CurPlayerPanel
    private TextMeshProUGUI curPlayerText;

    void Awake()
    {
        curPlayerPanel = this.transform.GetChild(0).gameObject;
        curPlayerText = curPlayerPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateUI(GameState gameState, char curPlayerChar)
    {
        curPlayerText.text = curPlayerChar.ToString();
    }

}
