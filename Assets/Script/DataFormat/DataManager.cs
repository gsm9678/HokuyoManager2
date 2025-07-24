using HKY;
using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public BoxManager m_boxManager;
    public HokuyoDataSettingManager settingManager;
    //public URGSensorObjectDetector m_senserData;
    public OSCManager m_OSCManager;

    [SerializeField] Slider X_Size;
    [SerializeField] Slider Y_Size;
    [SerializeField] Slider Point_Scale;
    [SerializeField] Slider Max_Scale;
    [SerializeField] Slider Min_Scale;
    [SerializeField] InputField OSC_IP_Adress;
    [SerializeField] InputField OSC_Adress;

    DataFormat data = new DataFormat();

    string path;

    public bool isStarted = false;

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        if (!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            data = JsonUtility.FromJson<DataFormat>(loadJson);

            if (data != null)
            {
                m_OSCManager.setName(data.OSC_Adress);
                //m_senserData.ip_address = data.IP_Adress;
                //m_senserData.gameObject.SetActive(true);

                X_Size.value = data.X_Size_Value;
                Y_Size.value = data.Y_Size_Value;
                Point_Scale.value = data.Point_Scale_Value;
                Max_Scale.value = data.Max_Scale_Value;
                Min_Scale.value = data.Min_Scale_Value;
                OSC_IP_Adress.text = data.OSC_IP_Adress;
                OSC_Adress.text = data.OSC_Adress;

                m_boxManager.boxes = data.BoxData;
                settingManager.HokuyoSetups = data.hokuyoSetups;
                m_boxManager.setDropDownOpthions();
                settingManager.Invoke("StartHokuyo", 0.1f);
            }
        }
        isStarted = true;
    }

    public void JsonSave()
    {
        Invoke("SaveFunc", 0.5f);
    }

    void SaveFunc()
    {
        data.X_Size_Value = X_Size.value;
        data.Y_Size_Value = Y_Size.value;
        data.Point_Scale_Value = Point_Scale.value;
        data.Max_Scale_Value = Max_Scale.value;
        data.Min_Scale_Value = Min_Scale.value;
        data.OSC_IP_Adress = OSC_IP_Adress.text;
        data.OSC_Adress = OSC_Adress.text;

        data.BoxData = m_boxManager.boxes;
        data.hokuyoSetups = settingManager.HokuyoSetups;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
}
