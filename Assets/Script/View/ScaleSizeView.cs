using UnityEngine;
using UnityEngine.UI;

class ScaleSizeView : View
{
    [SerializeField] InputField if_Epsilon;
    [SerializeField] InputField if_MinPoint;
    [SerializeField] Slider sl_Epsilon;
    [SerializeField] Slider sl_MinPoint;

    ScaleSizeViewModel _scaleSizeViewModel;

    private void Start()
    {
        _scaleSizeViewModel = new ScaleSizeViewModel();

        if_Epsilon.onEndEdit.AddListener(delegate { _scaleSizeViewModel.Epsilon = InputFieldTxtOnChanged(if_Epsilon.text, sl_Epsilon); UpdateDisplay(); });
        if_MinPoint.onEndEdit.AddListener(delegate { _scaleSizeViewModel.Min_Point = InputFieldTxtOnChanged(if_MinPoint.text, sl_MinPoint); UpdateDisplay(); });
        sl_Epsilon.onValueChanged.AddListener(delegate { _scaleSizeViewModel.Epsilon = sl_Epsilon.value; UpdateDisplay(); });
        sl_MinPoint.onValueChanged.AddListener(delegate { _scaleSizeViewModel.Min_Point = sl_MinPoint.value; UpdateDisplay(); });

        UpdateDisplay();
    }

    override protected void UpdateDisplay()
    {
        if_Epsilon.text = _scaleSizeViewModel.Epsilon.ToString();
        if_MinPoint.text = _scaleSizeViewModel.Min_Point.ToString();
        sl_Epsilon.value = _scaleSizeViewModel.Epsilon;
        sl_MinPoint.value = _scaleSizeViewModel.Min_Point;
    }
}
