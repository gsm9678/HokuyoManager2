using System.Collections.Generic;
using System.ComponentModel;

public class OSCSettingViewModel : INotifyPropertyChanged
{
    private List<OSCSettingModel> _OSCSettingModels;

    public OSCSettingViewModel()
    {
        _OSCSettingModels = GameManager.instance.data.OSCSettings;
    }

    #region Property
    public int ModelCount
    {
        get { return _OSCSettingModels.Count; }
    }

    public string OSC_IP_Address
    {
        get
        {
            if (_OSCSettingModels.Count != 0)
                return _OSCSettingModels[ModelListNum].OSC_IP_Address;
            else return null;
        }
        set
        {
            if (_OSCSettingModels.Count != 0)
            {
                if (_OSCSettingModels[ModelListNum].OSC_IP_Address != value)
                {
                    _OSCSettingModels[ModelListNum].OSC_IP_Address = value;
                    OnPropertyChanged("OSC_IP_Adress");
                }
            }
        } 
    }

    public int OSC_IP_Port
    {
        get
        {
            if (_OSCSettingModels.Count != 0)
                return _OSCSettingModels[ModelListNum].OSC_Port;
            else return 0;
        }
        set
        {
            if (_OSCSettingModels.Count != 0)
            {
                if (_OSCSettingModels[ModelListNum].OSC_Port != value)
                {
                    _OSCSettingModels[ModelListNum].OSC_Port = value;
                    OnPropertyChanged("OSC_Port");
                }
            }
        }
    }
    
    public string OSC_Message_Address
    {
        get { return GameManager.instance.data.OSC_Message_Address; }
        set
        {
            if (GameManager.instance.data.OSC_Message_Address != value)
            {
                GameManager.instance.data.OSC_Message_Address = value;
                OnPropertyChanged("OSC_Message_Address");
            }
        }
    }
    #endregion

    #region ListManage
    public int ModelListNum = 0;

    public void Add_Model()
    {
        OSCSettingModel model = new OSCSettingModel();
        _OSCSettingModels.Add(model);

        OnPropertyChanged("NeglectAreaAdd");
    }

    public void Remove_Model()
    {
        _OSCSettingModels.RemoveAt(ModelListNum);

        OnPropertyChanged("NeglectAreaRemove");
    }
    #endregion

    public void Connect_OSC()
    {
        OnPropertyChanged("Connect_OSC");
    }

    public void Disconnect_OSC()
    {
        OnPropertyChanged("Disconnect_OSC");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
