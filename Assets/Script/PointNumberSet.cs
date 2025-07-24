using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNumberSet : MonoBehaviour
{
    [SerializeField] PointDetector pointDetector;

    List<Vector3> InScaleDetectedObjectPoints = new List<Vector3>();
    List<PointTrackingData> PointTracking = new List<PointTrackingData>();

    private void Start()
    {
        InScaleDetectedObjectPoints = pointDetector.InScaleDetectedObjectPoints;
    }

    private void Update()
    {
        for(int i = PointTracking.Count; i < InScaleDetectedObjectPoints.Count; i++)
        {
            PointTracking.Add(new PointTrackingData());
        }

        foreach(var vector3 in InScaleDetectedObjectPoints)
        {
            PointTrackingData ptd = new PointTrackingData();

            for(int i = 0; i < PointTracking.Count; i++)
            {
                if (PointTracking[i].state == true)
                {
                    //PointTracking[i].position
                }
            }
        }
    }
}
