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

        // 1. �Է� �����͸� ����Ʈ ��ü�� ��ȯ
        DBSCAN_output.points = inputPositions.Select(pos => new DBSCANPoint { Position = pos }).ToList();

        // 2. DBSCAN ����
        int clusterCount = dbscan.Run(DBSCAN_output.points, ref DBSCAN_output.Centroids);
    }
}
