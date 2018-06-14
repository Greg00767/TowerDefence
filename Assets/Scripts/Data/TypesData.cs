using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Types")]
public class TypesData : TDObject
{
    [Header("Типы врагов")]
    [SerializeField] EnemyData[] enemyTypes;
    public EnemyData[] EnemyTypes
    {
        get { return enemyTypes; }
        set { enemyTypes = value; }
    }

    [Header("Типы башен")]
    [SerializeField] TowerData[] towerTypes;
    public TowerData[] TowerTypes
    {
        get { return towerTypes; }
        set { towerTypes = value; }
    }

    [Header("Типы снарядов")]
    [SerializeField] BulletData[] bulletTypes;
    public BulletData[] BulletTypes
    {
        get { return bulletTypes; }
        set { bulletTypes = value; }
    }
}
