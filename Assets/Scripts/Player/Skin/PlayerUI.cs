using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private StoreController _storeController;
    [SerializeField] private int _skinID;
    [SerializeField] private int _skinCost;
    [SerializeField] private string _skinName;
    [SerializeField] private Player _player;
    [SerializeField] private SimplePlatform _simplePlatform;
    [SerializeField] private float _jumpForce = 5;

    public int SkinID { get => _skinID; }
    public int SkinCost { get => _skinCost; }
    public string SkinName { get => _skinName; }
    public SimplePlatform SimplePlatform { get => _simplePlatform; }

    public void ChoosePlayer()
    {
        Jump();
        _storeController.SetPlayer(this);
    }
    public void Jump()
    {
        if (_simplePlatform.IsJump && _player.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            _simplePlatform.Jump(_player, _jumpForce);
        }
    }
}
