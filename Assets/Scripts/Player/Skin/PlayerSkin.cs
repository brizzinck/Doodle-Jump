using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "Charecter", order = 51)]
public class PlayerSkin : ScriptableObject
{
    public Sprite IdelSprite;
    public Sprite JumpSprite;
    public Sprite ShotSprite;
    public Sprite ShotSpriteJump;
}
