using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public static OSCManager instance;

    private OSCSettingModel _OSCSettingModel;

    public OSC _isOSC;

    string _name;

    private void Start()
    {
        if (instance == null)
            instance = this;

        _OSCSettingModel = GameManager.instance.data.OSCSetting;

        GameManager.instance.OSCManageAction += PropertyChanged;

        AwakeOSC();
    }

    void PropertyChanged(string name)
    {
        switch (name)
        {
            case "OSC_Message_Address":
                setName();
                break;
            case "Connect_OSC":
                OSCConnect();
                break;
            case "Disconnect_OSC":
                OSCDiconnect();
                break;
        }
    }

    void AwakeOSC()
    {
        _name = "/" + _OSCSettingModel.OSC_Message_Address;
        _isOSC.outIP = _OSCSettingModel.OSC_IP_Address;
        _isOSC.outPort = _OSCSettingModel.OSC_Port;
        _isOSC.gameObject.SetActive(true);
    }

    public void setName()
    {
        _name = "/" + _OSCSettingModel.OSC_Message_Address;
    }

    public void OSCConnect()
    {
        _isOSC.outIP = _OSCSettingModel.OSC_IP_Address;
        _isOSC.outPort = _OSCSettingModel.OSC_Port;
        _isOSC.Open();
    }

    public void OSCDiconnect()
    {
        QuitMessage();
        _isOSC.Close();
    }

    public void StartMessage(Vector2 vector2)
    {
        if(!_isOSC.isRunning())
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = _name + "/Start";
        message.values.Add(vector2.x);
        message.values.Add(vector2.y);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void SensorMessage(Vector3 vector2)
    {
        if (!_isOSC.isRunning())
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = _name + "/Data";
        message.values.Add(vector2.x);
        message.values.Add(vector2.y);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void EndMessage()
    {
        if (!_isOSC.isRunning())
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = _name + "/End";
        message.values.Add(1);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void QuitMessage()
    {
        if (!_isOSC.isRunning())
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = _name + "/Quit";
        message.values.Add(1);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void OnApplicationQuit()
    {
        QuitMessage();
    }
}
