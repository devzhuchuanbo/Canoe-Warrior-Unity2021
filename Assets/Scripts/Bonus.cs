using System;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	private void Start()
	{
	}
  //  private void OnCollisionEnter2D(Collision2D col)
  //  {
  //      if (col.gameObject.tag == "Money")
		//{
		//	this.mainScript.AnTien(col.transform.position);
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
		//else if (col.gameObject.tag == "TienTo")
		//{
		//	this.mainScript.AnTienTo(col.transform.position);
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
		//else if (col.gameObject.tag == "BV")
		//{
		//	this.mainScript.AnBV();
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
		//else if (col.gameObject.tag == "Soul")
		//{
		//	this.mainScript.AnMau(col.transform.position);
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
		//else if (col.gameObject.tag == "DM")
		//{
		//	this.mainScript.AnDM(col.transform.position);
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
		//else if (col.gameObject.tag == "Scroll")
		//{
		//	this.mainScript.AnScroll(col.transform.position);
		//	UnityEngine.Object.Destroy(col.gameObject);
		//}
  //  }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Money")
        {
            this.mainScript.AnTien(coll.transform.position);
            UnityEngine.Object.Destroy(coll.gameObject);
        }
        else if (coll.tag == "TienTo")
        {
            this.mainScript.AnTienTo(coll.transform.position);
            UnityEngine.Object.Destroy(coll.gameObject);
        }
        else if (coll.tag == "BV")
        {
            this.mainScript.AnBV();
            UnityEngine.Object.Destroy(coll.gameObject);
        }
        else if (coll.tag == "Soul")
        {
            this.mainScript.AnMau(coll.transform.position);
            UnityEngine.Object.Destroy(coll.gameObject);
        }
        else if (coll.tag == "DM")
        {
            this.mainScript.AnDM(coll.transform.position);
            UnityEngine.Object.Destroy(coll.gameObject);
        }
        else if (coll.tag == "Scroll")
        {
            this.mainScript.AnScroll(coll.transform.position);
            UnityEngine.Object.Destroy(coll.gameObject);
        }
    }

    public NinjaMovementScript mainScript;
}
