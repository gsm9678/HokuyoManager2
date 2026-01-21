using UnityEngine;
using UnityEngine.UI;

class OSCSettingView : View
{
    [SerializeField] Dropdown dr_Dropdown;
    [SerializeField] Button bt_Create;
    [SerializeField] Button bt_Delete;
    [SerializeField] Button bt_Connect;
    [SerializeField] Button bt_Disconnect;
    [SerializeField] InputField if_OscIpAddress;
    [SerializeField] InputField if_OscPort;
    [SerializeField] InputField if_OscMessageAddress;

    OSCSettingViewModel _OSCSettingViewModel;

    private void Start()
    {
        _OSCSettingViewModel = new OSCSettingViewModel();

        init_Area(dr_Dropdown);

        dr_Dropdown.onValueChanged.AddListener(delegate { Select_Area(dr_Dropdown); });
        bt_Create.onClick.AddListener(delegate { Add_Area(dr_Dropdown); });
        bt_Delete.onClick.AddListener(delegate { Remove_Area(dr_Dropdown); });
        bt_Connect.onClick.AddListener(delegate { });
        bt_Disconnect.onClick.AddListener(delegate { });
        if_OscIpAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_IP_Address = if_OscIpAddress.text; UpdateDisplay(); });
        if_OscPort.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_IP_Port = InputFieldTxtOnChanged(if_OscPort.text); UpdateDisplay(); });
        if_OscMessageAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_Message_Address = if_OscMessageAddress.text; UpdateDisplay(); });

        UpdateDisplay();
    }

    void init_Area(Dropdown dropdown)
    {
        dropdown.options.Clear();
        for (int i = 0; i < _OSCSettingViewModel.ModelCount; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Osc" + i;
            dropdown.options.Add(option); ;
            dropdown.value++;
        }

        Select_Area(dropdown);
    }

    void Select_Area(Dropdown dropdown)
    {
        try
        {
            _OSCSettingViewModel.ModelListNum = dropdown.value;
            UpdateDisplay();
        }
        catch { }
    }

    void Add_Area(Dropdown dropdown)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = "Osc" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value = dropdown.options.Count;

        _OSCSettingViewModel.Add_Model();
        Select_Area(dropdown);
    }

    void Remove_Area(Dropdown dropdown)
    {
        if (dropdown.options.Count != 0)
        {
            _OSCSettingViewModel.Remove_Model();

            dropdown.options.RemoveAt(dropdown.value);
            dropdown.value--;

            if (dropdown.options.Count == 0)
                dropdown.captionText.text = null;
            else
                for (int i = 0; i < dropdown.options.Count; i++)
                    dropdown.options[i].text = "Osc" + i.ToString();
            Select_Area(dropdown);
        }
    }

    override protected void UpdateDisplay()
    {
        if_OscIpAddress.text = _OSCSettingViewModel.OSC_IP_Address;
        if_OscPort.text = _OSCSettingViewModel.OSC_IP_Port.ToString();
        if_OscMessageAddress.text = _OSCSettingViewModel.OSC_Message_Address;
    }
}
