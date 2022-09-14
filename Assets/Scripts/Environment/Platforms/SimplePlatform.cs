using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatform : Environments
{
    [SerializeField] private float _jumpForce = 6.5f;
    [SerializeField] private bool _isJump = true;

    public bool IsJump { get => _isJump; }

    public override void Jump(Player player, float jumpForce, float jumpAnim = 0.2f)
    {
        base.Jump(player, jumpForce, jumpAnim);
    }
    public void SetJump(bool jump)
    {
        _isJump = jump;
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
}
