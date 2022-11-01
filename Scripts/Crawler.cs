using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Crawler : MonoBehaviour
{
    public float speed;
    public bool walkUpOnStart;
    public int waitTime;
    private bool walkingRight;
    private bool facingRight;
    private bool walkingLeft;
    private bool facingLeft;
    private float yMovement;
    private float targetPointTop;
    private float targetPointBottom;
    private bool waitRightCalled = true;
    private bool waitLeftCalled = true;
    private Animator anim;
    private AudioSource audioSource;

    private int rand1;
    private int rand2;
    private int rand3;
    private Light2D crawlerLight;
    private SpriteRenderer crawlerSprite;

    private float lastCallTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPointTop = transform.parent.transform.GetChild(1).transform.position.y;
        targetPointBottom = transform.parent.transform.GetChild(2).transform.position.y;
        audioSource = GetComponent<AudioSource>();

        crawlerLight = GetComponentInChildren<Light2D>();
        crawlerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rand1 = Random.Range(0, 255);
        rand2 = Random.Range(0, 255);
        rand3 = Random.Range(0, 255);
        crawlerLight.color = new Color32((byte)rand1, (byte)rand2, (byte)rand3, 255);
        crawlerSprite.color = new Color32((byte)rand1, (byte)rand2, (byte)rand3, 255);

        if (walkUpOnStart == true)
        {
            WalkingRight();
        }
        if (walkUpOnStart == false)
        {
            WalkingLeft();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingRight == true)
        {
            yMovement = transform.position.y + speed * Time.deltaTime;
        }
        if (walkingLeft == true)
        {
            yMovement = transform.position.y + -speed * Time.deltaTime;
        }
        if (facingRight == true)
        {
            yMovement = transform.position.y;
        }
        if (facingLeft == true)
        {
            yMovement = transform.position.y;
        }
        gameObject.transform.position = new Vector3(transform.position.x, yMovement , 0);
        
        if (Time.time - lastCallTime >= 0.2f)
        {
            SlowUpdate();
        }
    }
    private void SlowUpdate()
    {
        if (transform.position.y >= targetPointTop && waitRightCalled == false)
        {
            waitRightCalled = true;
            StartCoroutine(WaitRight());
        }
        if (transform.position.y <= targetPointBottom && waitLeftCalled == false)
        {
            waitLeftCalled = true;
            StartCoroutine(WaitLeft());
        }
    }
    private void WalkingRight()
    {
        audioSource.Play();
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        walkingRight = true;
        waitRightCalled = false;
    }
    private void WalkingLeft()
    {
        audioSource.Play();
        gameObject.transform.rotation = Quaternion.Euler(180, 0, 90);
        facingRight = false;
        walkingLeft = true;
        waitLeftCalled = false;
    }
    IEnumerator WaitRight()
    {
        audioSource.Stop();
        anim.SetTrigger("IdleRight");
        walkingRight = false;
        facingRight = true;
        yield return new WaitForSeconds(waitTime);
        anim.SetTrigger("WalkingRight");
        WalkingLeft();
    }
    IEnumerator WaitLeft()
    {
        audioSource.Stop();
        anim.SetTrigger("IdleRight");
        walkingLeft = false;
        facingLeft = true;
        yield return new WaitForSeconds(waitTime);
        anim.SetTrigger("WalkingRight");
        facingLeft = false;
        WalkingRight();
    }
}
