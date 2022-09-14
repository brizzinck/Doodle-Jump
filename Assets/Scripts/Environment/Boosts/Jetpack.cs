using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jetpack : Boost
{
    [SerializeField] private AudioClip _clipSpring;
    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private Sprite[] _activeSprites;
    Player _player;
    bool _jump = true;
    bool _sound = false;
    private Vector3 _jetpackOffset = new Vector3(-.4f, -.4f, 0);
    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Player player))
        {
            Boost boost = player.CurrentBoost;
            if (boost != null && boost != this) return;
            Jump(player, _jumpForce);
            player.CurrentBoost = this;
        }
    }
    public void Jump(Player player, float jumpForce)
    {
        if(!_sound)
        {
            AudioController.Instance.PlaySound(_clipSpring);
            _sound = true;
        }
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _player = player;
        StartCoroutine("JetpackAnim");
        Invoke("Destroy", 2);
    }

    private void Update()
    {
        if(_player != null && _jump)
        {
            transform.position = _player.transform.position + _jetpackOffset;
        }
        if(_player != null)
        {
            if (_player.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX)
            {
                _jetpackOffset.x = .4f;
                transform.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                _jetpackOffset.x = -.4f;
                transform.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    IEnumerator JetpackAnim()
    {
        for (int i = 0; i < _activeSprites.Length; i ++)
        {
            transform.GetComponent<SpriteRenderer>().sprite = _activeSprites[i];
            if (i == _activeSprites.Length - 1)
                i = 0;
            yield return new WaitForSeconds(0.5f);
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
