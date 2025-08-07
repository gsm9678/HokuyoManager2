using UnityEngine;
using UnityEngine.UI;

class FlipCheckView : View
{
    [SerializeField] Toggle tg_XFlip;
    [SerializeField] Toggle tg_YFlip;

    FlipCheckViewModel _flipCheckViewModel;

    private void Start()
    {
        _flipCheckViewModel = new FlipCheckViewModel();

        tg_XFlip.onValueChanged.AddListener(delegate { _flipCheckViewModel.X_Flip = tg_XFlip.isOn; UpdateDisplay(); });
        tg_YFlip.onValueChanged.AddListener(delegate { _flipCheckViewModel.Y_Flip = tg_YFlip.isOn; UpdateDisplay(); });

        UpdateDisplay();
    }

    override protected void UpdateDisplay()
    {
        tg_XFlip.isOn = _flipCheckViewModel.X_Flip;
        tg_YFlip.isOn = _flipCheckViewModel.Y_Flip;
    }
}
