using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GizmosPosition : MonoBehaviour
{
    [SerializeField] Slider Slider_X_Position;
    [SerializeField] Slider Slider_Y_Position;
    [SerializeField] Slider Slider_RotateCamera;
    [SerializeField] GameObject Gizmos_Ob;

    // Update is called once per frame
    void Update()
    {
        Gizmos_Ob.transform.localPosition = new Vector3(Slider_X_Position.value, Slider_Y_Position.value - 200);
        Gizmos_Ob.transform.localRotation = Quaternion.Euler(0,0,Slider_RotateCamera.value);
    }
}
