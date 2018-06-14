using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Enemy")]
public class EnemyData : TDObject
{
    //Название врага
    [Header("Название врага")]
    [SerializeField] string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    //Тип врага
    [Header("Тип врага")]
    [SerializeField] EnemyType type;
    public EnemyType Type
    {
        get { return type; }
        set { type = value; }
    }

    //Спрайт
    [Header("Спрайт")]
    [SerializeField] Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    //Броня
    [Header("Броня")]
    [SerializeField] float armor;
    public float Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    //Жизни
    [Header("Жизни")]
    [SerializeField] float health;
    public float Health
    {
        get { return health; }
        set
        {
            if (value < 0)
                health = 0;
            else
                health = value;
        }
    }

    //Цена за убийство
    [Header("Цена за убийство")]
    [SerializeField] float killprice;
    public float KillPrice
    {
        get { return killprice; }
        set { killprice = value; }
    }

    //Урон по игроку по достижении конца пути
    [Header("Урон по игроку")]
    [SerializeField] float playerDamage;
    public float PlayerDamage
    {
        get { return playerDamage; }
        set { playerDamage = value; }
    }

    //Скорость движения
    [Header("Скорость движения")]
    [SerializeField] float moveSpeed = 1.0f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
}