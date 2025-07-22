using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;

    [SerializeField] Slider X_Position;
    [SerializeField] Slider Y_Position;
    [SerializeField] Slider X_Size;
    [SerializeField] Slider Y_Size;

    public List<BoxData> boxes = new List<BoxData>();

    public List<RectTransform> objects = new List<RectTransform>();

    [SerializeField] GameObject p_box;
    [SerializeField] Transform t_boxes;

    private void Start()
    {
        dropdown.onValueChanged.AddListener(delegate { Function_Dropdown(dropdown); });
    }

    private void Update()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].Name == dropdown.options[dropdown.value].text)
            {
                boxes[i].X_Position_Value = X_Position.value;
                boxes[i].Y_Position_Value = Y_Position.value;
                boxes[i].X_Size_Value = X_Size.value;
                boxes[i].Y_Size_Value = Y_Size.value;
            }
        }

        for (int i =0; i < boxes.Count; i++)
        {
            objects[i].transform.localPosition = new Vector3(boxes[i].X_Position_Value, boxes[i].Y_Position_Value, 0);
            objects[i].sizeDelta = new Vector2(boxes[i].X_Size_Value, boxes[i].Y_Size_Value);
        }
    }

    private void Function_Dropdown(Dropdown select)
    {
        for (int i = 0;  i < boxes.Count; i++)
        {
            if (boxes[i].Name == select.options[select.value].text)
            {
                X_Position.value = boxes[i].X_Position_Value;
                Y_Position.value = boxes[i].Y_Position_Value;
                X_Size.value = boxes[i].X_Size_Value;
                Y_Size.value = boxes[i].Y_Size_Value;
            }
        }
    }

    public void setDropDownOpthions()
    {
        dropdown.options.Clear();
        for(int i = 0; i < boxes.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = boxes[i].Name;
            dropdown.options.Add(option);
            dropdown.value++;
            objects.Add(Instantiate(p_box, this.transform.position, this.transform.rotation, t_boxes.transform).GetComponent<RectTransform>());
        }
    }

    public void addDropDownOpthions()
    {
        Dropdown.OptionData option = new Dropdown.OptionData(); 
        option.text = "Box" + dropdown.options.Count.ToString();
        dropdown.options.Add(option);
        dropdown.value++;

        objects.Add(Instantiate(p_box, this.transform.position, this.transform.rotation, t_boxes.transform).GetComponent<RectTransform>());

        BoxData box = new BoxData();
        box.Name = option.text;
        boxes.Add(box);
        Function_Dropdown(dropdown);
    }

    public void removeDropDownOpthions()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            if (boxes[i].Name == dropdown.options[dropdown.value].text)
            {
                dropdown.options.RemoveAt(i);
                boxes.RemoveAt(i);
                dropdown.value--;

                Destroy(objects[i].transform.gameObject);
                objects.RemoveAt(i);

                for(int j = i; j < boxes.Count; j++)
                {
                    Dropdown.OptionData option = new Dropdown.OptionData();
                    option.text = "Box" + j.ToString();
                    dropdown.options[j].text = option.text;
                    boxes[j].Name = option.text;
                }
            }
        }
    }
}
