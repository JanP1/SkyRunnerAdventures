using System;

[System.Serializable]
public class TwoFieldArray : IComparable<TwoFieldArray>
{
    public string Field1; // Time value in "0:00" format
    public string Field2;

    public TwoFieldArray(string field1, string field2)
    {
        Field1 = field1;
        Field2 = field2;
    }

    public int CompareTo(TwoFieldArray other)
    {
        TimeSpan thisTime = TimeSpan.ParseExact(Field1, "m\\:ss", System.Globalization.CultureInfo.InvariantCulture);
        TimeSpan otherTime = TimeSpan.ParseExact(other.Field1, "m\\:ss", System.Globalization.CultureInfo.InvariantCulture);
        return thisTime.CompareTo(otherTime); // Ascending order
    }
}
