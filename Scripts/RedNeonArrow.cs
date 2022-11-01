using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RedNeonArrow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private float randWait;
    private float randWaitOffTime;
    public GameObject sparksGold;
    public GameObject sparksBlue;
    public Vector3 sparkLocation;
    public Vector3 sparkLocation2;
    public Light2D light1;
    // Start is called before the first frame update
    void Start()
    {
        sparkLocation = transform.GetChild(1).transform.position;
        if(transform.childCount >2)
        {
            sparkLocation2 = transform.GetChild(2).transform.position;
        }
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Flashing()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        light1.enabled = true;
        Instantiate(sparksGold, sparkLocation, Quaternion.identity);
        randWaitOffTime = Random.Range(0.02f, 01f);
        yield return new WaitForSeconds(randWaitOffTime);
        light1.enabled = false;
        Instantiate(sparksBlue, sparkLocation2, Quaternion.identity);
        spriteRenderer.color = new Color32(0, 0, 0, 255);
        randWait = Random.Range(.0f, .5f);
        yield return new WaitForSeconds(randWait);
        StartCoroutine(Flashing());
    }
}
