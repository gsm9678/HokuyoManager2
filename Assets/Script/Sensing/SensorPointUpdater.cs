using HKY;
using System.Collections.Generic;
using UnityEngine;

public class SensorPointUpdater : MonoBehaviour
{
    [SerializeField] SensorManager sensorManager;
    ClusterManager clusterManager;

    RoomSizeDataModel roomSizeDataModel;
    List<SensorSettingModel> sensorSettingModels;
    List<NeglectAreaModel> neglectAreaModels;

    public List<URGSensorObjectDetector> m_senserData;
    public List<Vector2> SensorPointVectors = new List<Vector2>();

    private void Start()
    {
        clusterManager= new ClusterManager();

        m_senserData = sensorManager.SensorDatas;

        sensorSettingModels = GameManager.instance.data.SensorSettingModels;
        roomSizeDataModel = GameManager.instance.data.RoomSizeData;
        neglectAreaModels = GameManager.instance.data.NeglectAreas;
    }

    private void Update()
    {
        SensorPointVectors.Clear();

        for (int i = 0; i < m_senserData.Count; i++)
        {
            if (m_senserData[i].isConnected())
            {
                foreach (var dis in m_senserData[i].DirectedDistances)
                {
                    Vector2 vector = new Vector3(scale(-m_senserData[i].detectRectWidth / 2, m_senserData[i].detectRectWidth / 2, (-m_senserData[i].detectRectWidth / 200) * sensorSettingModels[i].Zoom_IN_OUT, (m_senserData[i].detectRectWidth / 200) * sensorSettingModels[i].Zoom_IN_OUT, dis.x),
                                                scale(0, m_senserData[i].detectRectHeight, 0 * sensorSettingModels[i].Zoom_IN_OUT, m_senserData[i].detectRectHeight / 100 * sensorSettingModels[i].Zoom_IN_OUT, dis.y),
                                                0);
                    if (vector.x == 0 && vector.y == 0)
                    {
                        continue;
                    }

                    if (sensorSettingModels[i].X_Flip)
                        vector *= new Vector2(-1, 1);
                    if (sensorSettingModels[i].Y_Flip)
                        vector *= new Vector2(1, -1);

                    Vector2 VectorZero = new Vector2(sensorSettingModels[i].X_Position, sensorSettingModels[i].Y_Position);
                    RotateAround(sensorSettingModels[i].Rotate_Camera_Value, Vector2.Distance(Vector2.zero, vector), VectorZero, ref vector);

                    if (vector.x < roomSizeDataModel.X_Size_Value / 2 &&
                        vector.x > -roomSizeDataModel.X_Size_Value / 2 &&
                        vector.y < roomSizeDataModel.Y_Size_Value / 2 &&
                        vector.y > -roomSizeDataModel.Y_Size_Value / 2)
                    {
                        if (neglectAreaModels.Count > 0)
                        {
                            bool is_Area = false;
                            foreach (NeglectAreaModel area in neglectAreaModels)
                            {
                                if (vector.x < area.X_Size_Value / 2 + area.X_Position_Value &&
                                    vector.x > -area.X_Size_Value / 2 + area.X_Position_Value &&
                                    vector.y < area.Y_Size_Value / 2 + area.Y_Position_Value &&
                                    vector.y > -area.Y_Size_Value / 2 + area.Y_Position_Value)
                                {
                                    is_Area = true; break;
                                }
                            }
                            if (is_Area)
                                continue;
                        }

                        SensorPointVectors.Add(vector);
                    }
                }
            }
        }
        clusterManager.StartCluster(SensorPointVectors);
    }

    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void RotateAround(float deg, float circleR, Vector2 zero, ref Vector2 tr)
    {
        Vector2 offset = Vector2.zero - tr;

        float deg1 = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        var rad = Mathf.Deg2Rad * (deg + deg1);
        var x = -circleR * Mathf.Sin(rad);
        var y = circleR * Mathf.Cos(rad);
        tr = zero + new Vector2(x, y);
    }
}
