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
    [SerializeField] InputField if_MaxSendSignal;
    [SerializeField] Slider sl_MaxSendSignal;
    [SerializeField] Toggle tg_UseObjectTracking;

    OSCSettingViewModel _OSCSettingViewModel;
    bool isUpdatingDisplay;

    private void Start()
    {
        _OSCSettingViewModel = new OSCSettingViewModel();
        EnsureTrackingToggle();

        init_Area(dr_Dropdown);

        dr_Dropdown.onValueChanged.AddListener(delegate { Select_Area(dr_Dropdown); });
        bt_Create.onClick.AddListener(delegate { Add_Area(dr_Dropdown); });
        bt_Delete.onClick.AddListener(delegate { Remove_Area(dr_Dropdown); });
        bt_Connect.onClick.AddListener(delegate { });
        bt_Disconnect.onClick.AddListener(delegate { });
        if_OscIpAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_IP_Address = if_OscIpAddress.text; UpdateDisplay(); });
        if_OscPort.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_OscPort.text,  out int v)) _OSCSettingViewModel.OSC_IP_Port = v; UpdateDisplay(); });
        if_OscMessageAddress.onEndEdit.AddListener(delegate { _OSCSettingViewModel.OSC_Message_Address = if_OscMessageAddress.text; UpdateDisplay(); });
        if_MaxSendSignal.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_MaxSendSignal.text, sl_MaxSendSignal, out int v)) _OSCSettingViewModel.Max_SendSignal = v; UpdateDisplay(); });
        sl_MaxSendSignal.onValueChanged.AddListener(delegate { _OSCSettingViewModel.Max_SendSignal = (int)sl_MaxSendSignal.value; UpdateDisplay(); });
        tg_UseObjectTracking.onValueChanged.AddListener(delegate { if (!isUpdatingDisplay) _OSCSettingViewModel.UseObjectTracking = tg_UseObjectTracking.isOn; });

        UpdateDisplay();
    }

    void EnsureTrackingToggle()
    {
        if (tg_UseObjectTracking != null)
            return;

        GameObject toggleObject = new GameObject("UseObjectTrackingToggle", typeof(RectTransform), typeof(Toggle));
        toggleObject.transform.SetParent(transform, false);

        RectTransform toggleRect = toggleObject.GetComponent<RectTransform>();
        toggleRect.anchorMin = new Vector2(0.5f, 0.5f);
        toggleRect.anchorMax = new Vector2(0.5f, 0.5f);
        toggleRect.anchoredPosition = new Vector2(0f, -135f);
        toggleRect.sizeDelta = new Vector2(190f, 24f);

        GameObject backgroundObject = new GameObject("Background", typeof(RectTransform), typeof(Image));
        backgroundObject.transform.SetParent(toggleObject.transform, false);
        RectTransform backgroundRect = backgroundObject.GetComponent<RectTransform>();
        backgroundRect.anchorMin = new Vector2(0f, 0.5f);
        backgroundRect.anchorMax = new Vector2(0f, 0.5f);
        backgroundRect.anchoredPosition = new Vector2(10f, 0f);
        backgroundRect.sizeDelta = new Vector2(20f, 20f);
        Image backgroundImage = backgroundObject.GetComponent<Image>();
        backgroundImage.color = Color.white;

        GameObject checkmarkObject = new GameObject("Checkmark", typeof(RectTransform), typeof(Image));
        checkmarkObject.transform.SetParent(backgroundObject.transform, false);
        RectTransform checkmarkRect = checkmarkObject.GetComponent<RectTransform>();
        checkmarkRect.anchorMin = new Vector2(0.5f, 0.5f);
        checkmarkRect.anchorMax = new Vector2(0.5f, 0.5f);
        checkmarkRect.anchoredPosition = Vector2.zero;
        checkmarkRect.sizeDelta = new Vector2(12f, 12f);
        Image checkmarkImage = checkmarkObject.GetComponent<Image>();
        checkmarkImage.color = new Color(0.1f, 0.45f, 0.95f, 1f);

        GameObject labelObject = new GameObject("Label", typeof(RectTransform), typeof(Text));
        labelObject.transform.SetParent(toggleObject.transform, false);
        RectTransform labelRect = labelObject.GetComponent<RectTransform>();
        labelRect.anchorMin = new Vector2(0f, 0f);
        labelRect.anchorMax = new Vector2(1f, 1f);
        labelRect.offsetMin = new Vector2(28f, 0f);
        labelRect.offsetMax = new Vector2(0f, 0f);
        Text labelText = labelObject.GetComponent<Text>();
        labelText.text = "ID Tracking OSC";
        labelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        labelText.fontSize = 13;
        labelText.alignment = TextAnchor.MiddleLeft;
        labelText.color = Color.black;

        tg_UseObjectTracking = toggleObject.GetComponent<Toggle>();
        tg_UseObjectTracking.targetGraphic = backgroundImage;
        tg_UseObjectTracking.graphic = checkmarkImage;
        tg_UseObjectTracking.isOn = false;
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
        isUpdatingDisplay = true;
        if_OscIpAddress.text = _OSCSettingViewModel.OSC_IP_Address;
        if_OscPort.text = _OSCSettingViewModel.OSC_IP_Port.ToString();
        if_OscMessageAddress.text = _OSCSettingViewModel.OSC_Message_Address;
        if_MaxSendSignal.text = _OSCSettingViewModel.Max_SendSignal.ToString();
        tg_UseObjectTracking.isOn = _OSCSettingViewModel.UseObjectTracking;
        isUpdatingDisplay = false;
    }
}
