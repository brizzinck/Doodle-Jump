using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hat : Boost
{
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private Sprite[] _activeSprites;
    Player _player;
    bool _jump = true;
    bool _sound = false;
    private Vector3 _hatOffset = new Vector3(0, .4f, 0);
    public override void Jump(Player player, float jumpForce, float jumpAnim)
    {
        if (!_sound)
        {
            AudioController.Instance.PlaySound(_clip);
            _sound = true;
        }
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _player = player;
        StartCoroutine("HatAnim");
        Invoke("Destroy", 2);
    }
    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Player player))
        {
            Boost boost = player.CurrentBoost;
            if (boost != null && boost != this) return;
            Jump(player, _jumpForce, 0.5f);
            player.CurrentBoost = this;
        }
    }
    private void Update()
    {
        if (_player != null && _jump)
        {
            transform.position = _player.transform.position + _hatOffset + Vector3.back * 3;
        }
    }
    private IEnumerator HatAnim()
    {
        for (int i = 0; i < _activeSprites.Length; i++)
        {
            transform.GetComponent<SpriteRenderer>().sprite = _activeSprites[i];
            if (i == _activeSprites.Length - 1)
                i = 0;
            yield return new WaitForSeconds(0.2f);
        }

    }

    private void Destroy()
    {
        _jump = false;
        transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30);
        transform.DORotate(new Vector3(0, 0, 90), 1, RotateMode.FastBeyond360);
        _player.CurrentBoost = null;
    }
}
