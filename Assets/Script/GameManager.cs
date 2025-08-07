using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public DataFormat data = new DataFormat();
    public DBSCAN_Output DBSCAN_output = new DBSCAN_Output();

    public Action AreaSizeAction;
    public Action<string, int> NeglectAreaAction;
    public Action<string, int> SensorManageAction;
    public Action<string> OSCManageAction;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
