using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
