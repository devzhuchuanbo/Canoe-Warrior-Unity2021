using System;
using UnityEngine;

[RequireComponent(typeof(UbhSpaceship))]
public class UbhEnemy : UbhMonoBehaviour
{
	private void Start()
	{
		this._Spaceship = base.GetComponent<UbhSpaceship>();
		this.Move(base.transform.up.normalized * -1f);
	}

	private void FixedUpdate()
	{
		if (this._UseStop && base.transform.position.y < this._StopPoint)
		{
			base.rigidbody2D.velocity = Vector2.zero;
			this._UseStop = false;
		}
	}

	public void Move(Vector2 direction)
	{
		base.rigidbody2D.velocity = direction * this._Spaceship._Speed;
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name.Contains("PlayerBullet"))
		{
			UbhSimpleBullet component = c.transform.parent.GetComponent<UbhSimpleBullet>();
			UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(c.transform.parent.gameObject, false);
			this._Hp -= component._Power;
			if (this._Hp <= 0)
			{
				UnityEngine.Object.FindObjectOfType<UbhScore>().AddPoint(this._Point);
				this._Spaceship.Explosion();
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				this._Spaceship.GetAnimator().SetTrigger("Damage");
			}
		}
	}

	public const string NAME_PLAYER = "Player";

	public const string NAME_PLAYER_BULLET = "PlayerBullet";

	private const string ANIM_DAMAGE_TRIGGER = "Damage";

	[SerializeField]
	private int _Hp = 1;

	[SerializeField]
	private int _Point = 100;

	[SerializeField]
	private bool _UseStop;

	[SerializeField]
	private float _StopPoint = 2f;

	private UbhSpaceship _Spaceship;
}
