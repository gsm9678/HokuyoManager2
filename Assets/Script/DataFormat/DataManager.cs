using HKY;
using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public BoxManager m_boxManager;
    public URGSensorObjectDetector m_senserData;
    public OSCManager m_OSCManager;

    [SerializeField] Slider Zoom_InOut;
    [SerializeField] Slider X_Position;
    [SerializeField] Slider Y_Position;
    [SerializeField] Slider Rotate_Camera;
    [SerializeField] Slider X_Size;
    [SerializeField] Slider Y_Size;
    [SerializeField] Slider Point_Scale;
    [SerializeField] Slider Max_Scale;
    [SerializeField] Slider Min_Scale;
    [SerializeField] InputField IP_Adress;
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
                m_senserData.ip_address = data.IP_Adress;
                m_senserData.gameObject.SetActive(true);

                Zoom_InOut.value = data.Zoom_InOut_Value;
                X_Position.value = data.X_Position_Value;
                Y_Position.value = data.Y_Position_Value;
                Rotate_Camera.value = data.Rotate_Camera_Value;
                X_Size.value = data.X_Size_Value;
                Y_Size.value = data.Y_Size_Value;
                Point_Scale.value = data.Point_Scale_Value;
                Max_Scale.value = data.Max_Scale_Value;
                Min_Scale.value = data.Min_Scale_Value;
                IP_Adress.text = data.IP_Adress;
                OSC_IP_Adress.text = data.OSC_IP_Adress;
                OSC_Adress.text = data.OSC_Adress;

                m_boxManager.boxes = data.BoxData;
                m_boxManager.setDropDownOpthions();
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
        data.Zoom_InOut_Value = Zoom_InOut.value;
        data.X_Position_Value = X_Position.value;
        data.Y_Position_Value = Y_Position.value;
        data.Rotate_Camera_Value = Rotate_Camera.value;
        data.X_Size_Value = X_Size.value;
        data.Y_Size_Value = Y_Size.value;
        data.Point_Scale_Value = Point_Scale.value;
        data.Max_Scale_Value = Max_Scale.value;
        data.Min_Scale_Value = Min_Scale.value;
        data.IP_Adress = IP_Adress.text;
        data.OSC_IP_Adress = OSC_IP_Adress.text;
        data.OSC_Adress = OSC_Adress.text;

        data.BoxData = m_boxManager.boxes;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
}
