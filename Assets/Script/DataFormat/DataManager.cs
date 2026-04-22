using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    DataFormat data = new DataFormat();

    private static DataManager instance;

    string path;

    public static string DatabasePath
    {
        get { return Path.Combine(Application.dataPath, "database.json"); }
    }

    public static bool TryLoad(out DataFormat loadedData)
    {
        loadedData = null;
        string loadPath = DatabasePath;

        if (!File.Exists(loadPath))
            return false;

        string loadJson = File.ReadAllText(loadPath);
        loadedData = JsonUtility.FromJson<DataFormat>(loadJson);
        return loadedData != null;
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        path = DatabasePath;
        JsonLoad();
    }

    public void JsonLoad()
    {
        if (string.IsNullOrEmpty(path))
            path = DatabasePath;

        if (!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            if (TryLoad(out data))
            {
                GameManager.instance.data = data;
            }
        }
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
