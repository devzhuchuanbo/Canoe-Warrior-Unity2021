using System;
using UnityEngine;

public class ThungGo : MonoBehaviour
{
	private void Hit(Vector2 p)
	{
		if (this.loai == ThungGo.LoaiChum.Tien)
		{
			if (!this.tienTo)
			{
				if (this.tuiTien)
				{
					Vector2 vector = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
					Vector2 normalized = vector.normalized;
					GameObject gameObject = UnityEngine.Object.Instantiate(this.tuiTien, base.transform.position, Quaternion.identity) as GameObject;
					gameObject.GetComponent<Rigidbody2D>().AddForce(normalized * this.force);
				}
			}
			else if (this.tuiTienTo)
			{
				Vector2 vector2 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized2 = vector2.normalized;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(this.tuiTienTo, base.transform.position, Quaternion.identity) as GameObject;
				gameObject2.GetComponent<Rigidbody2D>().AddForce(normalized2 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Enemy)
		{
			if (this.enemy.Length > 0)
			{
				Vector2 vector3 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized3 = vector3.normalized;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this.enemy[UnityEngine.Random.Range(0, this.enemy.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject3.transform.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				gameObject3.GetComponent<Rigidbody2D>().AddForce(normalized3 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Bonus)
		{
			if (this.bonus.Length > 0)
			{
				Vector2 vector4 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized4 = vector4.normalized;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(this.bonus[UnityEngine.Random.Range(0, this.bonus.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject4.GetComponent<Rigidbody2D>().AddForce(normalized4 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.PhiTieu)
		{
			if (this.phiTieu)
			{
				Vector2 vector5 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized5 = vector5.normalized;
				GameObject gameObject5 = UnityEngine.Object.Instantiate(this.phiTieu, base.transform.position, Quaternion.identity) as GameObject;
				gameObject5.GetComponent<Rigidbody2D>().AddForce(normalized5 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Special && this.spec)
		{
			UnityEngine.Object.Instantiate(this.spec, base.transform.position, Quaternion.identity);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Hit2(Vector2 p)
	{
		if (this.loai == ThungGo.LoaiChum.Tien)
		{
			if (!this.tienTo)
			{
				if (this.tuiTien)
				{
					Vector2 vector = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
					Vector2 normalized = vector.normalized;
					GameObject gameObject = UnityEngine.Object.Instantiate(this.tuiTien, base.transform.position, Quaternion.identity) as GameObject;
					gameObject.GetComponent<Rigidbody2D>().AddForce(normalized * this.force);
				}
			}
			else if (this.tuiTienTo)
			{
				Vector2 vector2 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized2 = vector2.normalized;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(this.tuiTienTo, base.transform.position, Quaternion.identity) as GameObject;
				gameObject2.GetComponent<Rigidbody2D>().AddForce(normalized2 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Enemy)
		{
			if (this.enemy.Length > 0)
			{
				Vector2 vector3 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized3 = vector3.normalized;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this.enemy[UnityEngine.Random.Range(0, this.enemy.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject3.transform.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				gameObject3.GetComponent<Rigidbody2D>().AddForce(normalized3 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Bonus)
		{
			if (this.bonus.Length > 0)
			{
				Vector2 vector4 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized4 = vector4.normalized;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(this.bonus[UnityEngine.Random.Range(0, this.bonus.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject4.GetComponent<Rigidbody2D>().AddForce(normalized4 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.PhiTieu)
		{
			if (this.phiTieu)
			{
				Vector2 vector5 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized5 = vector5.normalized;
				GameObject gameObject5 = UnityEngine.Object.Instantiate(this.phiTieu, base.transform.position, Quaternion.identity) as GameObject;
				gameObject5.GetComponent<Rigidbody2D>().AddForce(normalized5 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Special && this.spec)
		{
			UnityEngine.Object.Instantiate(this.spec, base.transform.position, Quaternion.identity);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void DashOver()
	{
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeThungGo(base.gameObject.transform.position);
		if (this.loai == ThungGo.LoaiChum.Tien)
		{
			if (!this.tienTo)
			{
				if (this.tuiTien)
				{
					Vector2 vector = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
					Vector2 normalized = vector.normalized;
					GameObject gameObject = UnityEngine.Object.Instantiate(this.tuiTien, base.transform.position, Quaternion.identity) as GameObject;
					gameObject.GetComponent<Rigidbody2D>().AddForce(normalized * this.force);
				}
			}
			else if (this.tuiTienTo)
			{
				Vector2 vector2 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized2 = vector2.normalized;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(this.tuiTienTo, base.transform.position, Quaternion.identity) as GameObject;
				gameObject2.GetComponent<Rigidbody2D>().AddForce(normalized2 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Enemy)
		{
			if (this.enemy.Length > 0)
			{
				Vector2 vector3 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized3 = vector3.normalized;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this.enemy[UnityEngine.Random.Range(0, this.enemy.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject3.transform.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				gameObject3.GetComponent<Rigidbody2D>().AddForce(normalized3 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Bonus)
		{
			if (this.bonus.Length > 0)
			{
				Vector2 vector4 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized4 = vector4.normalized;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(this.bonus[UnityEngine.Random.Range(0, this.bonus.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject4.GetComponent<Rigidbody2D>().AddForce(normalized4 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.PhiTieu)
		{
			if (this.phiTieu)
			{
				Vector2 vector5 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized5 = vector5.normalized;
				GameObject gameObject5 = UnityEngine.Object.Instantiate(this.phiTieu, base.transform.position, Quaternion.identity) as GameObject;
				gameObject5.GetComponent<Rigidbody2D>().AddForce(normalized5 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Special && this.spec)
		{
			UnityEngine.Object.Instantiate(this.spec, base.transform.position, Quaternion.identity);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Exp()
	{
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeThungGo(base.gameObject.transform.position);
		if (this.loai == ThungGo.LoaiChum.Tien)
		{
			if (!this.tienTo)
			{
				if (this.tuiTien)
				{
					Vector2 vector = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
					Vector2 normalized = vector.normalized;
					GameObject gameObject = UnityEngine.Object.Instantiate(this.tuiTien, base.transform.position, Quaternion.identity) as GameObject;
					gameObject.GetComponent<Rigidbody2D>().AddForce(normalized * this.force);
				}
			}
			else if (this.tuiTienTo)
			{
				Vector2 vector2 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized2 = vector2.normalized;
				GameObject gameObject2 = UnityEngine.Object.Instantiate(this.tuiTienTo, base.transform.position, Quaternion.identity) as GameObject;
				gameObject2.GetComponent<Rigidbody2D>().AddForce(normalized2 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Enemy)
		{
			if (this.enemy.Length > 0)
			{
				Vector2 vector3 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized3 = vector3.normalized;
				GameObject gameObject3 = UnityEngine.Object.Instantiate(this.enemy[UnityEngine.Random.Range(0, this.enemy.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject3.transform.rotation = Quaternion.Euler(0f, 0f, (float)UnityEngine.Random.Range(0, 360));
				gameObject3.GetComponent<Rigidbody2D>().AddForce(normalized3 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Bonus)
		{
			if (this.bonus.Length > 0)
			{
				Vector2 vector4 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized4 = vector4.normalized;
				GameObject gameObject4 = UnityEngine.Object.Instantiate(this.bonus[UnityEngine.Random.Range(0, this.bonus.Length)], base.transform.position, Quaternion.identity) as GameObject;
				gameObject4.GetComponent<Rigidbody2D>().AddForce(normalized4 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.PhiTieu)
		{
			if (this.phiTieu)
			{
				Vector2 vector5 = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
				Vector2 normalized5 = vector5.normalized;
				GameObject gameObject5 = UnityEngine.Object.Instantiate(this.phiTieu, base.transform.position, Quaternion.identity) as GameObject;
				gameObject5.GetComponent<Rigidbody2D>().AddForce(normalized5 * this.force);
			}
		}
		else if (this.loai == ThungGo.LoaiChum.Special && this.spec)
		{
			UnityEngine.Object.Instantiate(this.spec, base.transform.position, Quaternion.identity);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public ThungGo.LoaiChum loai;

	public bool tienTo;

	public GameObject tuiTien;

	public GameObject tuiTienTo;

	public GameObject[] enemy;

	public GameObject[] bonus;

	public GameObject phiTieu;

	public GameObject spec;

	public float force;

	public enum LoaiChum
	{
		Tien,
		Enemy,
		Bonus,
		PhiTieu,
		Special
	}
}
