using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower Defense Objects/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] float value;
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
