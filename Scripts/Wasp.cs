using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;
    float tempx;
    float tempy;
    bool startPos;
    // Start is called before the first frame update
    void Start()
    {
        tempx = transform.position.x;
        tempy = transform.position.y;
        startPos = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 0;
        if (startPos == false)
        {
            transform.position = new Vector3(x + tempx, y + tempy, z);
        }
        else
        {
            transform.position = new Vector3(x, y, z);
        }

    }
}
