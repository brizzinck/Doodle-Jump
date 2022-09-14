using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseGameUIController : MonoBehaviour
{
    [SerializeField] protected Button _pause;
    [SerializeField] protected Button _resume;
    [SerializeField] protected Button[] _menu;
    [SerializeField] protected Button _replay;
    [SerializeField] protected GameObject FonPause;
    [SerializeField] protected Button _audionControll;
    [SerializeField] protected Button _vibrateControll;
    [SerializeField] protected Button _continueGame;
    [SerializeField] protected Text[] _audioDisplay;
    [SerializeField] protected Text[] _vibrateDisplay;
    [SerializeField] protected Text[] _scoreDisplay;
    [SerializeField] protected Text[] _coinsDisplay;
    [SerializeField] protected GameOver _gameOver;
    [SerializeField] protected AudioClip _startAudio;

    public virtual void ControllPausebutton(bool tap)
    {
        _pause.interactable = tap;
    }
    public virtual void DisplayInfoGameOver()
    {
        _scoreDisplay[0].text = "high score: " + Saver.GetIntPrefs("HighScore").ToString();
        _scoreDisplay[1].text = "last score: " + Saver.GetIntPrefs("LastScore").ToString();
        _coinsDisplay[0].text = "your coins: " + Saver.GetIntPrefs("Coins").ToString();
        _coinsDisplay[1].text = "continue for: " + (int)(Mathf.Clamp(2 * (Saver.GetIntPrefs("LastScore") / 100), 10, 99));
    }
    protected virtual void RePlay()
    {
        AudioController.Instance.PlayButtons();
        SceneManager.LoadScene("GameScene");
    }
    protected virtual void ContinueGame()
    {
        StartCoroutine(_gameOver.Continue(Saver.GetIntPrefs("LastScore") / 100));
    }
    protected virtual void Start()
    {
        AudioController.Instance.PlaySound(_startAudio);
        SetValues();
    }

    private void SetValues()
    {
        _pause.onClick.AddListener(Pause);
        _resume.onClick.AddListener(Resume);
        _menu[0].onClick.AddListener(GoMenu);
        _menu[1].onClick.AddListener(GoMenu);
        _replay.onClick.AddListener(RePlay);
        _vibrateControll.onClick.AddListener(VibrateControll);
        _audionControll.onClick.AddListener(AudioControll);
        _continueGame.onClick.AddListener(ContinueGame);
        DisplayOptionsInfo("MuteVibrate", _vibrateDisplay);
        DisplayOptionsInfo("MuteSound", _audioDisplay);
    }

    protected void GoMenu()
    {
        AudioController.Instance.PlayButtons();
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
    private void Pause()
    {
        AudioController.Instance.PlayButtons();
        AudioController.Instance.Pause();
        FonPause.SetActive(true);
        Time.timeScale = 0;

    }
    private void Resume()
    {
        AudioController.Instance.PlayButtons();
        AudioController.Instance.Play();
        FonPause.SetActive(false);
        Time.timeScale = 1;
    }
    private void AudioControll()
    {
        AudioController.Instance.PlayButtons();
        AudioController.MuteSound();
        DisplayOptionsInfo("MuteSound", _audioDisplay);
    }
    private void VibrateControll()
    {
        AudioController.Instance.PlayButtons();
        VibrateController.MuteVibrate();
        DisplayOptionsInfo("MuteVibrate", _vibrateDisplay);
    }

    private void DisplayOptionsInfo(string namePrefs, Text[] text)
    {
        if (Saver.GetStringPrefs(namePrefs) == "False")
        {
            text[0].color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            text[1].color = new Color(0.03f, 0.6f, 0f, 1f);
        }
        if (Saver.GetStringPrefs(namePrefs) == "True")
        {
            text[1].color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            text[0].color = new Color(0.03f, 0.6f, 0f, 1f);
        }
    }
}
