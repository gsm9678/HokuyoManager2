using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSize : MonoBehaviour
{
    [SerializeField] Slider Slider_X_Size;
    [SerializeField] Slider Slider_Y_Size;
    [SerializeField] RectTransform Map;

    private void Update()
    {
        Map.sizeDelta = new Vector2(Slider_X_Size.value ,Slider_Y_Size.value);
    }
}
