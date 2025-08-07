using System.ComponentModel;

public class FlipCheckViewModel : INotifyPropertyChanged
{
    private FlipDataModel _flipDataModel;

    public FlipCheckViewModel()
    {
        _flipDataModel = GameManager.instance.data.FlipData;
    }

    #region Property
    public bool X_Flip
    {
        get { return _flipDataModel.X_Flip; }
        set
        {
            if( _flipDataModel.X_Flip != value )
            {
                _flipDataModel.X_Flip = value;
                OnPropertyChanged("X_Flip");
            }
        }
    }

    public bool Y_Flip
    {
        get { return _flipDataModel.Y_Flip; }
        set
        {
            if (_flipDataModel.Y_Flip != value)
            {
                _flipDataModel.Y_Flip = value;
                OnPropertyChanged("Y_Flip");
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
