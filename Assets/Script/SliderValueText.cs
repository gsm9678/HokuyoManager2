using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    Slider slider;
    Text text;

    private void Start()
    {
        text = this.transform.Find("SliderText (Legacy)").GetComponent<Text>();
        slider = this.transform.Find("Slider").GetComponent<Slider>();

        StartCoroutine(SetValueText());
    }

    IEnumerator SetValueText()
    {
        while (true)
        {
            yield return new WaitUntil(() => text.text != (Mathf.Floor(slider.value * 100f) / 100f).ToString());
            float v = Mathf.Floor(slider.value * 100f) / 100f;
            text.text = v.ToString();
        }
    }
}
