// dnSpy decompiler from Assembly-CSharp.dll class: NinjaMovementScript
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaMovementScript : MonoBehaviour
{
	private void Start()
	{
		// PlayerPrefs.DeleteAll();ee
		this.MainEventsLog_script.FadeIn();
		this.Btn_Left_bool = false;
		this.Btn_Right_bool = true;
		this.PlayerSpeedtg = this.PlayerSpeed;
		this.canFi = true;
		this.dashing = false;
		this.isDieing = false;
		this.stoping = true;
		this.dangphattiengtreo = false;
		this.isTransing = false;
		this.tgbv = Time.time - 1f;
		this.tgtrans = Time.time - 1f;
		this.timeToDash = Time.time;
		this.timeToTrans = Time.time;
		this.timeCheckingAdd = 0f;
		this.DashCoolDown = this.MainEventsLog_script.dashCoolDown;
		this.TransCoolDown = this.MainEventsLog_script.transCoolDown;
		this.timeTrans = this.MainEventsLog_script.transTime;
		this.timeBV = this.MainEventsLog_script.timeBV;
		this.GroundedToObjectsList = new List<GameObject>();
		this.WalledToObjectsList = new List<GameObject>();
		this.camTargetStartPos = this.camTarget.transform.localPosition;
		this.camTargetStartIventPos = new Vector3(-this.camTargetStartPos.x, this.camTargetStartPos.y, 0f);
		this.DoubleJump = true;
		this.Ground_X_MIN = -0.75f;
		this.Ground_X_MAX = 0.75f;
		this.Ground_Y_MIN = 0.5f;
		this.Ground_Y_MAX = 1f;
		this.ActiveCheckpoint = base.gameObject.transform.position;
		GameObject gameObject = UnityEngine.Object.Instantiate(this.NinjaPlatformRoot_PREFAB, Vector3.zero, Quaternion.identity) as GameObject;
		this.NinjaPlatformRoot = gameObject.GetComponent<NinjaPlatformRoot>();
		gameObject.name = "NINJA_PlatformRoot";
		this.WallGripEmissionRate = 10;
		this.WallGripParticles.emissionRate = 0f;
		this.PlayerLooksRight = true;
		this.MySpriteOriginalScale = this.MySpriteOBJ.transform.localScale;
		this.caiXac = this.ragdollTotal.GetComponent<XacChetChay>();
		this.fiPool = GameObject.FindGameObjectWithTag("Pool").GetComponent<FiTieuPool>();
		this.Freezz = false;
		this.Checking = false;
		this.cameraScript = GameObject.FindGameObjectWithTag("Cam").GetComponent<CameraFollowTarget>();
		if (!PlayerPrefs.HasKey("TransInUse"))
		{
			this.Transformer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("bienhinh_goccay");

		}
		else
		{
			string @string = PlayerPrefs.GetString("TransInUse");
			this.Transformer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@string);
		}


	}

	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			Button_Jump_press();
			//print("space key was pressed");
		}
#if UNITY_EDITOR
		if (Input.GetKeyDown("d"))
        {
			Button_Right_press();
        }
        if (Input.GetKeyUp("d"))
        {
            Button_Right_release();
        }
        if (Input.GetKeyDown("a"))
        {
            Button_Left_press();
        }
        if (Input.GetKeyUp("a"))
        {
            Button_Left_release();
        }
#endif
		if (this.walljump_count >= 0f)
		{
			this.walljump_count -= Time.deltaTime;
		}
		if (!this.isDieing)
		{
			if (this.PlayerLooksRight)
			{
				this.camTarget.transform.localPosition = Vector3.Lerp(this.camTarget.transform.localPosition, this.camTargetStartPos, this.timeToMoveTarget * Time.deltaTime);
			}
			else
			{
				this.camTarget.transform.localPosition = Vector3.Lerp(this.camTarget.transform.localPosition, this.camTargetStartIventPos, this.timeToMoveTarget * Time.deltaTime);
			}
		}
		else
		{
			this.camTarget.transform.localPosition = Vector3.Lerp(this.camTarget.transform.localPosition, Vector3.zero, this.timeToMoveTarget * Time.deltaTime);
		}
		if (this.WallTouch && !this.IsGrounded)
		{

			if (!this.dangphattiengtreo)
			{
				this.dangphattiengtreo = true;
				this.AudioSource_Wall.Play();
			}
		}
		else if (this.dangphattiengtreo)
		{
			this.dangphattiengtreo = false;
			this.AudioSource_Wall.Stop();
		}
		if (this.Checking)
		{
			this.timeCheckingAdd += Time.deltaTime;
			if (this.timeCheckingAdd >= this.timeForChecking)
			{
				this.timeCheckingAdd = 0f;
				this.Checking = false;
				this.AnimatorController.SetBool("Checking", false);
				this.EnterCheckPointCash(base.gameObject.transform.position);
				this.MainEventsLog_script.FiGRelease();
			}
		}
	}

	private void FixedUpdate()
	{

		if (!this.Freezz && !this.Checking)
		{
			if (!this.dashing)
			{
				if (this.Btn_Left_bool && !this.Btn_Right_bool)
				{
					if (this.PlayerLooksRight && !this.WallTouch)
					{
						this.PlayerLooksRight = false;

						this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOriginalScale.y, this.MySpriteOriginalScale.z);
						if (!this.isTransing)
						{
							base.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.maxVelocity, base.GetComponent<Rigidbody2D>().velocity.y);
							this.trainLine.gameObject.SetActive(true);

						}
					}
					if (this.stoping && !this.WallTouch)
					{


						this.stoping = false;
						if (!this.isTransing)
						{
							base.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.maxVelocity, base.GetComponent<Rigidbody2D>().velocity.y);
						}
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.NinjaVisualRoot.transform.right.x, this.NinjaVisualRoot.transform.right.y) * -this.PlayerSpeed * Time.deltaTime);
					}
					else
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.NinjaVisualRoot.transform.right.x, this.NinjaVisualRoot.transform.right.y) * -this.PlayerSpeed * Time.deltaTime);
					}
				}
				else if (!this.Btn_Left_bool && this.Btn_Right_bool)
				{
					if (!this.PlayerLooksRight && !this.WallTouch)
					{
						this.PlayerLooksRight = true;
						this.MySpriteOBJ.transform.localScale = this.MySpriteOriginalScale;
						if (!this.isTransing)
						{
							this.trainLine.gameObject.SetActive(true);

							base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxVelocity, base.GetComponent<Rigidbody2D>().velocity.y);

						}
					}
					if (this.stoping && !this.WallTouch)
					{
						this.stoping = false;
						if (!this.isTransing)
						{
							base.GetComponent<Rigidbody2D>().velocity = new Vector2(this.maxVelocity, base.GetComponent<Rigidbody2D>().velocity.y);
						}
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.NinjaVisualRoot.transform.right.x, this.NinjaVisualRoot.transform.right.y) * this.PlayerSpeed * Time.deltaTime);
					}
					else
					{
						base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.NinjaVisualRoot.transform.right.x, this.NinjaVisualRoot.transform.right.y) * this.PlayerSpeed * Time.deltaTime);
					}
				}
				else if (!this.Btn_Left_bool && !this.Btn_Right_bool)
				{
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, base.GetComponent<Rigidbody2D>().velocity.y);
					this.stoping = true;
					if (this.AnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Start"))
					{
						this.trainLine.gameObject.SetActive(false);

					}
				}
			}
			else
			{
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				Vector2 direction = this.dashPos.position - base.transform.position;
				RaycastHit2D hit = Physics2D.Raycast(base.transform.position, direction, 1f, this.dashLayer);
				if (!hit)
				{
					base.transform.position = Vector3.MoveTowards(base.transform.position, this.dashPos.position, this.dashSpeed * Time.deltaTime);
				}
				Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1.1f);
				foreach (Collider2D collider2D in array)
				{
					collider2D.gameObject.SendMessage("DashOver", SendMessageOptions.DontRequireReceiver);
				}

			}
			if (!this.dashing)
			{
				if (this.IsGrounded && !this.WallTouch)
				{
					base.GetComponent<Rigidbody2D>().gravityScale = 0f;
				}
				else if (base.GetComponent<Rigidbody2D>().gravityScale != this.gravityScalef)
				{
					base.GetComponent<Rigidbody2D>().gravityScale = this.gravityScalef;
				}
			}
			else
			{
				base.GetComponent<Rigidbody2D>().gravityScale = 0f;
			}
			if (!this.IsGrounded && this.WallTouch)
			{


				base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, Physics2D.gravity.y * 0.01f);
			}
			this.UpInTheAir_Counter++;
			if (this.UpInTheAir_Counter > 5 && !this.IsGrounded && !this.WallTouch)
			{
				Vector2 to = new Vector2(base.transform.up.x, base.transform.up.y);
				Vector2 from = new Vector2(0f, 1f);
				float num = Vector2.Angle(from, to);
				if (from.normalized.x > to.normalized.x)
				{
					num *= -1f;
				}
				if (-from.normalized.y > to.normalized.y)
				{
					num *= -1f;
				}
				base.GetComponent<Rigidbody2D>().AddTorque(num * 400f * Time.deltaTime);
			}
			if (this.Btn_Jump_bool && this.JumpForceCount > 0f)
			{



				base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.JumpForce);
				this.JumpForceCount -= 0.1f * Time.deltaTime;
			}
			if (this.OnCollisionStayCounter == 0)
			{
				this.OnCollisionBugThreshold++;
			}
			else
			{
				this.OnCollisionStayCounter = 0;
			}
			if (this.OnCollisionBugThreshold > 0 && (this.IsGrounded || this.WallTouch))
			{

				this.DJ_available = true;
				this.IsGrounded = false;

				this.WallTouch = false;
				print("HErrrrrrre");
				base.transform.parent = null;
				this.GroundedToObjectsList.Clear();
				this.WalledToObjectsList.Clear();
				this.WallGripParticles.emissionRate = 0f;
				this.FixStateTimer = 0;
				this.OnCollisionBugThreshold = 0;
				this.OnCollisionStayCounter = 1;




			}
		}
		float value = base.GetComponent<Rigidbody2D>().velocity.y;
		float value2 = base.GetComponent<Rigidbody2D>().velocity.sqrMagnitude * 4f;
		if (this.JustPressedSpace > 0)
		{
			value2 = 0f;
			this.JustPressedSpace--;


		}
		if (!this.Btn_Jump_bool && this.IsGrounded)
		{
			value = 0f;
		}
		this.AnimatorController.SetFloat("HorizontalSpeed", value2);
		this.AnimatorController.SetFloat("VerticalSpeed", value);
		this.AnimatorController.SetBool("Grounded", this.IsGrounded);
		this.AnimatorController.SetBool("Walled", this.WallTouch);
		if (this.tgbv >= Time.time)
		{
			this.BVed = true;
		}
		else
		{
			this.BVed = false;
		}
		if (this.isTransing && this.tgtrans < Time.time)
		{
			this.bienhinhnguoc();
		}


	}

	public void EnterCheckPoint(Vector3 p)
	{
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 35)
		{
			this.CheckParticle.transform.position = p;
			this.CheckParticle.Play();
			this.checkAudio.Play();
		}
		this.ActiveCheckpoint = p;
		this.effectController.EnterCP();
	}

	public void EnterCheckPointCash(Vector3 p)
	{
		this.CheckParticle.transform.position = p;
		this.CheckParticle.Play();
		this.checkAudio.Play();
		this.ActiveCheckpoint = p;
		UnityEngine.Object.Instantiate(this.checkParticleInstance, base.transform.position, Quaternion.identity);
		this.effectController.EnterCP();
		this.effectController.SumonBat(base.transform.position);
		this.MainEventsLog_script.TruPhiCheck();
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "TrapDoc" || coll.tag == "Ong")
		{
			this.NinjaBiDinhDoc();
		}
		else if (coll.tag == "TrapLua")
		{
			this.NinjaBiChay();
		}
		else if (coll.gameObject.CompareTag("Trap2"))
		{
			this.NinjaBiTrapDam();
			coll.SendMessage("KilledPlayer", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void AnTien(Vector3 p)
	{
		this.effectController.AnTien(p);
		this.MainEventsLog_script.AnTien(0, base.transform);
	}

	public void AnTienTo(Vector3 p)
	{
		this.effectController.AnTien(p);
		this.MainEventsLog_script.AnTien(1, base.transform);
	}

	public void AnBV()
	{
		this.effectController.BV(this.timeBV);
		this.tgbv = Time.time + this.timeBV;
	}

	public void AnMau(Vector3 p)
	{
		this.effectController.AnMau(base.gameObject.transform.position);
		this.MainEventsLog_script.AnMau();
	}

	public void AnDM(Vector3 p)
	{
		this.effectController.AnDM(base.gameObject.transform.position);
		this.MainEventsLog_script.AnDM(base.transform);
	}

	public void AnScroll(Vector3 p)
	{
		this.effectController.AnScroll(base.gameObject.transform.position);
		this.MainEventsLog_script.AnScroll();
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Plat")
		{
			IsHeOnPlatform = true;

		}
		else
		{
			IsHeOnPlatform = false;
		}

		if (coll.gameObject.CompareTag("Trap"))
		{
			this.NinjaBiTrapDam();
		}
		if (!this.IsGrounded && !this.Btn_Left_bool && !this.Btn_Right_bool)
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x * 0.25f, -0.01f);
		}
		else if (!this.IsGrounded)
		{
			base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, -0.01f);
		}
		this.OnCollisionStayCounter++;
		this.OnCollisionBugThreshold = 0;
		this.UpInTheAir_Counter = 0;
		foreach (ContactPoint2D contactPoint2D in coll.contacts)
		{
			if (0.1f > contactPoint2D.normal.y && contactPoint2D.normal.x * contactPoint2D.normal.x < 0.7225f)
			{
				this.JumpForceCount = 0f;
			}
			else if (contactPoint2D.normal.x >= this.Ground_X_MIN && contactPoint2D.normal.x <= this.Ground_X_MAX && contactPoint2D.normal.y >= this.Ground_Y_MIN && contactPoint2D.normal.y <= this.Ground_Y_MAX)
			{
				int num = 0;
				foreach (GameObject gameObject in this.GroundedToObjectsList)
				{
					if (contactPoint2D.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
					{
						num++;
					}
				}
				if (num == 0)
				{
					this.DJ_available = false;
					this.GroundedToObjectsList.Add(contactPoint2D.collider.gameObject);
					base.transform.parent = null;
					this.NinjaPlatformRoot.transform.position = contactPoint2D.collider.gameObject.transform.position;
					this.NinjaPlatformRoot.RootedTo = contactPoint2D.collider.gameObject;
					base.transform.parent = this.NinjaPlatformRoot.transform;
					this.IsGrounded = true;
					this.j2 = 2;
					base.GetComponent<Rigidbody2D>().AddForce(contactPoint2D.normal * -300f);
					if (this.WallTouch)
					{
						this.WallGripParticles.emissionRate = 0f;
						this.FixStateTimer = 0;
					}
				}
			}
			else if (base.GetComponent<Rigidbody2D>().velocity.y < 0f && !this.IsGrounded)
			{
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				int num2 = 0;
				foreach (GameObject gameObject2 in this.WalledToObjectsList)
				{
					if (contactPoint2D.collider.gameObject.GetInstanceID() == gameObject2.GetInstanceID())
					{
						num2++;
					}
				}
				if (num2 == 0)
				{
					this.DJ_available = false;
					this.WalledToObjectsList.Add(contactPoint2D.collider.gameObject);
					base.transform.parent = null;
					this.NinjaPlatformRoot.transform.position = contactPoint2D.collider.gameObject.transform.position;
					this.NinjaPlatformRoot.RootedTo = contactPoint2D.collider.gameObject;
					base.transform.parent = this.NinjaPlatformRoot.transform;
					this.WallTouch = true;
					if (contactPoint2D.normal.x > 0f)
					{
						if (!this.dashing)
						{
							this.PlayerLooksRight = true;
							this.MySpriteOBJ.transform.localScale = this.MySpriteOriginalScale;
						}
					}
					else if (!this.dashing)
					{
						this.PlayerLooksRight = false;
						this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOriginalScale.y, this.MySpriteOriginalScale.z);
					}
					this.WallGripParticles.emissionRate = (float)this.WallGripEmissionRate;
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D coll)
	{

		this.OnCollisionStayCounter++;
		this.UpInTheAir_Counter = 0;
		if (!this.IsGrounded && !this.WallTouch)
		{
			this.FixStateTimer++;
			if (this.FixStateTimer > 4)
			{
				foreach (ContactPoint2D contactPoint2D in coll.contacts)
				{
					if (0.1f > contactPoint2D.normal.y && contactPoint2D.normal.x * contactPoint2D.normal.x < 0.7225f)
					{
						this.JumpForceCount = 0f;
					}
					else if (contactPoint2D.normal.x >= this.Ground_X_MIN && contactPoint2D.normal.x <= this.Ground_X_MAX && contactPoint2D.normal.y >= this.Ground_Y_MIN && contactPoint2D.normal.y <= this.Ground_Y_MAX)
					{
						this.FixStateTimer = 0;
						this.DJ_available = false;
						this.GroundedToObjectsList.Add(contactPoint2D.collider.gameObject);
						this.IsGrounded = true;
					}
					else if (base.GetComponent<Rigidbody2D>().velocity.y < 0f)
					{
						this.FixStateTimer = 0;
						this.DJ_available = false;
						this.WalledToObjectsList.Add(contactPoint2D.collider.gameObject);
						this.WallTouch = true;
						base.transform.parent = null;
						this.NinjaPlatformRoot.transform.position = contactPoint2D.collider.gameObject.transform.position;
						this.NinjaPlatformRoot.RootedTo = contactPoint2D.collider.gameObject;
						base.transform.parent = this.NinjaPlatformRoot.transform;
						if (contactPoint2D.normal.x > 0f)
						{
							if (!this.dashing)
							{
								this.PlayerLooksRight = true;
								this.MySpriteOBJ.transform.localScale = this.MySpriteOriginalScale;
							}
						}
						else if (!this.dashing)
						{
							this.PlayerLooksRight = false;
							this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOriginalScale.y, this.MySpriteOriginalScale.z);
						}
						this.WallGripParticles.emissionRate = (float)this.WallGripEmissionRate;
					}
				}
			}
		}
		else if (this.IsGrounded)
		{
			Vector2 vector = Vector2.zero;
			foreach (ContactPoint2D contactPoint2D2 in coll.contacts)
			{
				int num = 0;
				foreach (GameObject gameObject in this.GroundedToObjectsList)
				{
					if (contactPoint2D2.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
					{
						vector += contactPoint2D2.normal;
						num++;
					}
				}
				if (num > 0)
				{
					vector /= (float)num;
					if ((vector.x > this.Ground_X_MAX || vector.x < this.Ground_X_MIN) && (vector.y > this.Ground_Y_MAX || vector.y < this.Ground_Y_MIN))
					{
						base.GetComponent<Rigidbody2D>().AddForce(vector * 100f);
					}
					else
					{
						Vector2 to = new Vector2(base.transform.up.x, base.transform.up.y);
						float num2 = Vector2.Angle(vector, to);
						if (vector.normalized.x > to.normalized.x)
						{
							num2 *= -1f;
						}
						if (-vector.normalized.y > to.normalized.y)
						{
							num2 *= -1f;
						}
						base.GetComponent<Rigidbody2D>().AddTorque(num2 * 1000f * Time.deltaTime);
						base.GetComponent<Rigidbody2D>().AddForce(vector * -300f);
					}
				}
			}
		}
		else if (this.WallTouch)
		{
			foreach (ContactPoint2D contactPoint2D3 in coll.contacts)
			{
				Vector2 vector2 = Vector2.zero;
				int num3 = 0;
				foreach (GameObject gameObject2 in this.WalledToObjectsList)
				{
					if (contactPoint2D3.collider.gameObject.GetInstanceID() == gameObject2.GetInstanceID())
					{
						vector2 += contactPoint2D3.normal;
						num3++;
					}
				}
				if (num3 > 0)
				{
					vector2 /= (float)num3;
					if ((vector2.x > this.Ground_X_MAX || vector2.x < this.Ground_X_MIN) && (vector2.y > this.Ground_Y_MAX || vector2.y < this.Ground_Y_MIN))
					{
						if ((!this.Btn_Left_bool && !this.PlayerLooksRight) || (!this.Btn_Right_bool && this.PlayerLooksRight))
						{
							base.GetComponent<Rigidbody2D>().AddForce(vector2 * -100f);
						}
						Vector2 vector3 = new Vector2(base.transform.up.x, base.transform.up.y);
						if (!this.PlayerLooksRight)
						{
							vector3 = this.RotateThisVector(vector3, 1.35f);
						}
						else
						{
							vector3 = this.RotateThisVector(vector3, -1.35f);
						}
						float num4 = Vector2.Angle(vector2, vector3);
						if (contactPoint2D3.normal.x > vector3.normalized.x)
						{
							num4 *= -1f;
						}
						if (-contactPoint2D3.normal.y > vector3.normalized.y)
						{
							num4 *= -1f;
						}
						base.GetComponent<Rigidbody2D>().AddTorque(num4 * 450f * Time.deltaTime);
					}
					else if ((!this.Btn_Left_bool && !this.PlayerLooksRight) || (!this.Btn_Right_bool && this.PlayerLooksRight))
					{
						base.GetComponent<Rigidbody2D>().AddForce(vector2 * 100f);
					}
				}
			}
		}
	}

	private void OnCollisionExit2D(Collision2D coll)
	{
		this.OnCollisionStayCounter = 0;
		foreach (ContactPoint2D contactPoint2D in coll.contacts)
		{
			int num = 0;
			int num2 = 0;
			foreach (GameObject gameObject in this.GroundedToObjectsList)
			{
				if (contactPoint2D.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
				{
					num++;
				}
			}
			foreach (GameObject gameObject2 in this.WalledToObjectsList)
			{
				if (contactPoint2D.collider.gameObject.GetInstanceID() == gameObject2.GetInstanceID())
				{
					num2++;
				}
			}
			if (num > 0)
			{
				this.GroundedToObjectsList.Remove(contactPoint2D.collider.gameObject);
				if (this.GroundedToObjectsList.Count == 0)
				{
					this.DJ_available = true;
					this.IsGrounded = false;
					base.transform.parent = null;
					this.FixStateTimer = 0;
				}
			}
			if (num2 > 0)
			{
				this.WalledToObjectsList.Remove(contactPoint2D.collider.gameObject);
				if (this.WalledToObjectsList.Count == 0)
				{
					if (!this.NoNeedForSafeJump_bool)
					{
						this.walljump_count = 0.16f;
					}
					this.NoNeedForSafeJump_bool = false;
					this.DJ_available = true;
					base.transform.parent = null;
					this.WallTouch = false;
					this.WallGripParticles.emissionRate = 0f;
					this.FixStateTimer = 0;
				}
			}
		}
	}

	private void TrapHold(float holdTime)
	{
		this.Freezz = true;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (holdTime <= 3f)
		{
			this.MainEventsLog_script.playerDinhBayGau();
		}
		else
		{
			this.MainEventsLog_script.playerDinhBayLong();
		}
		base.StartCoroutine(this.FreeRun(holdTime));
	}

	private IEnumerator FreeRun(float holdTime)
	{
		yield return new WaitForSeconds(holdTime);
		this.Freezz = false;
		yield break;
	}

	private void BackToCP()
	{
		this.effectController.HoiSinh(this.ActiveCheckpoint);
		this.cameraScript.FollowSpeed += 3f;
		base.gameObject.transform.position = new Vector3(this.ActiveCheckpoint.x, base.gameObject.transform.position.y, this.ActiveCheckpoint.z);
	}

	public void NinjaKilledEnemy(Vector3 p)
	{
		int num = UnityEngine.Random.Range(1, 100);
		if (num <= 33)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.dm, p, Quaternion.identity) as GameObject;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1250f));
			this.MainEventsLog_script.TingTing();
		}
		this.dashOk++;
		this.MainEventsLog_script.EnemyDeath();
		this.GroundedToObjectsList.Clear();
		this.WalledToObjectsList.Clear();
	}

	public void NinjaKilledEnemyNoRw(Vector3 p)
	{
		this.dashOk++;
		this.MainEventsLog_script.EnemyDeath();
		this.GroundedToObjectsList.Clear();
		this.WalledToObjectsList.Clear();
	}

	public void Button_Left_press()
	{
		this.Btn_Left_bool = true;
		this.Btn_Right_bool = false;
	}

	public void Button_Left_release()
	{
		this.Btn_Left_bool = false;
	}

	public void Button_Right_press()
	{
		this.Btn_Right_bool = true;
		this.Btn_Left_bool = false;
	}

	public void Button_Right_release()
	{
		this.Btn_Right_bool = false;
	}

	public void Button_Check_press()
	{
		if (this.MainEventsLog_script.enoughToCheck)
		{
			this.Checking = true;
			this.AnimatorController.SetBool("Checking", true);
			this.MainEventsLog_script.FiGPress(3f);
		}
	}

	public void Button_Check_release()
	{
		this.Checking = false;
		this.AnimatorController.SetBool("Checking", false);
		this.timeCheckingAdd = 0f;
		this.MainEventsLog_script.FiGRelease();
	}

	public void Button_Jump_press()
	{
		if (!this.dashing && !this.Freezz && !this.Checking)
		{
			this.JustPressedSpace = 2;
			this.Btn_Jump_bool = true;
			if (this.IsGrounded)
			{
				this.DJ_available = true;
				this.AudioSource_Jump.Play();
				this.MainEventsLog_script.playerJump();
				this.JumpForceCount = 0.02f;
				base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 0f) + new Vector2(this.NinjaVisualRoot.transform.up.x, this.NinjaVisualRoot.transform.up.y) * this.JumpForce;
				this.JumpParticles_floor.Emit(20);
				this.j2 = 0;
			}
			else if (this.DoubleJump && !this.WallTouch)
			{
				if (this.j2 == 2)
				{
					this.DJ_available = true;
					this.AudioSource_Jump.Play();
					this.MainEventsLog_script.playerJump();
					this.JumpForceCount = 0.02f;
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, 0f) + new Vector2(this.NinjaVisualRoot.transform.up.x, this.NinjaVisualRoot.transform.up.y) * this.JumpForce;
					this.JumpParticles_floor.Emit(20);
					this.j2 = 0;
				}
				else if (this.DJ_available)
				{
					this.DJ_available = false;
					this.AudioSource_Jump.Play();
					this.MainEventsLog_script.playerJump();
					this.JumpForceCount = 0.02f;
					base.GetComponent<Rigidbody2D>().velocity = new Vector2(base.GetComponent<Rigidbody2D>().velocity.x, this.JumpForce);
					this.JumpParticles_doublejump.Emit(10);
				}
			}
			if ((this.WallTouch || this.walljump_count > 0f) && !this.IsGrounded)
			{
				if (this.walljump_count <= 0f)
				{
					this.NoNeedForSafeJump_bool = true;
				}

				this.DJ_available = true;
				this.AudioSource_Jump.Play();
				this.MainEventsLog_script.playerJump();
				this.WallTouch = false;
				this.JumpForceCount = 0.02f;
				this.JumpParticles_wall.Emit(20);
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				if (!this.PlayerLooksRight)
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.JumpForce * 32f, 0f));
				}
				else
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.JumpForce * 32f, 0f));
				}
			}
		}
	}

	public void Button_Fi_press()
	{
		if (this.canFi && !this.dashing && !this.Checking)
		{
			this.MainEventsLog_script.FiCoolDownFill(this.FiCoolDown);
			base.StartCoroutine(this.Fi());
			base.StartCoroutine(this.FCoolDown());
		}
	}

	private IEnumerator Fi()
	{
		this.canFi = false;
		if (this.IsGrounded)
		{
			this.AnimatorController.SetBool("Fing", false);
		}
		else
		{
			this.AnimatorController.SetBool("Fing", true);
		}
		this.AnimatorController.SetTrigger("Fi");
		if (this.PlayerLooksRight)
		{
			Transform go = this.fiPool.GetFiR();
			go.position = this.FiPos.position;
			go.gameObject.SetActive(true);
			this.AudioSource_Fi.Play();
		}
		else
		{
			Transform go2 = this.fiPool.GetFiL();
			go2.position = this.FiPos.position;
			go2.gameObject.SetActive(true);
			this.AudioSource_Fi.Play();
		}
		yield return new WaitForSeconds(0.3f);
		this.AnimatorController.SetBool("Fing", false);
		yield break;
	}

	private IEnumerator FCoolDown()
	{
		yield return new WaitForSeconds(this.FiCoolDown);
		this.canFi = true;
		yield break;
	}

	public void Button_Trans_press()
	{
		if (!this.isTransing)
		{
			if (!this.Freezz && !this.Checking)
			{
				this.bienhinhxuoi();
				this.timeCheckingAdd = 0f;
			}
		}
		else
		{
			this.bienhinhnguoc();
		}
	}

	private void bienhinhxuoi()
	{
		if (this.timeToTrans <= Time.time)
		{
			this.effectController.ToeTuyet(base.gameObject.transform.position);
			this.PlayerSpeed = this.PlayerSpeedTrans;
			this.Transformer.gameObject.SetActive(true);
			this.ninjaroot.gameObject.SetActive(false);
			this.trainLine.gameObject.SetActive(false);
			if (this.IsGrounded)
			{
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, this.forceUp));
			}
			if (this.WallTouch)
			{
				if (this.PlayerLooksRight)
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000f, 0f));
				}
				else
				{
					base.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 0f));
				}
			}
			base.gameObject.layer = 17;
			this.isTransing = true;
			this.tgtrans = Time.time + this.timeTrans;
			this.MainEventsLog_script.BienHinh(true);
			this.MainEventsLog_script.Trans2CoolDownFill(this.timeTrans);
		}
	}

	private void bienhinhnguoc()
	{
		this.timeToTrans = Time.time + this.TransCoolDown;
		this.effectController.ToeTuyet(base.gameObject.transform.position);
		this.PlayerSpeed = this.PlayerSpeedtg;
		this.Transformer.gameObject.SetActive(false);
		this.ninjaroot.gameObject.SetActive(true);
		this.trainLine.gameObject.SetActive(true);
		if (!this.Freezz && !this.Checking)
		{
			base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, this.forceUp));
		}
		base.gameObject.layer = 8;
		this.isTransing = false;
		this.MainEventsLog_script.BienHinh(false);
		this.MainEventsLog_script.TransCoolDownFill(this.TransCoolDown);
	}

	public void Button_Dash_press()
	{
		if (this.timeToDash <= Time.time && !this.Checking)
		{
			this.timeToDash = Time.time + this.DashCoolDown;
			this.MainEventsLog_script.DashCoolDownFill(this.DashCoolDown);
			base.StartCoroutine(this.Dash());
		}
	}

	private IEnumerator Dash()
	{
		this.Btn_Jump_bool = false;
		this.AnimatorController.SetBool("Dashing", true);
		this.AnimatorController.SetTrigger("Dash");
		this.dashing = true;
		this.dashOk = 0;
		this.AudioSource_Dash.Play();
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.JumpParticles_floor.Emit(20);
		yield return new WaitForSeconds(0.4f);
		base.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, base.GetComponent<Rigidbody2D>().velocity.y);
		this.dashing = false;
		if (this.dashOk > 0)
		{
			this.MainEventsLog_script.playerDashOk(this.dashOk);
		}
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		this.AnimatorController.SetBool("Dashing", false);
		yield break;
	}

	public void Button_Jump_release()
	{
	}

	private Vector2 RotateThisVector(Vector2 v, float angle)
	{
		float num = Mathf.Sin(angle);
		float num2 = Mathf.Cos(angle);
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num2 * y + num * x;
		return v;
	}

	private void CongFinish(GameObject target)
	{
		this.MainEventsLog_script.RemoveControl();
		this.Btn_Left_bool = false;
		this.Btn_Right_bool = true;
		this.cameraScript.FollowTargetOBJ = target;
		this.MainEventsLog_script.Finish(0);
	}

	private void CongBai1(GameObject target)
	{
		this.MainEventsLog_script.RemoveControl();
		this.Btn_Left_bool = false;
		this.Btn_Right_bool = true;
		this.cameraScript.FollowTargetOBJ = target;
		this.MainEventsLog_script.Finish(1);
	}

	private void CongPortal(GameObject target)
	{
		this.MainEventsLog_script.RemoveControl();
		this.Btn_Left_bool = false;
		this.Btn_Right_bool = false;
		this.cameraScript.FollowTargetOBJ = target;
		this.MainEventsLog_script.Finish(2);
	}

	private void Exp()
	{
		this.NinjaBiChay();
	}

	public void NinjaBiDinhDoc()
	{
		if (!this.isDieing && !this.BVed)
		{
			this.isDieing = true;
			this.iDie("Doc");
		}
	}

	public void NinjaBiSetGiat()
	{
		if (!this.isDieing && !this.BVed)
		{
			this.isDieing = true;
			this.iDie("Giat");
		}
	}

	public void NinjaBiChay()
	{
		if (!this.isDieing && !this.BVed)
		{
			this.isDieing = true;
			this.iDie("Chay");
		}
	}

	public void NinjaBiXien()
	{
		if (!this.isDieing && !this.BVed && !this.dashing)
		{
			this.isDieing = true;
			this.iDie("Xien");
		}
	}

	public void NinjaBiBan()
	{
		if (!this.isDieing && !this.BVed && !this.dashing)
		{
			this.isDieing = true;
			this.iDie("Ban");
		}
	}

	public void NinjaBiMuoiDot()
	{
		if (!this.isDieing && !this.BVed)
		{
			this.isDieing = true;
			this.iDie("Doc");
		}
	}

	public void NinjaBiTrapDam()
	{
		if (!this.isDieing)
		{
			this.effectController.ToeMau(base.gameObject.transform.position);
			this.isDieing = true;
			this.iDie("Trap");
		}
	}

	public void NinjaDiesKillZone()
	{
		if (!this.isDieing)
		{
			this.isDieing = true;
			this.iDie("KillZone");
		}
	}

	public void NinjaDies()
	{
		if (!this.isDieing)
		{
			this.isDieing = true;
			this.iDie(string.Empty);
		}
	}

	private void iDie(string d)
	{
		this.isDieing = true;
		this.dashing = false;
		this.camTarget.transform.localPosition = Vector3.zero;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		this.AnimatorController.SetBool("Dashing", false);
		Vector3 position = base.gameObject.transform.position;
		if (d == "Doc")
		{
			this.caiXac.DocAction(position);
		}
		else if (d == "Xien")
		{
			this.caiXac.XienAction(position);
		}
		else if (d == "Ban")
		{
			this.caiXac.BanAction(position);
		}
		else if (d == "Chay")
		{
			this.caiXac.ChayAction(position);
		}
		else if (d == "Giat")
		{
			this.caiXac.GiatAction(position);
		}
		else
		{
			Transform transform = UnityEngine.Object.Instantiate(this.ragdoll1, position, base.gameObject.transform.rotation) as Transform;
			UnityEngine.Object.Destroy(transform.gameObject, 3f);
		}
		if (this.trainLine)
		{
			this.trainLine.gameObject.SetActive(false);
		}
		base.gameObject.transform.position += new Vector3(0f, 70f, 0f);
		this.MainEventsLog_script.PlayerDied(d);
		this.effectController.PlayerDie();
		this.tgbv = Time.time + 5f;
		this.Checking = false;
		this.AnimatorController.SetBool("Checking", false);
		this.timeCheckingAdd = 0f;
		this.tgtrans = Time.time;
		this.MainEventsLog_script.RemoveControl();
		this.Btn_Left_bool = false;
		this.Btn_Right_bool = false;
		if (this.MainEventsLog_script.Life > 0)
		{
			base.Invoke("BackToCP", 2f);
			base.Invoke("iReborn", 3f);
		}
		else
		{
			// base.Invoke("iHetMang", 1f);
			WalletController.Instance.OnDead(onRebordHandle);
		}
		
	}

	void onRebordHandle()
	{
		base.Invoke("BackToCP", 2f);
		base.Invoke("iReborn", 3f);
	}

	public void iReborn()
	{
		this.timeToDash = Time.time;
		this.timeToTrans = Time.time;
		this.PlayerSpeed = this.PlayerSpeedtg;
		base.gameObject.layer = 8;
		this.isTransing = false;
		this.Transformer.gameObject.SetActive(false);
		this.ninjaroot.gameObject.SetActive(true);
		this.trainLine.gameObject.SetActive(true);
		this.isDieing = false;
		this.timeCheckingAdd = 0f;
		base.Invoke("getct", 1.5f);
		this.caiXac.HideNow();
		this.MainEventsLog_script.ComeBackCP();
		base.gameObject.transform.position = this.ActiveCheckpoint;
		this.cameraScript.FollowSpeed -= 3f;
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (this.trainLine)
		{
			this.trainLine.gameObject.SetActive(true);
		}
	}

	private void getct()
	{
		this.MainEventsLog_script.GetControl();
	}

	private void iHetMang()
	{
		this.cameraScript.FollowSpeed += 3f;
		this.MainEventsLog_script.HetMang();
	}

	public void FootStep()
	{
		this.MainEventsLog_script.Step();
	}

	public void CritEnemy()
	{
		this.effectController.Crit();
	}

	public float PlayerSpeed;

	private float PlayerSpeedtg;

	public float PlayerSpeedTrans;

	[HideInInspector]
	public bool MaxSkillDash;

	[HideInInspector]
	public bool MaxSkillFi;

	public float maxVelocity;

	public float dashSpeed;

	public float JumpForce;

	public Transform dashPos;

	public Transform trainLine;

	public Transform FiPos;

	public Transform ragdoll1;

	public Transform ragdollTotal;

	public Transform Transformer;

	public Transform ninjaroot;

	private XacChetChay caiXac;

	public GameObject dm;

	public float FiCoolDown;

	private float FiGCoolDown = 3f;

	public float timeForChecking;

	[HideInInspector]
	public float DashCoolDown;

	[HideInInspector]
	public float TransCoolDown;

	[HideInInspector]
	public float timeBV;

	[HideInInspector]
	public float timeTrans;

	public float gravityScalef;

	[HideInInspector]
	public bool BVed;

	private float tgbv;

	private float tgtrans;

	public Transform camTarget;

	public float timeToMoveTarget = 1f;

	private Vector3 camTargetStartPos;

	private Vector3 camTargetStartIventPos;

	private bool canFi;


	private int dashOk;

	[HideInInspector]
	public bool dashing;

	private float timeCheckingAdd;

	private bool DoubleJump;

	public bool isDieing;

	public MainEventsLog MainEventsLog_script;

	private bool DJ_available;

	private float JumpForceCount;

	private bool IsGrounded;

	public List<GameObject> GroundedToObjectsList;

	public List<GameObject> WalledToObjectsList;

	private float walljump_count;

	private bool WallTouch;

	private bool WallGripJustStarted;

	[HideInInspector]
	public bool PlayerLooksRight;

	private bool NoNeedForSafeJump_bool;

	private int JustPressedSpace;

	private int FixStateTimer;

	private int OnCollisionStayCounter;

	private int OnCollisionBugThreshold;

	private int UpInTheAir_Counter;

	private float Ground_X_MIN;

	private float Ground_X_MAX;

	private float Ground_Y_MIN;

	private float Ground_Y_MAX;

	public GameObject NinjaPlatformRoot_PREFAB;

	private NinjaPlatformRoot NinjaPlatformRoot;

	public GameObject NinjaVisualRoot;

	public Vector3 ActiveCheckpoint;

	[NonSerialized]
	public bool Btn_Left_bool;

	[NonSerialized]
	public bool Btn_Right_bool;

	private bool Btn_Jump_bool;

	[HideInInspector]
	public bool Freezz;

	[HideInInspector]
	public bool Checking;

	public Animator AnimatorController;

	public GameObject MySpriteOBJ;

	private Vector3 MySpriteOriginalScale;

	public ParticleSystem WallGripParticles;

	private int WallGripEmissionRate;

	public ParticleSystem JumpParticles_floor;

	public ParticleSystem JumpParticles_wall;

	public ParticleSystem JumpParticles_doublejump;

	public ParticleSystem Particles_DeathBoom;

	public EffectController effectController;

	public AudioSource AudioSource_Jump;

	public AudioSource AudioSource_Fi;

	public AudioSource AudioSource_Dash;

	public AudioSource AudioSource_Wall;

	private bool dangphattiengtreo;

	private FiTieuPool fiPool;

	private bool stoping;

	public float forceUp;

	public LayerMask dashLayer;

	private CameraFollowTarget cameraScript;

	private float timeToDash;

	private float timeToTrans;

	[HideInInspector]
	public bool isTransing;

	private int j2 = 2;

	public ParticleSystem CheckParticle;

	public AudioSource checkAudio;

	public GameObject checkParticleInstance;

	private bool IsHeOnPlatform = false;
}
