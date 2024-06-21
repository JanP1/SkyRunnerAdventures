using UnityEngine;
using TMPro;

public class TextUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private DataManager dataManager;

    private void Start()
    {
        dataManager = DataManager.Instance;
        UpdateText();
    }

    public void UpdateText()
    {
        TwoFieldArrayList dataList = dataManager.LoadData();
        string combinedText = "";

        foreach (var item in dataList.List)
        {
            combinedText += $"{item.Field1} - {item.Field2}\n";
        }

        textMeshPro.text = combinedText;
    }
}
