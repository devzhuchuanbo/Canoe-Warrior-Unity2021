using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public CircleCollider2D NinjaColider;

    private int Number = 0;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Number < 2)
        {
            Number++;
            NinjaColider.enabled = false;
            NinjaColider.enabled = true;
        }
    }
}
