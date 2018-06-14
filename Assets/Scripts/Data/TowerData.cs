using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Tower")]
public class TowerData : TDObject
{
    //Название вышки
    [Header("Название вышки")]
    [SerializeField] string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    //Тип вышки
    [Header ("Тип вышки")]
    [SerializeField] TowerType type;
    public TowerType Type
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

    //Цена покупки
    [Header("Цена покупки")]
    [SerializeField] float purchasePrice;
    public float PurchasePrice
    {
        get { return purchasePrice; }
        set { purchasePrice = value; }
    }

    //Цена продажи
    [Header("Цена продажи")]
    [SerializeField] float sellingPrice;
    public float SellingPrice
    {
        get { return sellingPrice; }
        set { sellingPrice = value; }
    }

    ////Тип атаки
    //[Header("Тип атаки")]
    //[SerializeField] AttackType attackType;
    //public AttackType AttackType
    //{
    //    get { return attackType; }
    //    set { attackType = value; }
    //}

    //Тип атаки
    [Header("Урон")]
    [SerializeField] float damageValue;
    public float DamageValue
    {
        get { return damageValue; }
        set { damageValue = value; }
    }

    //Радиус атаки
    [Header("Радиус атаки")]
    [SerializeField] float attackRange;
    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    //Скорость полета снаряда
    [Header("Скорость полета снаряда")]
    [SerializeField] float shellSpeed;
    public float ShellSpeed
    {
        get { return shellSpeed; }
        set { shellSpeed = value; }
    }

    ////Тип снаряда
    //[Header("Тип снаряда")]
    //[SerializeField] BulletController bullet;
    //public BulletController Bullet
    //{
    //    get { return bullet; }
    //    set { bullet = value; }
    //}

    //Частота выстрела
    [Header("Частота выстрела")]
    [SerializeField] float fireRate;
    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }
}

