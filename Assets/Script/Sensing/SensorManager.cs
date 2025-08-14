using HKY;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    [SerializeField] Transform tr_SensorData_Busket;
    [SerializeField] GameObject p_SensorData;
    public List<URGSensorObjectDetector> SensorDatas = new List<URGSensorObjectDetector>();

    List<SensorSettingModel> SensorSettings;

    private void Start()
    {
        SensorSettings = GameManager.instance.data.SensorSettingModels;
        init_SensorData();
        GameManager.instance.SensorManageAction += PropertyChanged;
    }

    void PropertyChanged(string name, int i)
    {
        switch (name)
        {
            case "SensorAdd":
                Add_SensorData();
                break;
            case "SensorRemove":
                Remove_SensorData(i);
                break;
            case "SensorConnect":
                Connect_SensorData(i);
                break;
            case "SensorDisconnect":
                Disconnect_SensorData(i);
                break;
        }
    }

    private void init_SensorData()
    {
        for(int i = 0; i < SensorSettings.Count; i++)
        {
            URGSensorObjectDetector sensorData = Instantiate(p_SensorData.GetComponent<URGSensorObjectDetector>(), this.transform.position, this.transform.rotation, tr_SensorData_Busket);
            SensorDatas.Add(sensorData);

            Connect_SensorData(i);
        }
    }

    private void Add_SensorData()
    {
        URGSensorObjectDetector sensorData = Instantiate(p_SensorData.GetComponent<URGSensorObjectDetector>(), this.transform.position, this.transform.rotation, tr_SensorData_Busket);
        SensorDatas.Add(sensorData);
    }

    private void Remove_SensorData(int i)
    {
        SensorDatas.RemoveAt(i);
    }

    private void Connect_SensorData(int i)
    {
        SensorDatas[i].ip_address = SensorSettings[i].Hokuyo_IP_Address;
        if (SensorDatas[i].gameObject.activeSelf)
            SensorDatas[i].EnableTCP();
        SensorDatas[i].gameObject.SetActive(true);
    }

    private void Disconnect_SensorData(int i)
    {
        try
        {
            SensorDatas[i].DisableTCP();
        }
        catch { }
    }
}
