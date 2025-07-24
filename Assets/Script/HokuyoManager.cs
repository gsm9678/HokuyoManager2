using System.Collections.Generic;
using UnityEngine;
using HKY;

public class HokuyoManager : MonoBehaviour
{
    public List<URGSensorObjectDetector> m_senserData;
    [SerializeField] BoxManager m_boxManager;

    public List<HokuyoSetup> hokuyoSetup;

    [SerializeField]
    RectTransform Map;

    [SerializeField]
    GameObject HokuyoPointPrefab;

    [SerializeField]
    Transform HokuyoPointParent;

    public List<Transform> HokuyoPointObjects = new List<Transform>();

    public List<Vector3> HokuyoPointVectors = new List<Vector3>();

    private void Start()
    {
        //Map = GameObject.Find("Map").GetComponent<RectTransform>();
        //PointParent = GameObject.Find("Point").GetComponent<Transform>();
        //m_senserData.ip_address = hokuyoSetup.Hokuyo_IP_Adress;
        //m_senserData.enabled = true;
    }

    private void Update()
    {
        int j = 0;
        HokuyoPointVectors.Clear();

        for (int k = 0; k < m_senserData.Count; k++)
        {
            if (m_senserData[k].gameObject.activeSelf)
            {
                foreach (var dis in m_senserData[k].DirectedDistances)
                {
                    Vector3 vector = new Vector3(scale(-m_senserData[k].detectRectWidth / 2, m_senserData[k].detectRectWidth / 2, (-m_senserData[k].detectRectWidth / 200) * hokuyoSetup[k].Zoom_IN_OUT, (m_senserData[k].detectRectWidth / 200) * hokuyoSetup[k].Zoom_IN_OUT, dis.x),
                                                scale(0, m_senserData[k].detectRectHeight, 0 * hokuyoSetup[k].Zoom_IN_OUT, m_senserData[k].detectRectHeight / 100 * hokuyoSetup[k].Zoom_IN_OUT, dis.y),
                                                0);
                    if (vector.x == 0 && vector.y == 0)
                    {
                        continue;
                    }

                    Vector3 VectorZero = new Vector3(hokuyoSetup[k].X_Position, hokuyoSetup[k].Y_Position);
                    RotateAround(hokuyoSetup[k].Rotate_Camera_Value, Vector3.Distance(Vector3.zero, vector), VectorZero, ref vector);

                    if (vector.x < Map.rect.width / 2 + Map.localPosition.x &&
                        vector.x > -Map.rect.width / 2 + Map.localPosition.x &&
                        vector.y < Map.rect.height / 2 + Map.localPosition.y &&
                        vector.y > -Map.rect.height / 2 + Map.localPosition.y)
                    {
                        bool isinBox = false;
                        if (m_boxManager.objects.Count > 0)
                        {
                            foreach (RectTransform box in m_boxManager.objects)
                            {
                                if (vector.x < box.rect.width / 2 + box.localPosition.x &&
                                    vector.x > -box.rect.width / 2 + box.localPosition.x &&
                                    vector.y < box.rect.height / 2 + box.localPosition.y &&
                                    vector.y > -box.rect.height / 2 + box.localPosition.y)
                                {
                                    isinBox = true; break;
                                }
                            }
                        }

                        if(!isinBox)
                        {
                            HokuyoPointVectors.Add(vector);

                            if (Application.isFocused)
                            {
                                if (HokuyoPointObjects.Count > j)
                                {
                                    HokuyoPointObjects[j].localPosition = vector;
                                    if (!HokuyoPointObjects[j].gameObject.activeSelf)
                                        HokuyoPointObjects[j].gameObject.SetActive(true);
                                }
                                else
                                {
                                    HokuyoPointObjects.Add(Instantiate(HokuyoPointPrefab.transform, HokuyoPointParent));
                                    HokuyoPointObjects[j].localPosition = vector;
                                    if (!HokuyoPointObjects[j].gameObject.activeSelf)
                                        HokuyoPointObjects[j].gameObject.SetActive(true);
                                }
                                j++;
                            }
                            else
                            {
                                for (; j < HokuyoPointObjects.Count; j++)
                                {
                                    Destroy(HokuyoPointObjects[j].gameObject);
                                }
                                HokuyoPointObjects.Clear();
                            }
                        }
                    }
                }
            }
        }
        for(; j < HokuyoPointObjects.Count; j++)
        {
            if (HokuyoPointObjects[j].gameObject.activeSelf)
                HokuyoPointObjects[j].gameObject.SetActive(false);
            else
                break;
        }
    }

    public void Connect_Sensor(int i)
    {
        m_senserData[i].ip_address = hokuyoSetup[i].Hokuyo_IP_Adress;
        if (m_senserData[i].gameObject.activeSelf)
            m_senserData[i].EnableTCP();
        m_senserData[i].gameObject.SetActive(true);
    }
    
    public void Disconnect_Sensor(int i)
    {
        try
        {
            m_senserData[i].DisableTCP();
            m_senserData[i].gameObject.SetActive(false);
        }
        catch { }
    }

    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void RotateAround(float deg, float circleR, Vector3 zero, ref Vector3 tr)
    {
        Vector3 offset = Vector3.zero - tr;

        float deg1 = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        var rad = Mathf.Deg2Rad * (deg + deg1);
        var x = -circleR * Mathf.Sin(rad);
        var y = circleR * Mathf.Cos(rad);
        tr = zero + new Vector3(x, y);
    }
}
