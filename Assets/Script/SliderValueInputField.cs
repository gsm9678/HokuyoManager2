using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueInputField : MonoBehaviour
{
    Slider slider;
    InputField inputField;
    bool inputfieldChange = false;
    float SliderValue;

    private void Start()
    {
        inputField = this.transform.Find("InputField (Legacy)").GetComponent<InputField>();
        slider = this.transform.Find("Slider").GetComponent<Slider>();

        inputField.onValueChanged.AddListener(delegate { OnValueChanged(inputField.text); });
        inputField.onEndEdit.AddListener(delegate { OnEndEdit(inputField.text); });

        StartCoroutine(SetValueText());
    }

    IEnumerator SetValueText()
    {
        while (true)
        {
            yield return new WaitUntil(() => !inputfieldChange || SliderValue != slider.value);
            yield return new WaitUntil(() => inputField.text != (Mathf.Floor(slider.value * 100f) / 100f).ToString());
            SliderValue = slider.value;
            float v = Mathf.Floor(slider.value * 100f) / 100f;
            inputField.text = v.ToString();
        }
    }

    public void OnValueChanged(string str)
    {
        inputfieldChange = true;
    }

    public void OnEndEdit(string str)
    {
        if (float.TryParse(str, out float num))
        {
            if (num > slider.maxValue)
            {
                inputField.text = slider.maxValue.ToString();
            }
            else if (num < slider.minValue)
            {
                inputField.text = slider.minValue.ToString();
            }
            else
            {
                slider.value = num;
            }
        }
        else
        {
            inputField.text = slider.maxValue.ToString();
        }
        inputfieldChange = false;
    }
}
