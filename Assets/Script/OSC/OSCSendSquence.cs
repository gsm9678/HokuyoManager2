using UnityEngine;

public class OSCSendSquence : MonoBehaviour
{
    private DBSCAN_Output DBSCAN_output;
    private RoomSizeDataModel _roomSizeDataModel;
    private int _max_SendSignal;
    // Start is called before the first frame update
    void Start()
    {
        RefreshReferences();
    }

    void RefreshReferences()
    {
        if (GameManager.instance == null || GameManager.instance.data == null)
            return;

        DBSCAN_output = GameManager.instance.DBSCAN_output;
        _roomSizeDataModel = GameManager.instance.data.RoomSizeData;
    }
    // Update is called once per frame
    void Update()
    {
        if (DBSCAN_output == null || _roomSizeDataModel == null)
            RefreshReferences();

        if (DBSCAN_output == null || _roomSizeDataModel == null || OSCManager.instance == null)
            return;

        if (_roomSizeDataModel != GameManager.instance.data.RoomSizeData)
            _roomSizeDataModel = GameManager.instance.data.RoomSizeData;
        if(_max_SendSignal != GameManager.instance.data.Max_SendSignal)
            _max_SendSignal = GameManager.instance.data.Max_SendSignal;
        
        if(_roomSizeDataModel.X_Size_Value != 0)
        {
            OSCManager.instance.StartMessage(new Vector2(_roomSizeDataModel.X_Size_Value, _roomSizeDataModel.Y_Size_Value));

            if (GameManager.instance.data.UseObjectTracking)
            {
                int sendCount = 0;
                for (int i = 0; i < DBSCAN_output.TrackedObjects.Count; i++)
                {
                    if (_max_SendSignal <= sendCount)
                        break;

                    TrackedObject trackedObject = DBSCAN_output.TrackedObjects[i];
                    if (trackedObject.State == TrackState.Lost)
                        continue;

                    OSCManager.instance.TrackedSensorMessage(trackedObject);
                    sendCount++;
                }
            }
            else
            {
                for (int i = 0; i < DBSCAN_output.Centroids.Count; i++)
                {
                    if (_max_SendSignal <= i)
                        break;
                    OSCManager.instance.SensorMessage(DBSCAN_output.Centroids[i]);
                }
            }
            OSCManager.instance.EndMessage();
        }
    }
}
