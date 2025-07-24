using HKY;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HokuyoDataSettingManager : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;

    [SerializeField] InputField IP_Adress;
    [SerializeField] Slider Zoom;
    [SerializeField] Slider X_Position;
    [SerializeField] Slider Y_Position;
    [SerializeField] Slider Rotate_Camera;

    [SerializeField] Transform SensorData_Busket;
    [SerializeField] GameObject SensorData;
    public List<URGSensorObjectDetector> SensorDatas = new List<URGSensorObjectDetector>();

    [SerializeField] HokuyoManager hokuyoManager;
    public List<HokuyoSetup> HokuyoSetups = new List<HokuyoSetup>();

    private void Start()
    {
        IP_Adress.onEndEdit.AddListener(delegate { IP_Adress_Setting(); });
        Zoom.onValueChanged.AddListener(delegate { Change_Zoom(); });
        X_Position.onValueChanged.AddListener(delegate { Change_X_Position(); });
        Y_Position.onValueChanged.AddListener (delegate { Change_Y_Position(); });
        Rotate_Camera.onValueChanged.AddListener(delegate { Change_Rotate_Camera(); });
        dropdown.onValueChanged.AddListener(delegate { Select_Hokuyo(dropdown); });
        Set_Hokuyo();
    }

    #region Dropdown Setting
    void Select_Hokuyo(Dropdown select)
    {
        try
        {
            IP_Adress.text = HokuyoSetups[select.value].Hokuyo_IP_Adress;
            Zoom.value = HokuyoSetups[select.value].Zoom_IN_OUT;
            X_Position.value = HokuyoSetups[select.value].X_Position;
            Y_Position.value = HokuyoSetups[select.value].Y_Position;
            Rotate_Camera.value = HokuyoSetups[select.value].Rotate_Camera_Value;
        }
        catch { }
    }

    public void Set_Hokuyo()
    {
        dropdown.options.Clear();
        hokuyoManager.hokuyoSetup = HokuyoSetups;

        for (int i = 0; i < HokuyoSetups.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Hokuyo" + i;
            dropdown.options.Add(option);
            dropdown.value++;

            URGSensorObjectDetector hm = Instantiate(SensorData.GetComponent<URGSensorObjectDetector>(), this.transform.position, this.transform.rotation, SensorData_Busket);
            SensorDatas.Add(hm);
        }

        hokuyoManager.m_senserData = SensorDatas;
    }

    public void Add_Hokuyo()
    {
        Dropdown.OptionData option = new Dropdown.OptionData();

        option.text = "Hokuyo" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value++;

        URGSensorObjectDetector hm = Instantiate(SensorData.GetComponent<URGSensorObjectDetector>(), this.transform.position, this.transform.rotation, SensorData_Busket);
        SensorDatas.Add(hm);

        HokuyoSetup hs = new HokuyoSetup();
        HokuyoSetups.Add(hs);

        hokuyoManager.m_senserData = SensorDatas;
        hokuyoManager.hokuyoSetup = HokuyoSetups;

        Select_Hokuyo(dropdown);
    }

    public void Remove_Hokuyo()
    {
        SensorDatas.RemoveAt(dropdown.value);
        HokuyoSetups.RemoveAt(dropdown.value);
        dropdown.options.RemoveAt(dropdown.value);
        for (int j = dropdown.value; j < dropdown.options.Count; j++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Hokuyo" + j.ToString();
            dropdown.options[j].text = option.text;
        }
        dropdown.value--;
    }
    #endregion

    public void StartHokuyo()
    {
        for(int i = 0; i < HokuyoSetups.Count; i++)
        {
            hokuyoManager.Connect_Sensor(i);
        }
    }

    public void ConnectSensor()
    {
        hokuyoManager.Connect_Sensor(dropdown.value);
    }

    public void DisconnectSensor()
    {
        hokuyoManager.Disconnect_Sensor(dropdown.value);
    }

    private void IP_Adress_Setting()
    {
        if(HokuyoSetups.Count != 0)
            HokuyoSetups[dropdown.value].Hokuyo_IP_Adress = IP_Adress.text;
    }

    private void Change_Zoom()
    {
        if (HokuyoSetups.Count != 0)
            HokuyoSetups[dropdown.value].Zoom_IN_OUT = Zoom.value;
    }

    private void Change_X_Position()
    {
        if (HokuyoSetups.Count != 0)
            HokuyoSetups[dropdown.value].X_Position = X_Position.value;
    }

    private void Change_Y_Position()
    {
        if (HokuyoSetups.Count != 0)
            HokuyoSetups[dropdown.value].Y_Position = Y_Position.value;
    }

    private void Change_Rotate_Camera()
    {
        if (HokuyoSetups.Count != 0)
            HokuyoSetups[dropdown.value].Rotate_Camera_Value = Rotate_Camera.value;
    }
}
