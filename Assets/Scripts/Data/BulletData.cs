using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Bullet")]
public class BulletData : TDObject
{
    //Изображение снаряда
    [Header("Изображение снаряда")]
    [SerializeField] Sprite bulletSprite;
    public Sprite BulletSprite
    {
        get { return bulletSprite; }
        set { bulletSprite = value; }
    }
}
