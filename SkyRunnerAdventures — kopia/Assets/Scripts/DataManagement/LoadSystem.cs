using UnityEngine;
using System.IO;

public class LoadSystem : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public TwoFieldArrayList LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TwoFieldArrayList data = JsonUtility.FromJson<TwoFieldArrayList>(json);
            return data;
        }
        else
        {
            return new TwoFieldArrayList();
        }
    }
}
