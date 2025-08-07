using UnityEngine;
using UnityEngine.UI;

class OSCSettingView : View
{
    [SerializeField] Button bt_Connect;
    [SerializeField] Button bt_Disconnect;
    [SerializeField] InputField if_OscIpAddress;
    [SerializeField] InputField if_OscPort;
    [SerializeField] InputField if_OscMessageAddress;

    OSCSettingViewModel _OSCSettingViewModel;

    private void Start()
    {
        _OSCSettingViewModel = new OSCSettingViewModel();

        bt_Connect.onClick.AddListener(delegate { });
        bt_Disconnect.onClick.AddListener(delegate { });
        if_OscIpAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_IP_Address = if_OscIpAddress.text; UpdateDisplay(); });
        if_OscPort.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_IP_Port = InputFieldTxtOnChanged(if_OscPort.text); UpdateDisplay(); });
        if_OscMessageAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_Message_Address = if_OscMessageAddress.text; UpdateDisplay(); });

        UpdateDisplay();
    }

    override protected void UpdateDisplay()
    {
        if_OscIpAddress.text = _OSCSettingViewModel.OSC_IP_Address;
        if_OscPort.text = _OSCSettingViewModel.OSC_IP_Port.ToString();
        if_OscMessageAddress.text = _OSCSettingViewModel.OSC_Message_Address;
    }
}
