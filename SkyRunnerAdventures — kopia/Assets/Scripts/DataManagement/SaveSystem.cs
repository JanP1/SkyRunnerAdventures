/*using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void SaveData(TwoFieldArray newData)
    {
        TwoFieldArrayList existingData = new TwoFieldArrayList();

        // Load existing data, if any
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            existingData = JsonUtility.FromJson<TwoFieldArrayList>(json);
        }

        // If there's space, add the new data
        if (existingData.List.Count < 10)
        {
            existingData.AddValue(newData.Field1, newData.Field2, 10);
        }
        else
        {
            // Find the smallest time in the existing data
            TwoFieldArray smallestTime = existingData.GetSmallestTime();

            // If the new data has a smaller time, replace it
            if (newData.CompareTo(smallestTime) > 0)
            {
                existingData.Remove(smallestTime);
                existingData.AddValue(newData.Field1, newData.Field2, 10);
            }
        }

        // Save the updated list
        string combinedJson = JsonUtility.ToJson(existingData);
        File.WriteAllText(filePath, combinedJson);
    }
}
*/


using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void SaveData(TwoFieldArray newData)
    {
        TwoFieldArrayList existingData = new TwoFieldArrayList();

        // Load existing data, if any
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            existingData = JsonUtility.FromJson<TwoFieldArrayList>(json);
        }

        // If there's space, add the new data
        if (existingData.List.Count < 10)
        {
            existingData.AddValue(newData.Field1, newData.Field2, 10);
        }
        else
        {
            // Find the smallest time in the existing data
            TwoFieldArray smallestTime = existingData.GetSmallestTime();

            // If the new data has a smaller time, replace it
            if (newData.CompareTo(smallestTime) > 0)
            {
                existingData.Remove(smallestTime);
                existingData.AddValue(newData.Field1, newData.Field2, 10);
            }
        }

        // Reverse the list order before saving
        existingData.List.Reverse();

        // Save the updated list
        string combinedJson = JsonUtility.ToJson(existingData);
        File.WriteAllText(filePath, combinedJson);
    }
}

