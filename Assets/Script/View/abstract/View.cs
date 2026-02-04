using UnityEngine;
using UnityEngine.UI;

abstract class View : MonoBehaviour
{
    protected bool InputFieldTxtOnChanged(string txt, Slider slider, out float value)
    {
        value = 0;
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

        return true;
    }

    protected bool InputFieldTxtOnChanged(string txt, out int value)
    {
        value = 0;
        if (int.TryParse(txt, out int num))
            value = num;
        else
            value = 0;

        return true;
    }

    protected bool InputFieldTxtOnChanged(string txt, Slider slider, out int value)
    {
        if (int.TryParse(txt, out int num))
        {
            if (num > slider.maxValue) num = (int)slider.maxValue;
            else if (num < slider.minValue) num = (int)slider.minValue;
            value = num;
        }
        else
        {
            value = (int)slider.minValue;
        }

        return true;
    }

    protected abstract void UpdateDisplay();
}
