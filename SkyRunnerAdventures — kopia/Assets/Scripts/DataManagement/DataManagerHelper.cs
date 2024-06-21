using UnityEngine;

public class DataManagerHelper : MonoBehaviour
{
    public static DataManagerHelper Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddNewData(string time, string value)
    {
        TwoFieldArray newData = new TwoFieldArray(time, value);
        DataManager.Instance.SaveData(newData);
    }
}
