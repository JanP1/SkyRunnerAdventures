using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxMovement : MonoBehaviour
{
    private float length, startPos;
    public GameObject camera1;
    public float paralaxEffect;
    public float yOffset = -3.58f;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = camera1.transform.position.x * (1 - paralaxEffect);


        float distance = camera1.transform.position.x * paralaxEffect;
        transform.position = new Vector3(startPos + distance, camera1.transform.position.y + yOffset, transform.position.z);


        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
