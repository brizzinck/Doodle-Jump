using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPlayerManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerSkin[] _playerSkins;
    private void Start()
    {
        SetSkin();
    }

    public void SetSkin()
    {
        _player.SetSprites(_playerSkins[Saver.GetIntPrefs("CurrentSkin")]);
    }
}
