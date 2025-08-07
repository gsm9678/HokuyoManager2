using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DataFormat data = new DataFormat();
    [SerializeField] OSCManager m_OSCManager;

    string path;

    public bool isStarted = false;

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "database.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        if (!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            data = JsonUtility.FromJson<DataFormat>(loadJson);

            if (data != null)
            {
                GameManager.instance.data = data;
            }
        }
        isStarted = true;
    }

    public void JsonSave()
    {
        Invoke("SaveFunc", 0.5f);
    }

    void SaveFunc()
    {
        data = GameManager.instance.data;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
}
