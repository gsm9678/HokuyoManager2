using System.ComponentModel;

public class MapSizeViewModel : INotifyPropertyChanged
{
    private RoomSizeDataModel _roomSizeDataModel;

    public MapSizeViewModel()
    {
        _roomSizeDataModel = GameManager.instance.data.RoomSizeData;
    }

    #region Property
    public float X_Size_Value
    {
        get { return _roomSizeDataModel.X_Size_Value; }
        set
        {
            if (_roomSizeDataModel.X_Size_Value != value)
            {
                _roomSizeDataModel.X_Size_Value = value;
                OnPropertyChanged("X_SizeValue");
            }
        }
    }

    public float Y_Size_Value
    {
        get { return _roomSizeDataModel.Y_Size_Value; }
        set
        {
            if(_roomSizeDataModel.Y_Size_Value != value)
            {
                _roomSizeDataModel.Y_Size_Value = value;
                OnPropertyChanged("Y_SizeValue");
            }
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        GameManager.instance.AreaSizeAction?.Invoke();
    }
}
