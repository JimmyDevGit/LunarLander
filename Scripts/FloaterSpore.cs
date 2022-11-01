using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FloaterSpore : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float randDirection;
    private float currentDirection;
    private float newDirection = 0;
    private int rand1;
    private int rand2;
    private int rand3;
    public Light2D sporeLight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sporeLight = GetComponentInChildren<Light2D>();
        rand1 = Random.Range(0, 255);
        rand2 = Random.Range(0, 255);
        rand3 = Random.Range(0, 255);
        sporeLight.color = new Color32((byte)rand1, (byte)rand2, (byte)rand3, 255);
        StartCoroutine(RandDirectionInterval());
    }
    private void FixedUpdate()
    {
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
}
