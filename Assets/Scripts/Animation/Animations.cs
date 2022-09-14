using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Animations
{
    public static void DoFade(CanvasGroup image, int fade, float speed = 0.6f)
    {
        image.DOFade(fade, speed);
    }
    public static void DoScale(Transform image, Vector2 scale, float speed = 0.6f)
    {
        image.DOScale(scale, speed);
    }
    public static void DoRotate(Transform transform, Vector3 rotate, float speed = 0.6f)
    {
        transform.DORotate(rotate, speed);
    }
    public static void DoMove(Transform transform, Vector3 move, float speed = 0.6f)
    {
        transform.DOLocalMove(move, speed);       
    }
    public static void ControllRaycast(CanvasGroup obj, bool ignore)
    {
        if (ignore || obj.blocksRaycasts)
        {
            obj.blocksRaycasts = false;
        }
        else
        {
            obj.blocksRaycasts = true;
        }
    }
}
