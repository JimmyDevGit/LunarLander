using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public Transform thruster;
    public GameObject dust;
    public GameObject dustRight;
    public GameObject dustLeft;
    private bool dustReady = true;
    private int currentFuel;
    private float vineAddForceXDirection;
    public int vinePushForce;
    public PlayerController playerController;
    private void Start()
    {
        vinePushForce = 20;
    }
    // Update is called once per frame
    void Update()
    {
        currentFuel = playerController.currentFuel;
        if(currentFuel > 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                ThrustRay();
            }
        }
    }
    public void ThrustRay()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(thruster.position, thruster.right, 2);

        if (hitInfo)
        {
            if (hitInfo.collider.gameObject.tag == "VineSegment")
            {
                if (gameObject.transform.rotation.eulerAngles.z - 270 > -90 && gameObject.transform.rotation.eulerAngles.z - 270 < 90)
                {
                    vineAddForceXDirection = (gameObject.transform.rotation.eulerAngles.z - 270) / 10;
                    hitInfo.rigidbody.AddForce(new Vector2(vineAddForceXDirection, -1) * vinePushForce);
                }
            }

            if (dustReady == true)
            {
                if (gameObject.transform.rotation.eulerAngles.z - 270 > 20)
                {
                    Instantiate(dustRight, hitInfo.point, Quaternion.identity);
                    StartCoroutine(DustCooldown());
                }
                else if (gameObject.transform.rotation.eulerAngles.z - 270 < -20)
                {
                    Instantiate(dustLeft, hitInfo.point, Quaternion.identity);
                    StartCoroutine(DustCooldown());
                }
                else
                {
                    Instantiate(dust, hitInfo.point, Quaternion.identity);
                    StartCoroutine(DustCooldown());
                }
            }
        }
    }
    IEnumerator DustCooldown()
    {
        dustReady = false;
        yield return new WaitForSeconds(0.1f);
        dustReady = true;
    }
}
