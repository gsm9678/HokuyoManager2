using System.Collections.Generic;
using System.ComponentModel;

public class NeglectAreaSettingViewModel : INotifyPropertyChanged
{
    private List<NeglectAreaModel> _neglectAreaModels;

    public NeglectAreaSettingViewModel()
    {
        _neglectAreaModels = GameManager.instance.data.NeglectAreas;
    }

    #region Property
    public int ModelCount
    {
        get { return _neglectAreaModels.Count; }
    }

    public float X_Position_Value
    {
        get
        {
            if (_neglectAreaModels.Count != 0)
                return _neglectAreaModels[ModelListNum].X_Position_Value;
            else return 0;
        }
        set
        {
            if (_neglectAreaModels.Count != 0)
            {
                if (_neglectAreaModels[ModelListNum].X_Position_Value != value)
                {
                    _neglectAreaModels[ModelListNum].X_Position_Value = value;
                    OnPropertyChanged("X_Position_Value");
                }
            }
        }
    }

    public float Y_Position_Value
    {
        get
        {
            if (_neglectAreaModels.Count != 0)
                return _neglectAreaModels[ModelListNum].Y_Position_Value;
            else return 0;
        }
        set
        {
            if (_neglectAreaModels.Count != 0)
            {
                if (_neglectAreaModels[ModelListNum].Y_Position_Value != value)
                {
                    _neglectAreaModels[ModelListNum].Y_Position_Value = value;
                    OnPropertyChanged("Y_Position_Value");
                }
            }
        }
    }

    public float X_Size_Value
    {
        get
        {
            if (_neglectAreaModels.Count != 0)
                return _neglectAreaModels[ModelListNum].X_Size_Value;
            else return 0;
        }
        set
        {
            if (_neglectAreaModels.Count != 0)
            {
                if (_neglectAreaModels[ModelListNum].X_Size_Value != value)
                {
                    _neglectAreaModels[ModelListNum].X_Size_Value = value;
                    OnPropertyChanged("X_Size_Value");
                }
            }
        }
    }

    public float Y_Size_Value
    {
        get
        {
            if (_neglectAreaModels.Count != 0)
                return _neglectAreaModels[ModelListNum].Y_Size_Value;
            else return 0;
        }
        set
        {
            if (_neglectAreaModels.Count != 0)
            {
                if (_neglectAreaModels[ModelListNum].Y_Size_Value != value)
                {
                    _neglectAreaModels[ModelListNum].Y_Size_Value = value;
                    OnPropertyChanged("Y_Size_Value");
                }
            }
        }
    }
    #endregion

    #region ListManage
    public int ModelListNum = 0;

    public void Add_Model()
    {
        NeglectAreaModel model = new NeglectAreaModel();
        _neglectAreaModels.Add(model);

        OnPropertyChanged("NeglectAreaAdd");
    }

    public void Remove_Model()
    {
        _neglectAreaModels.RemoveAt(ModelListNum);

        OnPropertyChanged("NeglectAreaRemove");
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        GameManager.instance.NeglectAreaAction?.Invoke(propertyName, ModelListNum);
    }
}
