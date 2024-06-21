using UnityEngine;

public class PowerUpActivation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;  // Array to hold references to ob1, ob2, and ob3
    private int activeIndex;      // Index of the active object

    void Start()
    {
        // Deactivate all objects initially
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        // Activate one random object
        activeIndex = Random.Range(0, objects.Length);
        objects[activeIndex].SetActive(true);
    }

    // Method to get the active object's index
    public int GetActiveIndex()
    {
        return activeIndex;
    }
}
