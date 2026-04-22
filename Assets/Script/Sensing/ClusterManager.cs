using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClusterManager
{
    DBSCAN dbscan = new DBSCAN();
    ObjectTracker objectTracker = new ObjectTracker();

    private DBSCAN_Output DBSCAN_output = new DBSCAN_Output();

    public ClusterManager()
    {
        DBSCAN_output = GameManager.instance.DBSCAN_output;
    }

    public void StartCluster(List<Vector2> inputPositions)
    {
        DBSCAN_output.points.Clear();
        DBSCAN_output.Centroids.Clear();
        DBSCAN_output.Clusters.Clear();

        // Convert input positions into DBSCAN points.
        DBSCAN_output.points = inputPositions.Select(pos => new DBSCANPoint { Position = pos }).ToList();

        // Run DBSCAN.
        int clusterCount = dbscan.Run(DBSCAN_output.points, ref DBSCAN_output.Centroids, ref DBSCAN_output.Clusters);

        // Update object tracking only when tracking mode is enabled.
        if (GameManager.instance.data.UseObjectTracking)
        {
            objectTracker.UpdateTracks(DBSCAN_output.Clusters, DBSCAN_output.TrackedObjects);
        }
        else
        {
            DBSCAN_output.TrackedObjects.Clear();
        }
    }
}
