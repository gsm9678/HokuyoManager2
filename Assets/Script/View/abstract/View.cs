using UnityEngine;
using UnityEngine.UI;

abstract class View : MonoBehaviour
{
    protected float InputFieldTxtOnChanged(string txt, Slider slider)
    {
        float value = 0;
        if (float.TryParse(txt, out float num))
        {
            if (num > slider.maxValue) num = slider.maxValue;
            else if (num < slider.minValue) num = slider.minValue;
            value = num;
        }
        else
        {
            value = slider.minValue;
        }

        return value;
    }

    protected int InputFieldTxtOnChanged(string txt)
    {
        int value = 0;
        if (int.TryParse(txt, out int num))
            value = num;
        else
            value = 0;

        return value;
    }

    protected abstract void UpdateDisplay();
}
