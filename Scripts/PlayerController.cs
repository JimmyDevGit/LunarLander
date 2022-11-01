using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float mainThrust;
    public float mainThrustOriginal;
    public float sideThrust;
    public Rigidbody2D rb;
    public SpriteRenderer thruster;
    public SpriteRenderer airThrusterTL;
    public SpriteRenderer airThrusterTR;
    public SpriteRenderer airThrusterBL;
    public SpriteRenderer airThrusterBR;
    public Light2D thrusterLight;
    public GameObject explosion;
    public int maxFuel;
    public int currentFuel;
    public HUD hud;
    public float maxHealth;
    public float currentHealth;
    public bool paused = false;
    private Vector2 velocity;
    private bool airThrustSoundPlaying = false;
    private bool radioActiveBoostRunning;
    private float thrusterLightColorR;
    private float thrusterLightColorG;
    private float thrusterLightColorB;
    private float newThrusterLightColorR;
    private float newThrusterLightColorG;
    private float newThrusterLightColorB;
    public Light2D cabinLight;
    private float cabinLightColorR;
    private float cabinLightColorG;
    private float cabinLightColorB;
    private float newCabinLightColorR;
    private float newCabinLightColorG;
    private float newCabinLightColorB;
    private float cabinLightIntensity;
    private float newCabinLightIntensity;
    private bool ChangeSpeedSoundPlayed = false;
    private float currentDrag;
    private bool touchingVine = false;
    private bool vineHit = true;
    public bool thrustSoundPlaying = false;
    public CameraFollow cameraFollow;
    private bool lowFuelSoundPlayed = false;
    private bool lowFuelAlarmPlayed = false;
    /*private bool warpBrake = false;
    private bool warpBrakeBoostAvailable = false;*/


    // Start is called before the first frame update
    void Start()
    {
        cabinLightIntensity = cabinLight.intensity;
        thrusterLightColorR = thrusterLight.color.r;
        thrusterLightColorG = thrusterLight.color.g;
        thrusterLightColorB = thrusterLight.color.b;
        mainThrustOriginal = 7;
        currentFuel = maxFuel;
        currentHealth = maxHealth;
        mainThrust = mainThrustOriginal;
        currentDrag = rb.drag;
    }
    private void Update()
    {

        if (paused == false)
        {
            if (Input.GetKeyDown(KeyCode.W) && currentFuel > 0)
            {
                ThrustSound();
            }
            if (Input.GetKey(KeyCode.W))
            {
                ThrustDown();
            }
            if (Input.GetKeyUp(KeyCode.W) || currentFuel <= 0)
            {
                ThrustUp();
            }
        }
        else
        {
            thruster.enabled = false;
            thrusterLight.enabled = false;
            AudioManager.instance.Stop("Thruster");
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeftDown();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            RotateLeftUp();
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotateRightDown();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RotateRightUp();
        }
        //WARP BRAKE + BOOST
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.isKinematic = false;
            warpBrake = true;
            warpBrakeBoostAvailable = true;
            StartCoroutine(WarpBrakeTimer());
        }
        if(Input.GetKey(KeyCode.LeftShift) && warpBrake == true)
        {
            if(warpBrakeBoostAvailable == true)
            {
                WarpBrakeBoost();
            }
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentFuel < 200 && lowFuelSoundPlayed == false)
        {
            lowFuelSoundPlayed = true;
            AudioManager.instance.Play("LowFuel");
        }
        if(currentFuel > 200)
        {
            lowFuelSoundPlayed = false;
        }
        if (currentFuel < 100 && lowFuelAlarmPlayed == false)
        {
            lowFuelAlarmPlayed = true;
            AudioManager.instance.Play("LowFuelAlarm");
        }
        if (currentFuel > 100)
        {
            lowFuelAlarmPlayed = false;
            AudioManager.instance.Stop("LowFuelAlarm");
        }
        if(currentFuel <= 0)
        {
            AudioManager.instance.Stop("LowFuelAlarm");
        }

        if (paused == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Thrust();
            }
            if (Input.GetKey(KeyCode.A))
            {
                RotateLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                RotateRight();
            }
        }
    }
    public void ThrustSound()
    {
        if(currentFuel > 0 && thrustSoundPlaying == false)
        {
            AudioManager.instance.Play("Thruster");
            thrustSoundPlaying = true;
        }
    }
    public void ThrustDown()
    {
        if (currentFuel > 0)
        {
            thruster.enabled = true;
            thrusterLight.enabled = true;
        }
    }
    public void ThrustUp()
    {
        thruster.enabled = false;
        thrusterLight.enabled = false;
        AudioManager.instance.Stop("Thruster");
        thrustSoundPlaying = false;
    }
    public void RotateLeftDown()
    {
        airThrusterTR.enabled = true;
        airThrusterBL.enabled = true;
        airThrusterTL.enabled = false;
        airThrusterBR.enabled = false;
        AirThrustSound();
    }
    public void RotateLeftUp()
    {
        airThrusterTR.enabled = false;
        airThrusterBL.enabled = false;
        AirThrustSoundStop();
        StartCoroutine(ThrustLeftStopRotation());
    }
    public void RotateRightDown()
    {
        airThrusterTL.enabled = true;
        airThrusterBR.enabled = true;
        airThrusterTR.enabled = false;
        airThrusterBL.enabled = false;
        AirThrustSound();
    }
    public void RotateRightUp()
    {
        airThrusterTL.enabled = false;
        airThrusterBR.enabled = false;
        AirThrustSoundStop();
        StartCoroutine(ThrustRightStopRotation());
    }




    public void Thrust()
    {
        if(currentFuel > 0)
        {
            rb.AddRelativeForce(new Vector2(1, 0) * mainThrust);
            currentFuel = currentFuel - 1;
            hud.SetFuel(currentFuel);
        }
    }
    public void RotateLeft()
    {
        rb.transform.Rotate(new Vector3(0,0,1), sideThrust * Time.deltaTime);
    }
    public void RotateRight()
    {
        rb.transform.Rotate(new Vector3(0, 0, 1), -sideThrust * Time.deltaTime);
    }
    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            if(radioActiveBoostRunning == true)
            {
                mainThrust = Mathf.Lerp(v_start, v_end, elapsed / duration);
                newThrusterLightColorR = Mathf.Lerp(0, thrusterLightColorR, elapsed / duration);
                newThrusterLightColorG = Mathf.Lerp(1, thrusterLightColorG, elapsed / duration);
                newThrusterLightColorB = Mathf.Lerp(0, thrusterLightColorB, elapsed / duration);
                thrusterLight.color = new Color(newThrusterLightColorR, newThrusterLightColorG, newThrusterLightColorB, 1);

                newCabinLightColorR = Mathf.Lerp(0, cabinLightColorR, elapsed / duration);
                newCabinLightColorG = Mathf.Lerp(1, cabinLightColorG, elapsed / duration);
                newCabinLightColorB = Mathf.Lerp(0, cabinLightColorB, elapsed / duration);
                cabinLight.intensity = Mathf.Lerp(2, cabinLightIntensity, elapsed / duration);
                cabinLight.color = new Color(newCabinLightColorR, newCabinLightColorG, newCabinLightColorB, 1);
                elapsed += Time.deltaTime;
                yield return null;
            }
            else
            {
                yield break;
            }
        }
        mainThrust = v_end;
    }
    IEnumerator ChangeSpeedTimer()
    {
        thrusterLight.color = new Color(0, 1, 0, 1);
        cabinLight.color = new Color(0, 1, 0, 1);
        cabinLight.intensity = 2;
        mainThrust = mainThrustOriginal * 2;
        yield return new WaitForSeconds(3);
        radioActiveBoostRunning = true;
        StartCoroutine(ChangeSpeed(14f, 7f, 5f));
    }
    IEnumerator ChangeSpeedSoundTimer()
    {
        yield return new WaitForSeconds(3);
        ChangeSpeedSoundPlayed = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Landed on legs
        if(collision.otherCollider is CapsuleCollider2D)
        {
            if(collision.gameObject.layer == 10)
            {
                AudioManager.instance.Play("BaseLand");
            }
            if (collision.gameObject.layer == 11)
            {
                AudioManager.instance.Play("GroundHit");
            }
            if (collision.relativeVelocity.y > 6)
            {
                BlowUp();
            }
                StartCoroutine(cameraFollow.CameraShake());
        }
        if (collision.otherCollider is PolygonCollider2D)
        {
            AudioManager.instance.Play("GroundHit");
            currentHealth -= collision.relativeVelocity.magnitude;
            hud.SetHealth(currentHealth);
            StartCoroutine(cameraFollow.CameraShake());
            if (currentHealth <= 0)
            {
                BlowUp();
            }
        }
    }

    IEnumerator ThrustLeftStopRotation()
    {
        airThrusterTL.enabled = true;
        airThrusterBR.enabled = true;
        airThrusterTR.enabled = false;
        airThrusterBL.enabled = false;
        AirThrustSound();
        yield return new WaitForSeconds(.3f);
        airThrusterTL.enabled = false;
        airThrusterBR.enabled = false;
        AirThrustSoundStop();
    }
    IEnumerator ThrustRightStopRotation()
    {
        airThrusterTR.enabled = true;
        airThrusterBL.enabled = true;
        airThrusterTL.enabled = false;
        airThrusterBR.enabled = false;
        AirThrustSound();
        yield return new WaitForSeconds(.3f);
        airThrusterTR.enabled = false;
        airThrusterBL.enabled = false;
        AirThrustSoundStop();
    }
    IEnumerator VineHitTimer()
    {
        vineHit = false;
        yield return new WaitForSeconds(5);
        vineHit = true;
    }
    private void AirThrustSound()
    {
        if(airThrustSoundPlaying == false)
        {
            AudioManager.instance.Play("AirThruster");
            airThrustSoundPlaying = true;
        }
    }
    private void AirThrustSoundStop()
    {
        if (airThrustSoundPlaying == true)
        {
            AudioManager.instance.Stop("AirThruster");
            airThrustSoundPlaying = false;
        }
    }

    /*IEnumerator WarpBrakeTimer()
    {
        yield return new WaitForSeconds(.5f);
        warpBrake = false;
    }
    private void WarpBrakeBoost()
    {
        warpBrakeBoostAvailable = false;
        rb.AddRelativeForce(new Vector2(1, 0) * mainThrust * 50);
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("RadioActiveDrip"))
        {

            if(ChangeSpeedSoundPlayed == false)
            {
                AudioManager.instance.Play("RadioActiveBoostAlert");
                ChangeSpeedSoundPlayed = true;
                StartCoroutine(ChangeSpeedSoundTimer());
            }
            radioActiveBoostRunning = false;
            StartCoroutine(ChangeSpeedTimer());

            collision.gameObject.transform.parent.gameObject.GetComponent<DestroyAfterAnim>().DestroyAndRespawnDrip();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "VineSegment")
        {
            touchingVine = true;
            if (rb.velocity.magnitude > 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                rb.drag = 5;
                AudioManager.instance.Play("VineRustle");
                if(vineHit == true)
                {
                    StartCoroutine(VineHitTimer());
                    AudioManager.instance.Play("VineRustle2");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingVine = false;
        if (touchingVine == false)
        {
            rb.drag = 0;
        }
    }
    private void BlowUp()
    {
        AudioManager.instance.Stop("Thruster");
        AudioManager.instance.Play("Explosion");
        AirThrustSoundStop();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameMan>().StartCoroutine("BlowUp");
    }
    public void GetFuel()
    {
        currentFuel++;
        hud.SetFuel(currentFuel);
    }
    public void PausePlayer()
    {
        rb.isKinematic = true;
        velocity = rb.velocity;
        rb.velocity = new Vector2(0, 0);
        paused = true;
        cameraFollow.paused = true;
    }
    public void UnPausePlayer()
    {
        rb.isKinematic = false;
        rb.velocity = velocity;
        paused = false;
        cameraFollow.paused = false;
    }
}
