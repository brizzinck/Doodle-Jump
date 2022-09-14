using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameUIController : BaseGameUIController
{
    public override void DisplayInfoGameOver()
    {
        _scoreDisplay[0].text = "high score: " + Saver.GetIntPrefs("HighScore").ToString();
        _scoreDisplay[1].text = "last score: " + Saver.GetIntPrefs("LastScore").ToString();
        _coinsDisplay[0].text = "your coins: " + Saver.GetIntPrefs("Coins").ToString();
        _coinsDisplay[1].text = "continue for: " + (int)(Mathf.Clamp((Saver.GetIntPrefs("LastScore") / 100), 10, 99));
    }
    protected override void Start()
    {
        base.Start();
    }
}
