using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SlowSign : MonoBehaviour
{
    public SpriteRenderer[] childrenSprites;
    private float randWait;
    private float randWaitOffTime;
    public GameObject sparksGold;
    public GameObject sparksBlue;
    public Vector3 sparkLocation;
    public Light2D light1;
    public Light2D light2;
    public Light2D light3;
    public Light2D light4;
    public Light2D light5;
    public Light2D light6;
    // Start is called before the first frame update
    void Start()
    {
        childrenSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        sparkLocation = gameObject.transform.parent.GetChild(gameObject.transform.GetSiblingIndex() + 1).transform.position;
        StartCoroutine(Flashing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Flashing()
    {
        for (int i = 0; i < childrenSprites.Length; i++)
        {
            childrenSprites[i].enabled = false;
        }
        light1.enabled = false;
        light2.enabled = false;
        light3.enabled = false;
        light4.enabled = false;
        if(light5 != null)
        light5.enabled = false;
        if (light6 != null)
            light6.enabled = false;
        Instantiate(sparksBlue, sparkLocation, Quaternion.identity);
        randWaitOffTime = Random.Range(0.02f, 0.2f);
        yield return new WaitForSeconds(randWaitOffTime);
        for (int i = 0; i < childrenSprites.Length; i++)
        {
            childrenSprites[i].enabled = true;
        }
        light1.enabled = true;
        light2.enabled = true;
        light3.enabled = true;
        light4.enabled = true;
        if (light5 != null)
            light5.enabled = true;
        if (light6 != null)
            light6.enabled = true;
        randWait = Random.Range(.0f, 1f);
        yield return new WaitForSeconds(randWait);
        StartCoroutine(Flashing());
    }
}
