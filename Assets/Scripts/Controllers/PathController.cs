using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathController : MonoBehaviour
{
    LocationData data;
    public LocationData Data
    {
        get { return data; }
        set { data = value; }
    }

    public Transform startPoint;
    public Transform finishPoint;

    [SerializeField] LineRenderer line;
    public LineRenderer Line
    {
        get { return line; }
        set { line = value; }
    }

    private void Start()
    {
        if (Line != null && Data != null)
        {
            Line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            Line.receiveShadows = false;
            Line.allowOcclusionWhenDynamic = false;
            Line.useWorldSpace = true;
            Line.positionCount = Data.WayPoints.Length;
            Line.SetPositions(Data.WayPoints);

            startPoint.position = Data.WayPoints[0];
            finishPoint.position = Data.WayPoints[Data.WayPoints.Length - 1];
        }
    }

    private void Update()
    {
        #if UNITY_EDITOR
            if (Line != null && Data != null)
            {
                Line.positionCount = Data.WayPoints.Length;
                Line.SetPositions(Data.WayPoints);

                startPoint.position = Data.WayPoints[0];
                finishPoint.position = Data.WayPoints[Data.WayPoints.Length - 1];
            }
        #endif
    }
}
