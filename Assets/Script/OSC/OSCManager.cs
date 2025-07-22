using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public OSC _isOSC;

    string _name;

    public void setName(string s)
    {
        _name = "/" + s;
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

    public void SensorMessage(Vector3 vector3)
    {
        if (!_isOSC.isRunning())
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = _name + "/Data";
        message.values.Add(vector3.x);
        message.values.Add(vector3.y);
        message.values.Add("");
        _isOSC.Send(message);
    }

    public void StopMessage()
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


    public void OnApplicationQuit()
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
}
