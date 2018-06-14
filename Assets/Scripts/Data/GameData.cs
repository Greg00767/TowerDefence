using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/Game parameters")]
public class GameData : TDObject
{
    [Header("Название игры")]
    [SerializeField] string gameName;

    [Header ("Локации")]
    [SerializeField] LocationData[] locations;

    public string GameName
    {
        get
        {
            return gameName;
        }

        set
        {
            gameName = value;
        }
    }

    public LocationData[] Locations
    {
        get
        {
            return locations;
        }

        set
        {
            locations = value;
        }
    }
}
