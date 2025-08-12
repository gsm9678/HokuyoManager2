using System.Collections.Generic;
using System.ComponentModel;

public class SensorSettingViewModel : INotifyPropertyChanged
{
    private List<SensorSettingModel> _sensorSettingModels;

    public SensorSettingViewModel()
    {
        _sensorSettingModels = GameManager.instance.data.SensorSettingModels;
    }

    #region Property
    public int ModelCount
    {
        get {  return _sensorSettingModels.Count; }
    }

    public string Hokuyo_IP_Address
    {
        get
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].Hokuyo_IP_Address;
            else return "192.168.0.10";
        } 
        set
        {
            if (_sensorSettingModels.Count != 0)
            {
                if (_sensorSettingModels[ModelListNum].Hokuyo_IP_Address != value)
                {
                    _sensorSettingModels[ModelListNum].Hokuyo_IP_Address = value;
                    OnPropertyChanged("Hokuyo_IP_Address");
                }
            }
        }
    }

    public float Zoom_IN_OUT
    {
        get
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].Zoom_IN_OUT;
            else return 0;
        }
        set
        {
            if (_sensorSettingModels.Count != 0)
            {
                if (_sensorSettingModels[ModelListNum].Zoom_IN_OUT != value)
                {
                    _sensorSettingModels[ModelListNum].Zoom_IN_OUT = value;
                    OnPropertyChanged("Zoom_IN_OUT");
                }
            }
        }
    }

    public float X_Position
    {
        get 
        { 
            if (_sensorSettingModels.Count != 0) 
                return _sensorSettingModels[ModelListNum].X_Position; 
            else return 0;
        }
        set
        {
            if (_sensorSettingModels.Count != 0)
            {
                if (_sensorSettingModels[ModelListNum].X_Position != value)
                {
                    _sensorSettingModels[ModelListNum].X_Position = value;
                    OnPropertyChanged("X_Position");
                }
            }
        }
    }

    public float Y_Position
    {
        get 
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].Y_Position;
            else return 0;
        }
        set
        {
            if (_sensorSettingModels.Count != 0)
            {
                if (_sensorSettingModels[ModelListNum].Y_Position != value)
                {
                    _sensorSettingModels[ModelListNum].Y_Position = value;
                    OnPropertyChanged("Y_Position");
                }
            }
        }
    }

    public float Rotate_Camera_Value
    {
        get
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].Rotate_Camera_Value;
            else return 0;
        }
        set
        {
            if (_sensorSettingModels.Count != 0)
            {
                if (_sensorSettingModels[ModelListNum].Rotate_Camera_Value != value)
                {
                    _sensorSettingModels[ModelListNum].Rotate_Camera_Value = value;
                    OnPropertyChanged("Rotate_Camera_Value");
                }
            }
        }
    }

    public bool X_Flip
    {
        get
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].X_Flip;
            else return false;
        }
        set
        {
            if( _sensorSettingModels.Count != 0)
                if (_sensorSettingModels[ModelListNum].X_Flip != value)
                {
                    _sensorSettingModels[ModelListNum].X_Flip = value;
                    OnPropertyChanged("X_Flip");
                }
        }
    }

    public bool Y_Flip
    {
        get
        {
            if (_sensorSettingModels.Count != 0)
                return _sensorSettingModels[ModelListNum].Y_Flip;
            else return false;
        }
        set
        {
            if (_sensorSettingModels.Count != 0)
                if (_sensorSettingModels[ModelListNum].Y_Flip != value)
                {
                    _sensorSettingModels[ModelListNum].Y_Flip = value;
                    OnPropertyChanged("Y_Flip");
                }
        }
    }
    #endregion

    #region ListManage
    public int ModelListNum = 0;

    public void Add_SensorSettingModel()
    {
        SensorSettingModel model = new SensorSettingModel();
        _sensorSettingModels.Add(model);

        OnPropertyChanged("SensorAdd");
    }

    public void Remove_SensorSettingModel()
    {
        _sensorSettingModels.RemoveAt(ModelListNum);

        OnPropertyChanged("SensorRemove");
    }
    #endregion

    public void Connect_Sensor()
    {
        OnPropertyChanged("SensorConnect");
    }

    public void Disconnect_Sensor()
    {
        OnPropertyChanged("SensorDisconnect");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        GameManager.instance.SensorManageAction?.Invoke(propertyName, ModelListNum);
    }
}
