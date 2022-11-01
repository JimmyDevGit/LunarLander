using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Worm : MonoBehaviour
{
    private int rand1;
    private int rand2;
    private int rand3;
    public Light2D wormLight;
    public SpriteRenderer wormSprite;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rand1 = Random.Range(0, 255);
        rand2 = Random.Range(0, 255);
        rand3 = Random.Range(0, 255);
        wormLight.color = new Color32((byte)rand1, (byte)rand2, (byte)rand3, 255);
        wormSprite.color = new Color32((byte)rand1, (byte)rand2, (byte)rand3, 255);
    }
}
