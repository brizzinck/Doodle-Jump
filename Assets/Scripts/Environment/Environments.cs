using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environments : MonoBehaviour
{
    [SerializeField] protected AudioClip _clip;
    protected float _contactOffsetY = .64f;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public virtual void Jump(Player player, float jumpForce, float jumpAnim = 0.2f)
    {
        AudioController.Instance.PlaySound(_clip);
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, 0, 0);
        rigidbody2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        if (jumpAnim >= 0) StartCoroutine(player.FullJumpAnim(jumpAnim));
    }
}
