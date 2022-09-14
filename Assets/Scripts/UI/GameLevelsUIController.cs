using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLevelsUIController : BaseGameUIController
{
    [SerializeField] private LevelsController _levelsController;
    [SerializeField] private Text[] _levelsInfo;
    [SerializeField] private Button _goMenu;
    [SerializeField] private Button _next;
    public override void DisplayInfoGameOver()
    {
        _scoreDisplay[0].text = "your level: " + Saver.GetIntPrefs("CurrentLevel").ToString();
        _coinsDisplay[0].text = "your coins: " + Saver.GetIntPrefs("Coins").ToString();
        _coinsDisplay[1].text = "continue for: " + Mathf.Clamp(_levelsController.ToFinished / 2, 10, 200);
    }
    public void Finish()
    {
        ControllPausebutton(false);
        _levelsController.Finished = true;
        _levelsInfo[0].text = "your level: " + (Saver.GetIntPrefs("CurrentLevel") + 1).ToString();
        _levelsInfo[1].text = "your coins: " + Saver.GetIntPrefs("Coins").ToString();
    }
    protected override void RePlay()
    {
        AudioController.Instance.PlayButtons();
        Time.timeScale = 1;
        SceneManager.LoadScene("GameLevelsScene");
    }
    protected override void ContinueGame()
    {
        StartCoroutine(_gameOver.Continue(_levelsController.ToFinished / 2));
    }
    protected override void Start()
    {
        base.Start();
        _goMenu.onClick.AddListener(GoMenu);
        _next.onClick.AddListener(RePlay);
    }
}
