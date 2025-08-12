using UnityEngine;
using UnityEngine.UI;

class SensorSettingView : View
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
    [SerializeField] Toggle tg_XFlip;
    [SerializeField] Toggle tg_YFlip;

    SensorSettingViewModel _sensorSettingViewModel;

    private void Start()
    {
        _sensorSettingViewModel = new SensorSettingViewModel();

        init_Hokuyo(dr_DropDown);

        dr_DropDown.onValueChanged.AddListener(delegate { Select_Hokuyo(dr_DropDown); });
        bt_Create.onClick.AddListener(delegate { Add_Hokuyo(dr_DropDown); });
        bt_Connect.onClick.AddListener(delegate { _sensorSettingViewModel.Connect_Sensor(); });
        bt_Disconnect.onClick.AddListener(delegate { _sensorSettingViewModel.Disconnect_Sensor(); });
        bt_Delete.onClick.AddListener(delegate { Remove_Hokuyo(dr_DropDown); });
        if_ipAddress.onEndEdit.AddListener(delegate { _sensorSettingViewModel.Hokuyo_IP_Address = if_ipAddress.text; UpdateDisplay(); });
        if_ZoomINOUT.onEndEdit.AddListener(delegate { _sensorSettingViewModel.Zoom_IN_OUT = InputFieldTxtOnChanged(if_ZoomINOUT.text, sl_ZoomINOUT); UpdateDisplay(); });
        if_XPosition.onEndEdit.AddListener(delegate { _sensorSettingViewModel.X_Position = InputFieldTxtOnChanged(if_XPosition.text, sl_XPosition); UpdateDisplay(); });
        if_YPosition.onEndEdit.AddListener(delegate { _sensorSettingViewModel.Y_Position = InputFieldTxtOnChanged(if_YPosition.text, sl_YPosition); UpdateDisplay(); });
        if_RotateSensor.onEndEdit.AddListener(delegate { _sensorSettingViewModel.Rotate_Camera_Value = InputFieldTxtOnChanged(if_RotateSensor.text, sl_RotateSensor); UpdateDisplay(); });
        sl_ZoomINOUT.onValueChanged.AddListener(delegate { _sensorSettingViewModel.Zoom_IN_OUT = sl_ZoomINOUT.value; UpdateDisplay(); });
        sl_XPosition.onValueChanged.AddListener(delegate { _sensorSettingViewModel.X_Position = sl_XPosition.value; UpdateDisplay(); });
        sl_YPosition.onValueChanged.AddListener(delegate { _sensorSettingViewModel.Y_Position = sl_YPosition.value; UpdateDisplay(); });
        sl_RotateSensor.onValueChanged.AddListener(delegate { _sensorSettingViewModel.Rotate_Camera_Value = sl_RotateSensor.value; UpdateDisplay(); });
        tg_XFlip.onValueChanged.AddListener(delegate { _sensorSettingViewModel.X_Flip = tg_XFlip.isOn; UpdateDisplay(); });
        tg_YFlip.onValueChanged.AddListener(delegate { _sensorSettingViewModel.Y_Flip = tg_YFlip.isOn; UpdateDisplay(); });
    }

    void init_Hokuyo(Dropdown dropdown)
    {
        dropdown.options.Clear();
        for (int i = 0; i < _sensorSettingViewModel.ModelCount; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Hokuyo" + i;
            dropdown.options.Add(option); ;
            dropdown.value++;
        }

        Select_Hokuyo(dropdown);
    }

    void Select_Hokuyo(Dropdown select)
    {
        try
        {
            _sensorSettingViewModel.ModelListNum = select.value;
            UpdateDisplay();
        }
        catch { }
    }
    
    void Add_Hokuyo(Dropdown dropdown)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();

        option.text = "Hokuyo" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value = dropdown.options.Count;

        _sensorSettingViewModel.Add_SensorSettingModel();

        Select_Hokuyo(dropdown);
    }

    void Remove_Hokuyo(Dropdown select)
    {
        _sensorSettingViewModel.Disconnect_Sensor();

        if (select.options.Count != 0)
        {
            _sensorSettingViewModel.Remove_SensorSettingModel();

            select.options.RemoveAt(select.value);
            select.value--;

            if(select.options.Count == 0)
                select.captionText.text = null;
            else
                for (int i = 0; i < select.options.Count; i++)
                    select.options[i].text = "Hokuyo" + i.ToString();

            Select_Hokuyo(select);
        }
    }

    override protected void UpdateDisplay()
    {
        if_ipAddress.text = _sensorSettingViewModel.Hokuyo_IP_Address;
        if_ZoomINOUT.text = _sensorSettingViewModel.Zoom_IN_OUT.ToString();
        if_XPosition.text = _sensorSettingViewModel.X_Position.ToString();
        if_YPosition.text = _sensorSettingViewModel.Y_Position.ToString();
        if_RotateSensor.text = _sensorSettingViewModel.Rotate_Camera_Value.ToString();
        sl_ZoomINOUT.value = _sensorSettingViewModel.Zoom_IN_OUT;
        sl_XPosition.value = _sensorSettingViewModel.X_Position;
        sl_YPosition.value = _sensorSettingViewModel.Y_Position;
        sl_RotateSensor.value = _sensorSettingViewModel.Rotate_Camera_Value;
        tg_XFlip.isOn = _sensorSettingViewModel.X_Flip;
        tg_YFlip.isOn = _sensorSettingViewModel.Y_Flip;
    }
}
