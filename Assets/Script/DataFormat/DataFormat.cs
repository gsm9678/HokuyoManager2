using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // Á÷·ÄÈ­
public class DataFormat
{
    public float Zoom_InOut_Value;
    public float X_Position_Value;
    public float Y_Position_Value;
    public float Rotate_Camera_Value;
    public float X_Size_Value;
    public float Y_Size_Value;
    public float Point_Scale_Value;
    public float Max_Scale_Value;
    public float Min_Scale_Value;
    public string IP_Adress;
    public string OSC_IP_Adress;
    public string OSC_Adress;

    public List<BoxData> BoxData = new List<BoxData>();

    public List<HokuyoSetup> hokuyoSetups = new List<HokuyoSetup>();
}

[Serializable]
public class HokuyoSetup
{
    public string Hokuyo_IP_Adress;
    public float Zoom_IN_OUT;
    public float X_Position;
    public float Y_Position;
    public float Rotate_Camera_Value;

    public HokuyoSetup()
    {
        Hokuyo_IP_Adress = "192.168.0.10";
        Zoom_IN_OUT = 1;
        X_Position = 0;
        Y_Position = 0;
        Rotate_Camera_Value = 0;
    }
}

[Serializable]
public class BoxData
{
    public string Name;
    public float X_Position_Value;
    public float Y_Position_Value;
    public float X_Size_Value;
    public float Y_Size_Value;
}

[Serializable]
public class DetectedObjectData
{
    public float Right, Left, Top, Bottom;

    public DetectedObjectData(Vector3 vector3, float Scale)
    {
        Right = Left = vector3.x;
        Top = Bottom = vector3.y;

        if (Right < vector3.x + Scale / 2)
            Right = vector3.x + Scale / 2;
        if (Left > vector3.x - Scale / 2)
            Left = vector3.x - Scale / 2;
        if (Top < vector3.y + Scale / 2)
            Top = vector3.y + Scale / 2;
        if (Bottom > vector3.y - Scale / 2)
            Bottom = vector3.y - Scale / 2;
    }

    public void setData(Vector3 vector3, float Scale)
    {
        if (Right < vector3.x + Scale/ 2)
            Right = vector3.x + Scale / 2;
        if (Left > vector3.x - Scale/ 2)
            Left = vector3.x - Scale / 2;
        if (Top < vector3.y + Scale / 2)
            Top = vector3.y + Scale / 2;
        if (Bottom > vector3.y - Scale / 2)
            Bottom = vector3.y - Scale / 2;
    }

    public Vector3 getCenter()
    {
        return new Vector3((Right + Left) / 2, (Top + Bottom) / 2, 0);
    }

    public Vector3 getSize()
    {
        return new Vector3(Mathf.Abs(Right) - Mathf.Abs(Left), Mathf.Abs(Top) - Mathf.Abs(Bottom), 0);
    }
}

public class PointTrackingData
{
    public Vector3 position;
    public float PauseTime;
    public bool state = false;
}