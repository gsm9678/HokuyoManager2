using HKY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HokuyoManager : MonoBehaviour
{
    private URGSensorObjectDetector m_senserData;
    private BoxManager m_boxManager;

    [SerializeField] Slider Zoom;

    [SerializeField] GameObject gizmos_Image, Gizmos_Ob;

    public List<GameObject> gizmos_Images =  new List<GameObject>();

    [SerializeField]
    RectTransform Map;

    // Start is called before the first frame update
    void Start()
    {
        m_senserData = GameObject.Find("Sensor Data").GetComponent<URGSensorObjectDetector>();
        m_boxManager = GameObject.Find("BoxManager").GetComponent<BoxManager>();

        for (int i = 0; i < 1081; i++)
        {
            gizmos_Images.Add(Instantiate(gizmos_Image, this.transform.position, this.transform.rotation, Gizmos_Ob.transform));
        }
    }


    private void FixedUpdate()
    {
        for (int i = 0; i < m_senserData.DirectedDistances.Count; i++)
        {
            Vector3 vector = new Vector3(scale(-m_senserData.detectRectWidth / 2, m_senserData.detectRectWidth / 2, (-m_senserData.detectRectWidth / 200) * Zoom.value, (m_senserData.detectRectWidth / 200) * Zoom.value, m_senserData.DirectedDistances[i].x),
                                        scale(0, m_senserData.detectRectHeight, 0 * Zoom.value, m_senserData.detectRectHeight / 100 * Zoom.value, m_senserData.DirectedDistances[i].y),
                                        0);

            gizmos_Images[i].transform.localPosition = vector;

            if (gizmos_Images[i].transform.position.x < Map.rect.width / 2 + Map.transform.position.x &&
                gizmos_Images[i].transform.position.x > -Map.rect.width / 2 + Map.transform.position.x &&
                gizmos_Images[i].transform.position.y < Map.rect.height / 2 + Map.transform.position.y &&
                gizmos_Images[i].transform.position.y > -Map.rect.height / 2 + Map.transform.position.y)
            {
                if(m_boxManager.objects.Count > 0)
                {
                    foreach (RectTransform box in m_boxManager.objects)
                    {
                        if (gizmos_Images[i].transform.position.x < box.rect.width / 2 + box.transform.position.x &&
                            gizmos_Images[i].transform.position.x > -box.rect.width / 2 + box.transform.position.x &&
                            gizmos_Images[i].transform.position.y < box.rect.height / 2 + box.transform.position.y &&
                            gizmos_Images[i].transform.position.y > -box.rect.height / 2 + box.transform.position.y)
                        {
                            gizmos_Images[i].SetActive(false); break;
                        }
                        else
                        {
                            gizmos_Images[i].SetActive(true);
                        }
                    }
                }
                else
                {
                    gizmos_Images[i].SetActive(true);
                }
                    
            }
            else
            {
                gizmos_Images[i].SetActive(false);
            }
        }
    }

    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
