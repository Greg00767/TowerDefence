using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Location")]
public class LocationData : TDObject
{
    [Header("Название локации")]
    [SerializeField] string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    [Header("Типы бъектов сцены")]
    [SerializeField] TypesData objectTypes;
    public TypesData ObjectTypes
    {
        get { return objectTypes; }
        set { objectTypes = value; }
    }

    [Header("Маршрут")]
    [SerializeField] Vector3[] wayPoints;
    public Vector3[] WayPoints
    {
        get { return wayPoints; }
        set { wayPoints = value; }
    }

    [Header("Волны")]
    [SerializeField] WaveData[] wavesData;
    public WaveData[] WavesData
    {
        get { return wavesData; }
        set { wavesData = value; }
    }

    [Header("Доступные вышки")]
    [SerializeField] TowerData[] towers;
    public TowerData[] Towers
    {
        get { return towers; }
        set { towers = value; }
    }
}
