using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public float speed;
    public bool walkRightOnStart;
    public int waitTime;

    private float targetPointLeft;
    private float targetPointRight;
    private float xMovement;
    private bool walkingRight;
    private bool facingRight;
    private bool walkingLeft;
    private bool facingLeft;
    private bool waitRightCalled = true;
    private bool waitLeftCalled = true;

    private Animator anim;
    private AudioSource audioSource;

    private float lastCallTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPointLeft = transform.parent.transform.GetChild(2).transform.position.x;
        targetPointRight = transform.parent.transform.GetChild(1).transform.position.x;
        audioSource = GetComponent<AudioSource>();
        if(walkRightOnStart == true)
        {
            WalkingRight();
        }
        if(walkRightOnStart == false)
        {
            WalkingLeft();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(walkingRight == true)
        {
            xMovement = transform.position.x + speed * Time.deltaTime;
        }
        if(walkingLeft == true)
        {
            xMovement = transform.position.x + -speed * Time.deltaTime;
        }
        if(facingRight == true)
        {
            xMovement = transform.position.x;
        }
        if(facingLeft == true)
        {
            xMovement = transform.position.x;
        }
        gameObject.transform.position = new Vector3(xMovement, transform.position.y, 0);
        
        if(Time.time - lastCallTime >= 0.2f)
        {
            SlowUpdate();
        }
    }
    //Updates 5 times per second
    private void SlowUpdate()
    {
        lastCallTime = Time.time;
        if (transform.position.x >= targetPointRight && waitRightCalled == false)
        {
            waitRightCalled = true;
            StartCoroutine(WaitRight());
        }
        if (transform.position.x <= targetPointLeft && waitLeftCalled == false)
        {
            waitLeftCalled = true;
            StartCoroutine(WaitLeft());
        }
    }
    private void WalkingRight()
    {
        audioSource.Play();
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        walkingRight = true;
        waitRightCalled = false;
    }
    private void WalkingLeft()
    {
        audioSource.Play();
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
