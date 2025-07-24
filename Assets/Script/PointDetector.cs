using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDetector : MonoBehaviour
{
    [SerializeField] HokuyoManager hokuyoManager;
    [SerializeField] OSCManager OSCmanager;

    [SerializeField] Transform tr_DetectedPointBusket;
    [SerializeField] GameObject DetectedPointPrefebs;

    [SerializeField] Slider Point_Scale;
    [SerializeField] Slider Max_Scale;
    [SerializeField] Slider Min_Scale;

    List<Vector3> HokuyoPointVectors;
    List<Transform> HokuyoPointObjects;
    List<Transform> DetectedObjectPoints = new List<Transform>();
    
    [HideInInspector] public List<Vector3> InScaleDetectedObjectPoints = new List<Vector3>();

    private void Start()
    {
        HokuyoPointVectors = hokuyoManager.HokuyoPointVectors;
        HokuyoPointObjects = hokuyoManager.HokuyoPointObjects;
    }

    private void Update()
    {
        for(int i = 0; i < HokuyoPointObjects.Count; i++)
        {
            HokuyoPointObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(Point_Scale.value, Point_Scale.value);
        }

        List<DetectedObjectData> detectedObjectDatas = new List<DetectedObjectData>();

        foreach (var vector3 in HokuyoPointVectors)
        {
            if (detectedObjectDatas.Count == 0)
            {
                detectedObjectDatas.Add(new DetectedObjectData(vector3, Point_Scale.value));
            }

            for (int j = 0; j < detectedObjectDatas.Count; j++)
            {
                if ((vector3.x + Point_Scale.value / 2 > detectedObjectDatas[j].Left && vector3.y + Point_Scale.value / 2 > detectedObjectDatas[j].Bottom && vector3.y + Point_Scale.value / 2 < detectedObjectDatas[j].Top) ||
                    (vector3.x + Point_Scale.value / 2 > detectedObjectDatas[j].Left && vector3.y - Point_Scale.value / 2 > detectedObjectDatas[j].Bottom && vector3.y - Point_Scale.value / 2 < detectedObjectDatas[j].Top))
                {
                    //Debug.Log("A");
                    detectedObjectDatas[j].setData(vector3, Point_Scale.value);
                    break;
                }
                if (j == detectedObjectDatas.Count - 1)
                {
                    detectedObjectDatas.Add(new DetectedObjectData(vector3, Point_Scale.value));
                    break;
                }
            }
        }

        for (int i = DetectedObjectPoints.Count; i < detectedObjectDatas.Count; i++)
        {
            DetectedObjectPoints.Add(Instantiate(DetectedPointPrefebs.transform, detectedObjectDatas[i].getCenter(), this.transform.rotation, tr_DetectedPointBusket));
        }

        InScaleDetectedObjectPoints.Clear();
        OSCmanager.StartMessage(tr_DetectedPointBusket.GetComponent<RectTransform>().rect.size);

        for (int i = 0; i < DetectedObjectPoints.Count; i++)
        {
            if (detectedObjectDatas.Count > i)
            {
                if (Mathf.Abs(detectedObjectDatas[i].getSize().x) > Min_Scale.value && Mathf.Abs(detectedObjectDatas[i].getSize().x) < Max_Scale.value)
                {
                    DetectedObjectPoints[i].gameObject.SetActive(true);
                    DetectedObjectPoints[i].localPosition = detectedObjectDatas[i].getCenter();
                    InScaleDetectedObjectPoints.Add(DetectedObjectPoints[i].localPosition);
                    OSCmanager.SensorMessage(DetectedObjectPoints[i].transform.localPosition);
                }
                else if (Mathf.Abs(detectedObjectDatas[i].getSize().y) > Min_Scale.value && Mathf.Abs(detectedObjectDatas[i].getSize().y) < Max_Scale.value)
                {
                    DetectedObjectPoints[i].gameObject.SetActive(true);
                    DetectedObjectPoints[i].localPosition = detectedObjectDatas[i].getCenter();
                    InScaleDetectedObjectPoints.Add(DetectedObjectPoints[i].localPosition);
                    OSCmanager.SensorMessage(DetectedObjectPoints[i].localPosition);
                }
                else
                {
                    DetectedObjectPoints[i].gameObject.SetActive(false);
                }
            }
            else
            {   
                DetectedObjectPoints[i].gameObject.SetActive(false);
            }
        }
    }
}
