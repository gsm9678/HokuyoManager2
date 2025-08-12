using System;
using System.Collections.Generic;
using UnityEngine;

public class DBSCANPoint
{
    public Vector2 Position;
    public int ClusterId = -1;
    public bool Visited = false;
}

[Serializable]
public class DBSCAN_Output
{
    public List<DBSCANPoint> points = new List<DBSCANPoint>();
    public List<Vector2> Centroids = new List<Vector2>();
}

#region RoomSettingData
[Serializable] // 직렬화
public class DataFormat
{
    public RoomSizeDataModel RoomSizeData;
    public ScaleSizeDataModel ScaleSizeData;
    public OSCSettingModel OSCSetting;

    public List<SensorSettingModel> SensorSettingModels = new List<SensorSettingModel>();
    public List<NeglectAreaModel> NeglectAreas = new List<NeglectAreaModel>();
}

[Serializable]
public class RoomSizeDataModel //방 사이즈 데이터
{
    public float X_Size_Value;
    public float Y_Size_Value;
}

[Serializable]
public class ScaleSizeDataModel
{
    public float Epsilon;
    public float Min_Point;
}

[Serializable]
public class FlipDataModel
{
    public bool X_Flip;
    public bool Y_Flip;
}

[Serializable]
public class OSCSettingModel
{
    public string OSC_IP_Address;
    public int OSC_Port;
    public string OSC_Message_Address;

    public OSCSettingModel()
    {
        OSC_IP_Address = "127.0.0.1";
        OSC_Port = 7000;
        OSC_Message_Address = "Sensor";
    }
}

[Serializable]
public class SensorSettingModel
{
    public string Hokuyo_IP_Address;
    public float Zoom_IN_OUT;
    public float X_Position;
    public float Y_Position;
    public float Rotate_Camera_Value;
    public bool X_Flip;
    public bool Y_Flip;

    public SensorSettingModel()
    {
        Hokuyo_IP_Address = "192.168.0.10";
        Zoom_IN_OUT = 1;
        X_Position = 0;
        Y_Position = 0;
        Rotate_Camera_Value = 0;
    }
}

[Serializable]
public class NeglectAreaModel
{
    public float X_Position_Value;
    public float Y_Position_Value;
    public float X_Size_Value;
    public float Y_Size_Value;

    public NeglectAreaModel()
    {
        X_Position_Value = 0;
        Y_Position_Value = 0;
        X_Size_Value = 10;
        Y_Size_Value = 10;
    }
}
#endregion