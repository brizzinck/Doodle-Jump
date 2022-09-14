using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private SimplePlatform[] _simplePlatforms;
    [SerializeField] private Button _buy;
    [SerializeField] private Text _costSkin;
    [SerializeField] private Text _nameSkin;
    [SerializeField] private Text _buyOrPlay;
    [SerializeField] private PlayerUI[] _players;
    [SerializeField] private Text _coinsDisplay;
    private PlayerUI _player;
    private List<int> _idPlayers;

    public void StoreAnimation(SimplePlatform platform)
    {
        foreach (SimplePlatform simplePlatform in _simplePlatforms)
        {
            simplePlatform.SetJump(false);
            if (platform.gameObject.GetInstanceID() == simplePlatform.gameObject.GetInstanceID()) simplePlatform.SetJump(true);
        }
    }
    public void SetCurrentSkin()
    {
        foreach (PlayerUI playerUI in _players)
        {
            if (playerUI.SkinID == Saver.GetIntPrefs("CurrentSkin"))
            {
                playerUI.SimplePlatform.SetJump(true);
                playerUI.Jump();
                SetPlayer(playerUI);
            }
        }
    }
    public void SetPlayer(PlayerUI playerUI)
    {
        _player = playerUI;
        if (Saver.GetArraySkinsId().Contains(_player.SkinID))
        {
            _costSkin.text = "";
            _nameSkin.text = _player.SkinName;
            _buyOrPlay.text = "play";
        }
        else
        {
            _costSkin.text = _player.SkinCost.ToString();
            _nameSkin.text = _player.SkinName;
            _buyOrPlay.text = "buy";
        }
    }
    public void BuySkin()
    {
        AudioController.Instance.PlayButtons();
        if (Saver.GetIntPrefs("Coins") >= _player.SkinCost && !Saver.GetArraySkinsId().Contains(_player.SkinID))
        {
            Saver.SaveIntPrefs("Coins", Saver.GetIntPrefs("Coins") - _player.SkinCost);
            _idPlayers.Add(_player.SkinID);
            Saver.SetArraySkinsId(_idPlayers);
            _coinsDisplay.text = Saver.GetIntPrefs("Coins").ToString();
            SetPlayer(_player);
            Saver.SaveIntPrefs("CurrentSkin", _player.SkinID);
        }       
        else if (Saver.GetArraySkinsId().Contains(_player.SkinID)) Saver.SaveIntPrefs("CurrentSkin", _player.SkinID);
    }
    private void Start()
    {
        _buy.onClick.AddListener(BuySkin);
        _idPlayers = Saver.GetArraySkinsId();
    }
}
