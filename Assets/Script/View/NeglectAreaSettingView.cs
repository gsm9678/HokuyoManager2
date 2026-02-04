using UnityEngine;
using UnityEngine.UI;

class NeglectAreaSettingView : View
{
    [SerializeField] Dropdown dr_Dropdown;
    [SerializeField] Button bt_Create;
    [SerializeField] Button bt_Delete;
    [SerializeField] InputField if_XPosition;
    [SerializeField] InputField if_YPosition;
    [SerializeField] InputField if_XSize;
    [SerializeField] InputField if_YSize;
    [SerializeField] Slider sl_XPosition;
    [SerializeField] Slider sl_YPosition;
    [SerializeField] Slider sl_XSize;
    [SerializeField] Slider sl_YSize;

    NeglectAreaSettingViewModel _neglectAreaSettingViewModel;

    private void Start()
    {
        _neglectAreaSettingViewModel = new NeglectAreaSettingViewModel();

        init_Area(dr_Dropdown);

        dr_Dropdown.onValueChanged.AddListener(delegate { Select_Area(dr_Dropdown); });
        bt_Create.onClick.AddListener(delegate { Add_Area(dr_Dropdown); });
        bt_Delete.onClick.AddListener(delegate { Remove_Area(dr_Dropdown); });
        if_XPosition.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_XPosition.text, sl_XPosition, out float v)) _neglectAreaSettingViewModel.X_Position_Value = v; UpdateDisplay(); });
        if_YPosition.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_YPosition.text, sl_YPosition, out float v)) _neglectAreaSettingViewModel.Y_Position_Value = v; UpdateDisplay(); });
        if_XSize.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_XSize.text, sl_XSize, out float v)) _neglectAreaSettingViewModel.X_Size_Value = v; UpdateDisplay(); });
        if_YSize.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_YSize.text, sl_YSize, out float v)) _neglectAreaSettingViewModel.Y_Size_Value = v; UpdateDisplay(); });
        sl_XPosition.onValueChanged.AddListener(delegate { _neglectAreaSettingViewModel.X_Position_Value = sl_XPosition.value; UpdateDisplay(); });
        sl_YPosition.onValueChanged.AddListener(delegate { _neglectAreaSettingViewModel.Y_Position_Value = sl_YPosition.value; UpdateDisplay(); });
        sl_XSize.onValueChanged.AddListener(delegate { _neglectAreaSettingViewModel.X_Size_Value = sl_XSize.value; UpdateDisplay(); });
        sl_YSize.onValueChanged.AddListener(delegate { _neglectAreaSettingViewModel.Y_Size_Value = sl_YSize.value; UpdateDisplay(); });
    }

    void init_Area(Dropdown dropdown)
    {
        dropdown.options.Clear();
        for (int i = 0; i < _neglectAreaSettingViewModel.ModelCount; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Neglect Area" + i;
            dropdown.options.Add(option); ;
            dropdown.value++;
        }

        Select_Area(dropdown);
    }

    void Select_Area(Dropdown dropdown)
    {
        try
        {
            _neglectAreaSettingViewModel.ModelListNum = dropdown.value;
            UpdateDisplay();
        }
        catch { }
    }

    void Add_Area(Dropdown dropdown)
    {
        Dropdown.OptionData option = new Dropdown.OptionData();
        option.text = "Neglect Area" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value = dropdown.options.Count;

        _neglectAreaSettingViewModel.Add_Model();
        Select_Area(dropdown);
    }

    void Remove_Area(Dropdown dropdown)
    {
        if (dropdown.options.Count != 0)
        {
            _neglectAreaSettingViewModel.Remove_Model();

            dropdown.options.RemoveAt(dropdown.value);
            dropdown.value--;

            if (dropdown.options.Count == 0)
                dropdown.captionText.text = null;
            else
                for (int i = 0; i < dropdown.options.Count; i++)
                    dropdown.options[i].text = "Neglect Area" + i.ToString();
            Select_Area(dropdown);
        }
    }

    protected override void UpdateDisplay()
    {
        if_XPosition.text = _neglectAreaSettingViewModel.X_Position_Value.ToString("F2");
        if_YPosition.text = _neglectAreaSettingViewModel.Y_Position_Value.ToString("F2");
        if_XSize.text = _neglectAreaSettingViewModel.X_Size_Value.ToString("F2");
        if_YSize.text = _neglectAreaSettingViewModel.Y_Size_Value.ToString("F2");
        sl_XPosition.value = _neglectAreaSettingViewModel.X_Position_Value;
        sl_YPosition.value = _neglectAreaSettingViewModel.Y_Position_Value;
        sl_XSize.value = _neglectAreaSettingViewModel.X_Size_Value;
        sl_YSize.value = _neglectAreaSettingViewModel.Y_Size_Value;
    }
}
