using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class SensorSettingView : MonoBehaviour
{
    [SerializeField] Dropdown dr_DropDown;
    [SerializeField] Button bt_Create;
    [SerializeField] Button bt_Connect;
    [SerializeField] Button bt_Disconnect;
    [SerializeField] Button bt_Delete;
    [SerializeField] InputField if_ipAddress;
    [SerializeField] InputField if_ZoomINOUT;
    [SerializeField] InputField if_XPosition;
    [SerializeField] InputField if_YPosition;
    [SerializeField] InputField if_RotateSensor;
    [SerializeField] Slider sl_ZoomINOUT;
    [SerializeField] Slider sl_XPosition;
    [SerializeField] Slider sl_YPosition;
    [SerializeField] Slider sl_RotateSensor;

    [SerializeField] SensorSettingViewModel _sensorSettingViewModel;

    private void Start()
    {
        _sensorSettingViewModel = new SensorSettingViewModel();

        dr_DropDown.onValueChanged.AddListener(delegate { Select_Hokuyo(dr_DropDown); });
        bt_Create.onClick.AddListener(delegate {  });
        bt_Connect.onClick.AddListener(delegate {  });
        bt_Disconnect.onClick.AddListener(delegate {  });
        bt_Delete.onClick.AddListener(delegate {  });
        if_ipAddress.onValueChanged.AddListener(delegate {  });
        if_ZoomINOUT.onValueChanged.AddListener(delegate { SyscInputField2Slider(if_ZoomINOUT, sl_ZoomINOUT); });
        if_XPosition.onValueChanged.AddListener(delegate { SyscInputField2Slider(if_XPosition, sl_XPosition); });
        if_YPosition.onValueChanged.AddListener(delegate { SyscInputField2Slider(if_YPosition, sl_YPosition); });
        if_RotateSensor.onValueChanged.AddListener(delegate { SyscInputField2Slider(if_RotateSensor, sl_RotateSensor); });
        sl_ZoomINOUT.onValueChanged.AddListener(delegate { SyscSlider2InputField(sl_ZoomINOUT, if_ZoomINOUT); });
        sl_XPosition.onValueChanged.AddListener(delegate { SyscSlider2InputField(sl_XPosition, if_XPosition); });
        sl_YPosition.onValueChanged.AddListener(delegate { SyscSlider2InputField(sl_YPosition, if_YPosition); });
        sl_RotateSensor.onValueChanged.AddListener(delegate { SyscSlider2InputField(sl_RotateSensor, if_RotateSensor); });
    }

    void Select_Hokuyo(Dropdown select)
    {
        try
        {
            if_ipAddress.text = _sensorSettingViewModel._sensorSettingModels[select.value].Hokuyo_IP_Adress;
            sl_ZoomINOUT.value = _sensorSettingViewModel._sensorSettingModels[select.value].Zoom_IN_OUT;
            sl_XPosition.value = _sensorSettingViewModel._sensorSettingModels[select.value].X_Position;
            sl_YPosition.value = _sensorSettingViewModel._sensorSettingModels[select.value].Y_Position;
            sl_RotateSensor.value = _sensorSettingViewModel._sensorSettingModels[select.value].Rotate_Camera_Value;
        }
        catch { }
    }

    void SyscInputField2Slider(InputField inputField, Slider slider)
    {
        if (float.TryParse(inputField.text, out float num))
        {
            if (num > slider.maxValue)
            {
                inputField.text = slider.maxValue.ToString();
            }
            else if (num < slider.minValue)
            {
                inputField.text = slider.minValue.ToString();
            }
            else
            {
                slider.value = num;
            }
        }
        else
        {
            inputField.text = slider.minValue.ToString();
        }
    }

    void SyscSlider2InputField(Slider slider, InputField inputField)
    {
        inputField.text = slider.value.ToString();
    }

}
