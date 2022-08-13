using System;
using UnityEngine;

public class FiTieuGolden : MonoBehaviour
{
	private void OnEnable()
	{
		this.timeToLife = Time.time + this.maxLifeTime;
	}

	private void Start()
	{
		this.mainCam = GameObject.FindGameObjectWithTag("Cam").transform;
		this.effectScript = GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>();
		if (this.dirF == FiTieuGolden.DirFly.Right)
		{
			this.dir = Vector2.right;
		}
		else if (this.dirF == FiTieuGolden.DirFly.Left)
		{
			this.dir = Vector2.left;
		}
	}

	private void FixedUpdate()
	{
		RaycastHit2D hit = Physics2D.Raycast(base.transform.position, this.dir, this.maxSpeed * Time.deltaTime, this.layer);
		if (hit)
		{
			if (hit.collider.tag == "Wall")
			{
				this.effectScript.ToeDat(hit.point);
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				base.gameObject.SetActive(false);
			}
			else if (hit.collider.tag == "Enemy")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeMau(hit.point);
			}
			else if (hit.collider.tag == "Metal")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeLua(hit.point);
			}
			else if (hit.collider.tag == "Wood")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeGo(hit.point);
			}
			else if (hit.collider.tag == "Su")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeSu(hit.point);
			}
			else if (hit.collider.tag == "Trap")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeLua(hit.point);
			}
			else if (hit.collider.tag == "Ong")
			{
				hit.collider.gameObject.SendMessage("Hit2", hit.point, SendMessageOptions.DontRequireReceiver);
				this.effectScript.ToeMauXanh(hit.point);
			}
			else if (hit.collider.tag == "PL")
			{
				this.effectScript.ToeDat(hit.point);
				Rigidbody2D component = hit.collider.gameObject.GetComponent<Rigidbody2D>();
				if (component)
				{
					if (this.dirF == FiTieuGolden.DirFly.Left)
					{
						component.AddForceAtPosition(Vector2.left * 200000f, hit.point);
					}
					else
					{
						component.AddForceAtPosition(Vector2.right * 200000f, hit.point);
					}
				}
				base.gameObject.SetActive(false);
			}
			else
			{
				this.effectScript.ToeDat(hit.point);
				base.gameObject.SetActive(false);
			}
		}
		if (this.dirF == FiTieuGolden.DirFly.Right)
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * this.maxSpeed);
		}
		else if (this.dirF == FiTieuGolden.DirFly.Left)
		{
			base.transform.Translate(Vector3.left * Time.deltaTime * this.maxSpeed);
		}
		if (base.transform.position.x > this.mainCam.position.x + 15f || base.transform.position.x < this.mainCam.position.x - 15f || Time.time > this.timeToLife)
		{
			base.gameObject.SetActive(false);
		}
	}

	public FiTieuGolden.DirFly dirF;

	public float maxSpeed = 10f;

	private Transform mainCam;

	private Vector2 dir;

	public LayerMask layer;

	private float timeToLife;

	public float maxLifeTime = 3f;

	public EffectController effectScript;

	public enum DirFly
	{
		Left,
		Right
	}
}
