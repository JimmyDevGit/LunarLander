using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomScript : MonoBehaviour
{
    public float lifetime = 10;
    public GameObject spore;
    public GameObject sporeDud;
    public Vector3 sporeSpawnLocation;
    public GameObject sporeSpawnLocationGO;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sporeSpawnLocation = sporeSpawnLocationGO.transform.position;
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        anim.SetTrigger("dead");
        Debug.Log("Animator called");
        Instantiate(spore, sporeSpawnLocation, transform.rotation * Quaternion.Euler(0f, 0, 180f));
        Instantiate(spore, sporeSpawnLocation, transform.rotation * Quaternion.Euler(0f, 0, 180f));
        Instantiate(sporeDud, sporeSpawnLocation, transform.rotation * Quaternion.Euler(0f, 0, 180f));
        Instantiate(sporeDud, sporeSpawnLocation, transform.rotation * Quaternion.Euler(0f, 0, 180f));
        Instantiate(sporeDud, sporeSpawnLocation, transform.rotation * Quaternion.Euler(0f, 0, 180f));
    }
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
