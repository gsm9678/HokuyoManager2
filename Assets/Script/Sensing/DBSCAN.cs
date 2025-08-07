using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBSCAN
{
    private ScaleSizeDataModel _scaleSizeDataModel;

    public DBSCAN()
    {
        _scaleSizeDataModel = GameManager.instance.data.ScaleSizeData;
    }

    public int Run(List<DBSCANPoint> points, ref List<Vector2> Centroids)
    {
        int clusterId = 0;

        foreach (var point in points)
        {
            if (point.Visited)
                continue;

            point.Visited = true;
            var neighbors = GetNeighbors(points, point);

            if (neighbors.Count < _scaleSizeDataModel.Min_Point)
            {
                point.ClusterId = -1; // Noise
            }
            else
            {
                Centroids.Add(ExpandCluster(points, point, neighbors, clusterId));
                clusterId++;
            }
        }

        return clusterId; // total cluster count
    }

    private Vector2 ExpandCluster(List<DBSCANPoint> points, DBSCANPoint point, List<DBSCANPoint> neighbors, int clusterId)
    {
        point.ClusterId = clusterId;

        for (int i = 0; i < neighbors.Count; i++)
        {
            var p = neighbors[i];

            if (!p.Visited)
            {
                p.Visited = true;
                var pNeighbors = GetNeighbors(points, p);
                if (pNeighbors.Count >= _scaleSizeDataModel.Min_Point)
                {
                    neighbors.AddRange(pNeighbors.Except(neighbors));
                }
            }

            if (p.ClusterId == -1)
            {
                p.ClusterId = clusterId;
            }
        }

        return ComputeCentroid(neighbors);
    }

    Vector2 ComputeCentroid(List<DBSCANPoint> clusterPoints)
    {
        Vector2 sum = Vector2.zero;
        foreach (var p in clusterPoints)
            sum += p.Position;
        return sum / clusterPoints.Count;
    }

    private List<DBSCANPoint> GetNeighbors(List<DBSCANPoint> points, DBSCANPoint center)
    {
        List<DBSCANPoint> neighbors = new List<DBSCANPoint>();
        foreach (var p in points)
        {
            if (Vector2.Distance(center.Position, p.Position) <= _scaleSizeDataModel.Epsilon)
                neighbors.Add(p);
        }
        return neighbors;
    }
}
