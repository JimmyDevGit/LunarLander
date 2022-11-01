using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    public CapsuleCollider2D LegLeft;
    public CapsuleCollider2D LegRight;
    private bool landedLeft;
    private bool landedRight;
    public GameMan gameMan;

    private void Update()
    {
        if(landedRight == true && landedLeft == true)
        {
            GameObject.Find("GameManager").GetComponentInChildren<GameMan>().LevelComplete();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == LegLeft)
        {
            landedLeft = true;
        }
        if (collision.collider == LegRight)
        {
            landedRight = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == LegLeft)
        {
            landedLeft = false;
        }
        if (collision.collider == LegRight)
        {
            landedRight = false;
        }
    }
}
