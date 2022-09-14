using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trampoline : Boost
{
    [SerializeField] private AudioClip _clipSpring;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private Sprite _activeSpring;
    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Player player))
        {
            float offset = collider2D.transform.localPosition.y - transform.localPosition.y;
            if (player.transform.GetComponent<Rigidbody2D>().velocity.y > 0 || Mathf.Abs(offset) < _contactOffsetY) return;
            Jump(player, _jumpForce, 0.5f);
        }
    }
    public override void Jump(Player player, float jumpForce, float jumpAnim = 0.2f)
    {
        base.Jump(player, jumpForce, jumpAnim);
        transform.GetComponent<SpriteRenderer>().sprite = _activeSpring;
        transform.position += new Vector3(0, .1f, 0);
        player.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360) ;
    }
    private void Start()
    {
        _contactOffsetY = .60f;
    }
}
