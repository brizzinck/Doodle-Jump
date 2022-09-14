using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : Environments
{
    [SerializeField] private SpriteRenderer _enemySpriteRenderer;
    [SerializeField] private float _speed = 2f;
    
    private bool _moveWay = true;
    
    void Update()
    {
        if (_moveWay)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2, transform.position.y, -1), _speed * Time.deltaTime);
            _enemySpriteRenderer.flipX = false;
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-2, transform.position.y, -1), _speed * Time.deltaTime);
            _enemySpriteRenderer.flipX = true;
        }
           
        if (Mathf.Abs(transform.position.x) == 2 )
            _moveWay = !_moveWay;
    }
}
