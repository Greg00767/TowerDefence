using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] int value;
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
