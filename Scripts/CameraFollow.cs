using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public Rigidbody2D playerRB;
    public Vector3 targetExplosion;
    public float smoothTime = .3F;
    private Vector3 velocity = Vector3.zero;
    public bool paused;
    public float shakeX;
    public float shakeY;
    public List<float> previousMagnitude;
    float crashSeverity;
    private bool deathShakeOver = false;

    private void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        target = playerRB.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null && paused == false)
        {
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
            transform.position = Vector3.SmoothDamp(new Vector3(transform.position.x +shakeX + playerRB.velocity.x /22, transform.position.y +shakeY + playerRB.velocity.y/25, transform.position.z), targetPosition, ref velocity, smoothTime);
        }
        if(player == null)
        {
            if(deathShakeOver == false)
            {
                StartCoroutine(DeathShakeTimer());
                DeathShake();
            }
            targetExplosion = GameObject.Find("ShipExplosion(Clone)").transform.position;
        }
        if(paused == true)
        {
            transform.position = transform.position;
        }

        if(player != null)
        previousMagnitude.Insert(0,playerRB.velocity.magnitude);
        if(previousMagnitude.Count > 10)
        {
            previousMagnitude.RemoveAt(10);
        }
        if(previousMagnitude.Count > 9)
        {
            crashSeverity = previousMagnitude[5] / 100;
        }
    }
    public IEnumerator CameraShake()
    {
        if(player != null)
        {
            shakeX = crashSeverity;
            shakeY = crashSeverity;
            yield return new WaitForSeconds(0.1f);
            shakeX = -crashSeverity;
            shakeY = -crashSeverity;
            yield return new WaitForSeconds(0.1f);
            shakeX = crashSeverity;
            shakeY = crashSeverity;
            yield return new WaitForSeconds(0.1f);
            shakeX = crashSeverity;
            shakeY = crashSeverity;
            yield return new WaitForSeconds(0.1f);
            shakeX = crashSeverity;
            shakeY = crashSeverity;
            yield return new WaitForSeconds(0.1f);
            shakeX = crashSeverity;
            shakeY = crashSeverity;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator DeathShakeTimer()
    {
        yield return new WaitForSeconds(.4f);
        deathShakeOver = true;
        transform.position = new Vector3(targetExplosion.x, targetExplosion.y, transform.position.z);
    }
    private void DeathShake()
    {
        transform.position = targetExplosion + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), transform.position.z);
    }
}
