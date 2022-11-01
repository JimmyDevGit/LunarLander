using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LandingPadLights : MonoBehaviour
{
    public Sprite lightsOn;
    public Sprite lightsOff;
    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;
    public Light2D Light1;
    public Light2D Light2;
    public Light2D Light3;
    public Light2D Light4;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeSprite());
    }
    IEnumerator ChangeSprite()
    {
        if(Light1 != null)
        {
            Light1.enabled = true;
            Light2.enabled = true;
            Light3.enabled = true;
            Light4.enabled = true;
            rightSprite.sprite = lightsOn;
            leftSprite.sprite = lightsOn;
            yield return new WaitForSeconds(.5f);
            Light1.enabled = false;
            Light2.enabled = false;
            Light3.enabled = false;
            Light4.enabled = false;
            leftSprite.sprite = lightsOff;
            rightSprite.sprite = lightsOff;
            yield return new WaitForSeconds(.5f);
            StartCoroutine(ChangeSprite());
        }
    }
}
