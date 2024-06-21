using System.Collections.Generic;

[System.Serializable]
public class TwoFieldArrayList
{
    public List<TwoFieldArray> List;

    public TwoFieldArrayList()
    {
        List = new List<TwoFieldArray>();
    }

    public void AddValue(string field1, string field2, int maxCount)
    {
        var newItem = new TwoFieldArray(field1, field2);

        if (List.Count < maxCount)
        {
            List.Add(newItem);
        }
        else
        {
            // Sort the list first so that the smallest time is at index 0
            List.Sort();
            // Get the smallest time
            TwoFieldArray smallestTime = List[0];
            // Compare with the new time
            if (newItem.CompareTo(smallestTime) > 0)
            {
                // Remove the smallest and add the new item
                List.RemoveAt(0);
                List.Add(newItem);
            }
        }
        // Sort the list after adding new item
        List.Sort();
    }

    public TwoFieldArray GetSmallestTime()
    {
        if (List.Count == 0)
            return null;

        TwoFieldArray smallest = List[0];
        foreach (var item in List)
        {
            if (item.CompareTo(smallest) < 0)
                smallest = item;
        }
        return smallest;
    }

    public void Remove(TwoFieldArray item)
    {
        List.Remove(item);
    }
}
