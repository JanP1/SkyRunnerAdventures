using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public SaveSystem saveSystem;
    public LoadSystem loadSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveSystem = gameObject.AddComponent<SaveSystem>();
            loadSystem = gameObject.AddComponent<LoadSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(TwoFieldArray newData)
    {
        saveSystem.SaveData(newData);
    }

    public TwoFieldArrayList LoadData()
    {
        return loadSystem.LoadData();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (Instance == null)
        {
            GameObject dataManager = new GameObject("DataManager");
            dataManager.AddComponent<DataManager>();
        }
    }
}
