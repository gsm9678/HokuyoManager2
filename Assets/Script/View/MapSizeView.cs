using UnityEngine;
using UnityEngine.UI;

class MapSizeView : View
{
    [SerializeField] InputField if_XSize;
    [SerializeField] InputField if_YSize;
    [SerializeField] Slider sl_XSize;
    [SerializeField] Slider sl_YSize;

    MapSizeViewModel _mapSizeViewModel;

    private void Start()
    {
        _mapSizeViewModel = new MapSizeViewModel();

        if_XSize.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_XSize.text, sl_XSize, out float v)) _mapSizeViewModel.X_Size_Value = v; UpdateDisplay(); });
        if_YSize.onEndEdit.AddListener(delegate { if (InputFieldTxtOnChanged(if_YSize.text, sl_YSize, out float v)) _mapSizeViewModel.Y_Size_Value = v; UpdateDisplay(); });
        sl_XSize.onValueChanged.AddListener(delegate { _mapSizeViewModel.X_Size_Value = sl_XSize.value; UpdateDisplay(); });
        sl_YSize.onValueChanged.AddListener(delegate { _mapSizeViewModel.Y_Size_Value = sl_YSize.value; UpdateDisplay(); });

        UpdateDisplay();
    }

    override protected void UpdateDisplay()
    {
        if_XSize.text = _mapSizeViewModel.X_Size_Value.ToString("F2");
        if_YSize.text = _mapSizeViewModel.Y_Size_Value.ToString("F2");
        sl_XSize.value = _mapSizeViewModel.X_Size_Value;
        sl_YSize.value = _mapSizeViewModel.Y_Size_Value;
    }
}
