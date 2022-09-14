using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Boost
{
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private Sprite _activeSpring;
    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Player player))
        {
            float offset = collider2D.transform.localPosition.y - transform.localPosition.y;
            if (player.transform.GetComponent<Rigidbody2D>().velocity.y > 0 || Mathf.Abs(offset) < _contactOffsetY) return;
            Jump(player, _jumpForce, 0.2f);
        }
    }
    public override void Jump(Player player, float jumpForce, float jumpAnim = 0.2f)
    {
        base.Jump(player, jumpForce, jumpAnim);
        transform.GetComponent<SpriteRenderer>().sprite = _activeSpring;
        transform.position += new Vector3(0, .2f, 0);
    }
    private void Start()
    {
        _contactOffsetY = .60f;
    }
}
