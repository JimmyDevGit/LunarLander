using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnim : MonoBehaviour
{
    public Transform spawnLocation;
    private GameObject drip;
    public GameObject dripPrefab;
    public Transform stopPoint;
    // Update is called once per frame
    private void Start()
    {
        Instantiate(dripPrefab);
        drip = GameObject.Find("RadioActiveBarrel_4(Clone)");
        drip.transform.SetParent(gameObject.transform);
        drip.transform.position = spawnLocation.transform.position;
    }
    void Update()
    {
        if (drip != null)
        {
            if (drip.transform.position.y <= stopPoint.position.y)
            {
                DestroyAndRespawnDrip();
            }
        }
    }
    public void DestroyAndRespawnDrip()
    {   
        Destroy(drip);
        GameObject myPrefab = GameObject.Instantiate(dripPrefab);
        drip = myPrefab;
        drip.transform.SetParent(gameObject.transform);
        drip.transform.position = spawnLocation.transform.position;
    }
}
