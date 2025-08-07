using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointVisualization : MonoBehaviour
{
    private ScaleSizeDataModel _scaleSizeDataModel;
    private DBSCAN_Output DBSCAN_output;

    [SerializeField] GameObject p_Point;
    [SerializeField] Transform tr_PointParent;

    [SerializeField] GameObject p_DetectedPoint;
    [SerializeField] Transform tr_DetectedPoint;

    List<Transform> PointObject = new List<Transform>();
    List<Transform> DetectedObjectPoints = new List<Transform>();

    private void Start()
    {
        _scaleSizeDataModel = GameManager.instance.data.ScaleSizeData;
        DBSCAN_output = GameManager.instance.DBSCAN_output;
    }

    void FixedUpdate()
    {
        if (Application.isFocused)
        {
            int i = 0;

            for (; i < DBSCAN_output.points.Count; i++)
            {
                if (PointObject.Count <= i)
                    PointObject.Add(Instantiate(p_Point.transform, tr_PointParent));

                PointObject[i].localPosition = DBSCAN_output.points[i].Position;
                if (!PointObject[i].gameObject.activeSelf)
                    PointObject[i].gameObject.SetActive(true);

                PointObject[i].GetComponent<RectTransform>().sizeDelta = new Vector2(_scaleSizeDataModel.Epsilon, _scaleSizeDataModel.Epsilon);

                Color color = DBSCAN_output.points[i].ClusterId == -1 ? Color.gray : GetColor(DBSCAN_output.points[i].ClusterId);
                PointObject[i].GetComponent<Image>().color = color;
            }

            for (; i < PointObject.Count; i++)
            {
                if (PointObject[i].gameObject.activeSelf)
                    PointObject[i].gameObject.SetActive(false);
                else
                    break;
            }

            int j = 0;
            for (; j < DBSCAN_output.Centroids.Count; j++)
            {
                if (DetectedObjectPoints.Count <= j)
                    DetectedObjectPoints.Add(Instantiate(p_DetectedPoint.transform,  tr_DetectedPoint));

                DetectedObjectPoints[j].gameObject.SetActive(true);
                DetectedObjectPoints[j].localPosition = DBSCAN_output.Centroids[j];
            }
            for (; j < DetectedObjectPoints.Count; j++)
            {
                if (DetectedObjectPoints[j].gameObject.activeSelf)
                    DetectedObjectPoints[j].gameObject.SetActive(false);
                else
                    break;
            }
        }
        else
        {
            for (int i = 0; i < PointObject.Count; i++)
            {
                Destroy(PointObject[i].gameObject);
            }
            PointObject.Clear();
            for (int i = 0; i < DetectedObjectPoints.Count; i++)
            {
                Destroy(DetectedObjectPoints[i].gameObject);
            }
            DetectedObjectPoints.Clear();
        }
    }

    Color GetColor(int clusterId)
    {
        Random.InitState(clusterId);
        return new Color(Random.value, Random.value, Random.value);
    }
}
