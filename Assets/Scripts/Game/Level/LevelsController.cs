using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelsController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text[] _levelsDisplay;
    [SerializeField] private Text _coinsDisplay;
    [SerializeField] Score _score;
    private int _toFinished;
    private bool _finished;
    public int ToFinished { get => _toFinished; }
    public bool Finished { get => _finished; set => _finished = value; }
    private void Start()
    {
        Display();
    }
    private void Update()
    {
        Display();
    }

    private void Display()
    {
        if (_finished)
        {
            _levelsDisplay[0].text = "";
            _levelsDisplay[1].text = "";
            _coinsDisplay.text = "";
            return;
        }
        if (_score.score != 0)
            _toFinished = (int)(_score.score * 100 / (Saver.GetIntPrefs("CurrentLevel") * 1050));
        if (_toFinished > 100)
            _toFinished = 100;
        if (_toFinished < 0) _toFinished = 0;
        _levelsDisplay[0].text = "levels: " + Saver.GetIntPrefs("CurrentLevel") + ", " + _toFinished + "%";
        _levelsDisplay[1].text = "you passed: " + _toFinished + "%";
    }
}
