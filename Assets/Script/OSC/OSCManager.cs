using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public static OSCManager instance;

    private List<OSCSettingModel> _OSCSettings;

    [Header("OscOut ÇÁ¸®Æé")]
    [SerializeField] private OscOut OSC_ChannelPrefab;

    private List<OscOut> OscOuts = new List<OscOut>();
    private string OscAddress = string.Empty;

    private void Start()
    {
        if (instance == null)
            instance = this;

        _OSCSettings = GameManager.instance.data.OSCSettings;

        foreach (var oscOutLine in _OSCSettings)
        {
            OscOuts.Add(CreateOscOut(oscOutLine));
        }
        setName();
    }

    public OscOut CreateOscOut(OSCSettingModel oscLine)
    {
        OscOut temp = Instantiate(OSC_ChannelPrefab, this.transform);
        SetOSC(temp.GetComponent<OscOut>(), oscLine.OSC_Port, oscLine.OSC_IP_Address);
        temp.Open(oscLine.OSC_Port, oscLine.OSC_IP_Address);

        return temp;
    }

    private void SetOSC(OscOut osc, int outp, string ip = "127.0.0.1")
    {
        osc.port = outp;
        osc.remoteIpAddress = ip;
    }

    public void SendOSC(string Message, int i)
    {
        foreach (OscOut oscOut in OscOuts)
            oscOut.Send(Message, i);
    }

    public void setName()
    {
        OscAddress = "/" + GameManager.instance.data.OSC_Message_Address;
    }

    public void StartMessage(Vector2 vector2)
    {
        if (OscOuts.Count == 0)
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = OscAddress + "/Start";

        message.Add(vector2.x);
        message.Add(vector2.y);
        message.Add("");
        for (int i = 0; i < OscOuts.Count; i++) 
        {
            OscOuts[i].Send(message);
        }
    }

    public void SensorMessage(Vector3 vector2)
    {
        if (OscOuts.Count == 0)
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = OscAddress + "/Data";
        message.Add(vector2.x);
        message.Add(vector2.y);
        message.Add("");
        for (int i = 0; i < OscOuts.Count; i++)
        {
            OscOuts[i].Send(message);
        }
    }

    public void EndMessage()
    {
        if (OscOuts.Count == 0)
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = OscAddress + "/End";
        message.Add(1);
        message.Add("");
        for (int i = 0; i < OscOuts.Count; i++)
        {
            OscOuts[i].Send(message);
        }
    }

    public void QuitMessage()
    {
        if (OscOuts.Count == 0)
        {
            return;
        }
        OscMessage message = new OscMessage();
        message.address = OscAddress + "/Quit";
        message.Add(1);
        message.Add("");
        for (int i = 0; i < OscOuts.Count; i++)
        {
            OscOuts[i].Send(message);
        }
    }

    public void OnApplicationQuit()
    {
        QuitMessage();
    }
}
