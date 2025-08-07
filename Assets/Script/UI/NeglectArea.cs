using System.Collections.Generic;
using UnityEngine;

public class NeglectArea : MonoBehaviour
{
    [SerializeField] GameObject p_NeglectArea;
    [SerializeField] Transform t_Box;
    private List<NeglectAreaModel> _neglectAreaModels;
    private List<RectTransform> NeglectAreaObjects;

    // Start is called before the first frame update
    void Start()
    {
        _neglectAreaModels = GameManager.instance.data.NeglectAreas;
        NeglectAreaObjects = new List<RectTransform>();

        GameManager.instance.NeglectAreaAction += PropertyChanged;
        Init_NeglectArea();
    }

    void PropertyChanged(string name, int i)
    {
        switch (name)
        {
            case "NeglectAreaAdd":
                Add_NeglectArea();
                break;
            case "NeglectAreaRemove":
                Remove_NeglectArea(i);
                break;
            default:
                Show_NeglectArea(i);
                break;
        }
    }

    void Init_NeglectArea()
    {
        for(int i = 0; i < _neglectAreaModels.Count; i++)
        {
            NeglectAreaObjects.Add(Instantiate(p_NeglectArea, t_Box.transform).GetComponent<RectTransform>());
            Show_NeglectArea(i);
        }
    }

    void Show_NeglectArea(int i)
    {
        NeglectAreaObjects[i].transform.localPosition = new Vector3(_neglectAreaModels[i].X_Position_Value, _neglectAreaModels[i].Y_Position_Value, 0);
        NeglectAreaObjects[i].sizeDelta = new Vector2(_neglectAreaModels[i].X_Size_Value, _neglectAreaModels[i].Y_Size_Value);
    }

    void Add_NeglectArea()
    {
        NeglectAreaObjects.Add(Instantiate(p_NeglectArea, t_Box.transform).GetComponent<RectTransform>());
        Show_NeglectArea(NeglectAreaObjects.Count - 1);
    }

    void Remove_NeglectArea(int i)
    {
        Destroy(NeglectAreaObjects[i].transform.gameObject);
        NeglectAreaObjects.RemoveAt(i);
    }
}
