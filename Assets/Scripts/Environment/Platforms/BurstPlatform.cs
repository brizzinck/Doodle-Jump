using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPlatform : Environments
{
    [SerializeField] private AudioClip _clipExplosion;
    [SerializeField] private float _jumpForce = 6.5f;
    [SerializeField] private bool _isJump = true;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animSpeed = 1;
    private int _spriteIndex = 0;

    public bool IsJump { get => _isJump; }

    public override void Jump(Player player, float jumpForce, float jumpAnim = 0.2f)
    {
        base.Jump(player, jumpForce, jumpAnim);      
    }
    public void SetJump()
    {
        _isJump = !_isJump;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player) && _isJump)
        {
            float offset = collision.transform.localPosition.y - transform.localPosition.y;
            if (collision.relativeVelocity.y >= 0 || Mathf.Abs(offset) < _contactOffsetY) return;
            Jump(player, _jumpForce);
        }
    }

    private void Start()
    {
        Invoke("ChangeSprite", _animSpeed);
    }

    private void ChangeSprite()
    {
        transform.GetComponent<SpriteRenderer>().sprite = _sprites[_spriteIndex];
        _spriteIndex += 1;
        if(_spriteIndex > 3)
        {
            //AudioController.Instance.PlaySound(_clipExplosion);
            gameObject.SetActive(false);
        }
        else
            Invoke("ChangeSprite", _animSpeed);
    }
}
