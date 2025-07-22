using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDetection : MonoBehaviour
{
    private HokuyoManager hokuyoManager;
    private OSCManager OSCmanager;

    [SerializeField] GameObject Point_Ob;
    [SerializeField] GameObject ObjectPoint;

    [SerializeField] Slider Point_Scale;
    [SerializeField] Slider Max_Scale;
    [SerializeField] Slider Min_Scale;

    List<GameObject> gizmos_Images = new List<GameObject>();

    public List<GameObject> DetectedObjectPoints = new List<GameObject>();

    private void Start()
    {
        hokuyoManager = GameObject.Find("HokuyoManager").GetComponent<HokuyoManager>();
        OSCmanager = GameObject.Find("OSCManager").GetComponent<OSCManager>();
        StartCoroutine("CheckPointDetect");
    }

    IEnumerator CheckPointDetect()
    {
        yield return new WaitUntil(() => hokuyoManager.gizmos_Images != null);
        gizmos_Images = hokuyoManager.gizmos_Images;

        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (gizmos_Images[0] != Point_Scale)
                for(int i = 0; i < gizmos_Images.Count; i++)
                {
                    gizmos_Images[i].GetComponent<RectTransform>().sizeDelta= new Vector2(Point_Scale.value, Point_Scale.value);
                }

            List<DetectedObjectData> detectedObjects = new List<DetectedObjectData>();

            for (int i = 0; i < gizmos_Images.Count; i++)
            {
                if (gizmos_Images[i].activeSelf)
                {
                    Vector3 vector3 = gizmos_Images[i].transform.position;

                    if (detectedObjects.Count == 0)
                    {
                        detectedObjects.Add(new DetectedObjectData(vector3, Point_Scale.value));
                    }
                    else
                    {
                        for(int j = 0; j < detectedObjects.Count; j++)
                        {
                            if ((vector3.x + Point_Scale.value / 2 > detectedObjects[j].Left && vector3.y + Point_Scale.value / 2 > detectedObjects[j].Bottom && vector3.y + Point_Scale.value / 2 < detectedObjects[j].Top) ||
                                (vector3.x + Point_Scale.value / 2 > detectedObjects[j].Left && vector3.y - Point_Scale.value / 2 > detectedObjects[j].Bottom && vector3.y - Point_Scale.value / 2 < detectedObjects[j].Top))
                            {
                                //Debug.Log("A");
                                detectedObjects[j].setData(vector3, Point_Scale.value);
                                break;
                            }
                            if (j == detectedObjects.Count - 1)
                            {
                                detectedObjects.Add(new DetectedObjectData(vector3, Point_Scale.value));
                                break;
                            }
                        }
                    }
                }
            }

            for (int i = DetectedObjectPoints.Count;  i < detectedObjects.Count; i++)
            {
                DetectedObjectPoints.Add(Instantiate(ObjectPoint, detectedObjects[i].getCenter(), this.transform.rotation, Point_Ob.transform));
            }

            OSCmanager.StartMessage(Point_Ob.GetComponent<RectTransform>().rect.size);

            for (int i = 0; i < DetectedObjectPoints.Count; i++)
            {
                if (detectedObjects.Count > i)
                {
                    if (detectedObjects[i].getSize().x > Min_Scale.value && detectedObjects[i].getSize().x < Max_Scale.value)
                    {
                        DetectedObjectPoints[i].SetActive(true);
                        DetectedObjectPoints[i].transform.position = detectedObjects[i].getCenter();
                        OSCmanager.SensorMessage(DetectedObjectPoints[i].transform.localPosition);
                    }
                    else if (detectedObjects[i].getSize().y > Min_Scale.value && detectedObjects[i].getSize().y < Max_Scale.value)
                    {
                        DetectedObjectPoints[i].SetActive(true);
                        DetectedObjectPoints[i].transform.position = detectedObjects[i].getCenter();
                        OSCmanager.SensorMessage(DetectedObjectPoints[i].transform.localPosition);
                    }
                    else
                    {
                        DetectedObjectPoints[i].SetActive(false);
                    }
                }
                else
                {
                    DetectedObjectPoints[i].SetActive(false);
                }
            }

            OSCmanager.StopMessage();
        }
    }
}
