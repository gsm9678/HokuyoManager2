using UnityEngine;

public class AreaSize : MonoBehaviour
{
    private RoomSizeDataModel _roomSizeDataModel;

    [SerializeField] RectTransform Map;

    private void Start()
    {
        _roomSizeDataModel = GameManager.instance.data.RoomSizeData;
        GameManager.instance.AreaSizeAction += MapUpdate;
    }

    private void MapUpdate()
    {
        Map.sizeDelta = new Vector2(_roomSizeDataModel.X_Size_Value, _roomSizeDataModel.Y_Size_Value);
    }
}
