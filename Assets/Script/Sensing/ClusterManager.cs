using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClusterManager
{
    DBSCAN dbscan = new DBSCAN();

    private DBSCAN_Output DBSCAN_output = new DBSCAN_Output();

    public ClusterManager()
    {
        DBSCAN_output = GameManager.instance.DBSCAN_output;
    }

    public void  StartCluster(List<Vector2> inputPositions)
    {
        DBSCAN_output.points.Clear();
        DBSCAN_output.Centroids.Clear();

        // 1. 입력 데이터를 포인트 객체로 변환
        DBSCAN_output.points = inputPositions.Select(pos => new DBSCANPoint { Position = pos }).ToList();

        // 2. DBSCAN 실행
        int clusterCount = dbscan.Run(DBSCAN_output.points, ref DBSCAN_output.Centroids);
    }
}
