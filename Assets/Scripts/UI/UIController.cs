using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _playEndeless;
    [SerializeField] private Button _playLevels;
    [SerializeField] private Button[] _shops;
    [SerializeField] private Button _score;
    [SerializeField] private CanvasGroup _shopCanvas;
    [SerializeField] private CanvasGroup _scorePanel;
    [SerializeField] private Text[] _scoreTXT;
    [SerializeField] private Text _coinsTXT;
    [SerializeField] private SimplePlatform _simplePlatform;
    [SerializeField] private StoreController _storeController;
    [SerializeField] private SkinPlayerManager _skinPlayerManager;

    private void Start()
    {
        AddButton();
        SetValue();
    }

    private void SetValue()
    {
        _scoreTXT[0].text = "high score: " + Saver.GetIntPrefs("HighScore").ToString();
        _scoreTXT[1].text = "last score: " + Saver.GetIntPrefs("LastScore").ToString();
        _coinsTXT.text = Saver.GetIntPrefs("Coins").ToString();
    }

    private void AddButton()
    {
        _playEndeless.onClick.AddListener(StartGameEndeless);
        _playLevels.onClick.AddListener(StartGameLeveles);
        _shops[0].onClick.AddListener(ShopController);
        _shops[1].onClick.AddListener(ShopController);
        _score.onClick.AddListener(ScoreController);
    }

    private void StartGameEndeless()
    {
        AudioController.Instance.PlayButtons();
        SceneManager.LoadScene("GameScene");
    }
    private void StartGameLeveles()
    {
        AudioController.Instance.PlayButtons();
        SceneManager.LoadScene("GameLevelsScene");
    }
    private void ShopController()
    {
        AudioController.Instance.PlayButtons();
        ControllRaycastsCanvasGroup(_shopCanvas);
        if (!_shopCanvas.blocksRaycasts)
        {
            _simplePlatform.SetJump(true);
            _skinPlayerManager.SetSkin();
        }
        else if (_shopCanvas.blocksRaycasts)
        {
            _simplePlatform.SetJump(false);
            _storeController.SetCurrentSkin();
        }
    }
    private void ScoreController()
    {
        AudioController.Instance.PlayButtons();
        ControllRaycastsCanvasGroup(_scorePanel);
        ControllScorePanel(_scorePanel);
    }
    private void ControllRaycastsCanvasGroup(CanvasGroup canvasGroup)
    {
        if (canvasGroup.blocksRaycasts)
        {
            Animations.DoFade(canvasGroup, 0);
            Animations.ControllRaycast(canvasGroup, false);
        }
        else
        {
            Animations.DoFade(canvasGroup, 1);
            Animations.ControllRaycast(canvasGroup, false);
        }
    }
    private void ControllScorePanel(CanvasGroup canvasGroup)
    {
        if (canvasGroup.blocksRaycasts)
        {
            Animations.DoMove(canvasGroup.transform, new Vector3(-88, 120, 0));
            Animations.DoScale(canvasGroup.transform, new Vector3(1, 1, 1));
        }
        else
        {
            Animations.DoMove(canvasGroup.transform, new Vector3(0, 0, 0));
            Animations.DoScale(canvasGroup.transform, new Vector3(0, 0, 0));
        }
    }
}
