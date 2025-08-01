using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SensorSettingViewModel : INotifyPropertyChanged
{
    public List<SensorSettingModel> _sensorSettingModels;

    public SensorSettingViewModel()
    {
        _sensorSettingModels = new List<SensorSettingModel>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
