using System.ComponentModel;

public class ScaleSizeViewModel : INotifyPropertyChanged
{
    private ScaleSizeDataModel _scaleSizeDataModel;

    public ScaleSizeViewModel()
    {
        _scaleSizeDataModel = GameManager.instance.data.ScaleSizeData;
    }

    #region Property
    public float Epsilon
    {
        get { return _scaleSizeDataModel.Epsilon; }
        set
        {
            if(_scaleSizeDataModel.Epsilon != value)
            {
                _scaleSizeDataModel.Epsilon = value;
                OnPropertyChanged("Epsilon");
            }
        }
    }

    public float Min_Point
    {
        get { return _scaleSizeDataModel.Min_Point; }
        set
        {
            if(_scaleSizeDataModel.Min_Point != value)
            {
                _scaleSizeDataModel.Min_Point = value;
                OnPropertyChanged("Min_Point");
            }
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
