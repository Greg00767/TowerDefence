using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LocationController : MonoBehaviour
{
    [SerializeField] int index;
    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    [SerializeField] LocationData data;
    public LocationData Data
    {
        get { return data; }
        set { data = value; }
    }
}