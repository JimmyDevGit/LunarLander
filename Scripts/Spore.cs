using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float randDirection;
    private float currentDirection;
    private float newDirection = 0;
    public GameObject pinkShroom;
    private bool startPeriod = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartPeriod());
    }
    private void FixedUpdate()
    {
        /*if (startPeriod == true)
        {
            rb.AddForce(new Vector2(Random.Range(-50f,50f),1*speed));
        }
        else
        {
            rb.AddForce(transform.up * -speed);
            rb.transform.Rotate(0, 0, newDirection);
        }*/
        rb.AddForce(transform.up * -speed);
        rb.transform.Rotate(0, 0, newDirection);
    }
    IEnumerator RandDirectionInterval()
    {
        currentDirection = rb.transform.rotation.z;
        randDirection = Random.Range(-1, 1);
        StartCoroutine(RandDirection(currentDirection, randDirection, 2f));
        yield return new WaitForSeconds(2);
        StartCoroutine(RandDirectionInterval());
    }
    IEnumerator RandDirection(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
                newDirection = Mathf.Lerp(v_start, v_end, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
        }
        newDirection = v_end;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if (gameObject.tag == "Spore")
            {
                Instantiate(pinkShroom, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            if (gameObject.tag == "SporeDud")
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator StartPeriod()
    {
        yield return new WaitForSeconds(Random.Range(2,5));
        startPeriod = false;
        StartCoroutine(RandDirectionInterval());
    }
}
