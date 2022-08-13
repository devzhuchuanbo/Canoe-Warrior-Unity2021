using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

   public CircleCollider2D mycol;

   void OnCollisionEnter2D(Collision2D col)
   {
      mycol.radius = 0f;
      mycol.radius = 0.15f;


   }


}
