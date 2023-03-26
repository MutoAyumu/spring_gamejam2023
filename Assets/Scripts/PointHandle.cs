using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHandle : MonoBehaviour
{
    [HideInInspector] public StartEndPoint[] MovePointArray = new StartEndPoint[1];
}

[System.Serializable]
public class StartEndPoint
{
    public StartEndPoint(Vector3 start, Vector3 end)
    {
        Start = start;
        End = end;
    }
    public StartEndPoint() { }

    public Vector3 Start;
    public Vector3 End;
}