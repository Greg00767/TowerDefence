using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class TDObject : ScriptableObject
{
    public static int Id { get; set; }

    public TDObject()
    {
        ++Id;
    }
}
