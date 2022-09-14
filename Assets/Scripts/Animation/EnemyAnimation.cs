using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private void Start()
    {
        Idle();
    }

    private void Idle()
    {
        DOTween.Sequence().
            Append(transform.DOScale(0.9f, 1f)).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).
            Append(transform.DOScale(1f,1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear));
    }
}
