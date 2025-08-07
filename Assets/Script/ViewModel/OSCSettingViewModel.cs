using System.ComponentModel;

public class OSCSettingViewModel : INotifyPropertyChanged
{
    private OSCSettingModel _OSCSettingModel;

    public OSCSettingViewModel()
    {
        _OSCSettingModel = GameManager.instance.data.OSCSetting;
    }

    #region Property
    public string OSC_IP_Address
    {
        get { return _OSCSettingModel.OSC_IP_Address; }
        set
        {
            if(_OSCSettingModel.OSC_IP_Address != value)
            {
                _OSCSettingModel.OSC_IP_Address = value;
                OnPropertyChanged("OSC_IP_Adress");
            }
        }
    }

    public int OSC_IP_Port
    {
        get { return _OSCSettingModel.OSC_Port; }
        set
        {
            if(_OSCSettingModel.OSC_Port != value)
            {
                _OSCSettingModel.OSC_Port = value;
                OnPropertyChanged("OSC_Port");
            }
        }
    }
    
    public string OSC_Message_Address
    {
        get { return _OSCSettingModel.OSC_Message_Address; }
        set
        {
            if (_OSCSettingModel.OSC_Message_Address != value)
            {
                _OSCSettingModel.OSC_Message_Address = value;
                OnPropertyChanged("OSC_Message_Address");
            }
        }
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
