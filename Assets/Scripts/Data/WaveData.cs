using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Wave")]
public class WaveData : TDObject
{
    //Номер локации
    [Header("Номер локации")]
    [SerializeField] int locationNumber;
    public int LocationNumber
    {
        get { return locationNumber; }
        set { locationNumber = value; }
    }

    //Наименование волны
    [Header("Наименование волны")]
    [SerializeField] string waveName = "Wave";
    public string WaveName
    {
        get { return waveName; }
        set { waveName = value; }
    }

    //Минимальное количество врагов в одной волне
    readonly int minEnemyCount = 1;
    public int MinEnemyCount
    {
        get { return minEnemyCount; }
    }

    //Максимальное количество врагов в одной волне
    readonly int maxEnemyCount = 100;
    public int MaxEnemyCount
    {
        get { return maxEnemyCount; }
    }

    //Количество врагов
    [Header("Количество врагов")]
    [SerializeField] int enemyCount = 10;
    public int EnemyCount
    {
        get { return enemyCount; }
        set
        {
            if (value > MaxEnemyCount)
                enemyCount = MaxEnemyCount;
            else if (value < MinEnemyCount)
                enemyCount = MinEnemyCount;
            else
                enemyCount = value;
        }
    }

    //Минимальное значение пропорции типов врагов
    int minEnemyProportions = 0;
    public int MinEnemyProportions
    {
        get { return minEnemyProportions; }
    }

    //Максимальное значение пропорции типов врагов
    int maxEnemyProportions = 100;
    public int MaxEnemyProportions
    {
        get { return maxEnemyProportions; }
    }

    //Пропорции типов врагов
    [Header("Пропорции типов врагов")]
    [SerializeField] int enemyTypeProportions = 50;
    public int EnemyTypeProportions
    {
        get { return enemyTypeProportions; }
        set
        {
            if (value > MaxEnemyProportions)
                enemyCount = MaxEnemyProportions;
            else if (value < MinEnemyProportions)
                enemyCount = MinEnemyProportions;
            else
                enemyCount = value;
        }
    }

    //Задержка перед запуском волны
    [Header("Задержка перед запуском волны")]
    [SerializeField] float startDelay;
    public float StartDelay
    {
        get { return startDelay; }
        set { startDelay = value; }
    }

    //Задержка между противниками
    [Header("Задержка между противниками")]
    [SerializeField] float enemyDelay;
    public float EnemyDelay
    {
        get { return enemyDelay; }
        set { enemyDelay = value; }
    }
}
