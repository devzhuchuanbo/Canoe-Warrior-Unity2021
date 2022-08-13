using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
	private void Start()
	{
		this.AudioS = base.GetComponent<AudioSource>();
		this.timeToPlaySound = Time.time;
	}

	public void ToeMau(Vector2 p)
	{
		this.MauFiteu.transform.position = p;
		this.MauFiteu.Play();
		if (this.MauFiteuClips.Length > 0 && Time.time >= this.timeToPlaySound)
		{
			this.AudioS.clip = this.MauFiteuClips[UnityEngine.Random.Range(0, this.MauFiteuClips.Length)];
			this.AudioS.Play();
		}
	}

	public void Crit()
	{
		if (this.CritClips.Length > 0)
		{
			this.AudioS.clip = this.CritClips[UnityEngine.Random.Range(0, this.CritClips.Length)];
			this.AudioS.Play();
		}
		this.timeToPlaySound = Time.time + 0.1f;
	}

	public void FireInTheGun(Vector2 pos, Vector2 dir)
	{
		if (dir == Vector2.left)
		{
			this.BulletSmoke.transform.position = pos - new Vector2(7f, 0f);
		}
		else
		{
			this.BulletSmoke.transform.position = pos + new Vector2(7f, 0f);
		}
		this.BulletSmoke.Play();
	}

	public void EnterCP()
	{
		this.AudioSCP.clip = this.enterCpClip;
		this.AudioSCP.Play();
	}

	public void DamBayGau()
	{
		if (this.damBayGauClip)
		{
			this.AudioSTrapGau.clip = this.damBayGauClip;
			this.AudioSTrapGau.Play();
		}
	}

	public void DamBayLong()
	{
		if (this.LinhLongClip)
		{
			this.AudioSDM.clip = this.LinhLongClip;
			this.AudioSDM.Play();
		}
	}

	public void LongRoi()
	{
		if (this.LongRoiClip)
		{
			this.AudioSTrapGau.clip = this.LongRoiClip;
			this.AudioSTrapGau.Play();
		}
	}

	public void ToeMauXanh(Vector2 p)
	{
		this.MauXanh.transform.position = p;
		this.MauXanh.Play();
		if (this.MauXanhClips.Length > 0 && Time.time >= this.timeToPlaySound)
		{
			this.AudioS.clip = this.MauXanhClips[UnityEngine.Random.Range(0, this.MauXanhClips.Length)];
			this.AudioS.Play();
		}
	}

	public void ToeDat(Vector2 p)
	{
		this.DatDa.transform.position = p;
		this.DatDa.Play();
		if (this.DatDaClips.Length > 0)
		{
			this.AudioS.clip = this.DatDaClips[UnityEngine.Random.Range(0, this.DatDaClips.Length)];
			this.AudioS.Play();
		}
	}

	public void ToeLua(Vector2 p)
	{
		this.TiaLua.transform.position = p;
		this.TiaLua.Play();
		if (this.TiaLuaClips.Length > 0)
		{
			this.AudioS.clip = this.TiaLuaClips[UnityEngine.Random.Range(0, this.TiaLuaClips.Length)];
			this.AudioS.Play();
		}
	}

	public void ToeGo(Vector2 p)
	{
		this.ManhGo.transform.position = p;
		this.ManhGo.Play();
		if (this.ManhGoClips.Length > 0)
		{
			this.AudioS.clip = this.ManhGoClips[UnityEngine.Random.Range(0, this.ManhGoClips.Length)];
			this.AudioS.Play();
		}
	}

	public void ToeThungGo(Vector2 p)
	{
		this.ManhThungGo.transform.position = p;
		this.ManhThungGo.Play();
		this.AudioSThungGo.Play();
	}

	public void ToeSu(Vector2 p)
	{
		this.ManhSu.transform.position = p;
		this.ManhSu.Play();
		if (this.ManhSuClips.Length > 0)
		{
			this.AudioS.clip = this.ManhSuClips[UnityEngine.Random.Range(0, this.ManhSuClips.Length)];
			this.AudioS.Play();
		}
	}

	public void ToeTuyet(Vector2 p)
	{
		this.KhoiTuyet.transform.position = p;
		this.KhoiTuyet.Play();
		if (this.KhoiTuyetClips.Length > 0)
		{
			this.AudioS.clip = this.KhoiTuyetClips[UnityEngine.Random.Range(0, this.KhoiTuyetClips.Length)];
			this.AudioS.Play();
		}
	}

	public void HoiSinh(Vector2 p)
	{
		this.HoiSinhParticle.transform.position = p;
		this.HoiSinhParticle.Play();
		if (this.HoiSinhClip)
		{
			this.audioSRieng.clip = this.HoiSinhClip;
			this.audioSRieng.Play();
		}
	}

	public void AnTien(Vector2 p)
	{
		this.TienVang.transform.position = p;
		this.TienVang.Play();
		if (this.TienVangClips.Length > 0)
		{
			this.AudioS.clip = this.TienVangClips[UnityEngine.Random.Range(0, this.TienVangClips.Length)];
			this.AudioS.Play();
		}
	}

	public void AnDM(Vector2 p)
	{
		this.DMparticle.transform.position = p;
		this.DMparticle.Play();
		if (this.DMClip)
		{
			this.AudioSDM.clip = this.DMClip;
			this.AudioSDM.Play();
		}
	}

	public void AnScroll(Vector2 p)
	{
		this.LightParticle.transform.position = p;
		this.LightParticle.Play();
		if (this.scrollClip)
		{
			this.AudioSDM.clip = this.scrollClip;
			this.AudioSDM.Play();
		}
	}

	public void AnMau(Vector2 p)
	{
		this.AnMauParticle.transform.position = p;
		this.AnMauParticle.Play();
		if (this.AnMauClip)
		{
			this.audioSRieng.clip = this.AnMauClip;
			this.audioSRieng.Play();
		}
	}

	public void BoomNo(Vector2 p)
	{
		this.BoomNoParticle.transform.position = p;
		this.BoomNoParticle.Play();
		if (this.boomNoClip)
		{
			this.audioSRieng.clip = this.boomNoClip;
			this.audioSRieng.Play();
		}
	}

	public void BoomXi()
	{
		if (this.boomXiClip)
		{
			this.audioSRieng.clip = this.boomXiClip;
			this.audioSRieng.Play();
		}
	}

	public void BV(float t)
	{
		this.vongBaoVe.BV(t);
		if (this.BVClip)
		{
			this.audioSRieng.clip = this.BVClip;
			this.audioSRieng.Play();
		}
	}

	public void SumonBat(Vector2 p)
	{
		this.LightParticle.transform.position = p;
		this.LightParticle.Play();
	}

	public void PlayerDie()
	{
		this.vongBaoVe.playerDie();
	}

	public void FiG(Vector2 p)
	{
	}

	public ParticleSystem MauFiteu;

	public ParticleSystem MauXanh;

	public ParticleSystem DatDa;

	public ParticleSystem TienVang;

	public ParticleSystem DMparticle;

	public ParticleSystem TiaLua;

	public ParticleSystem ManhGo;

	public ParticleSystem ManhSu;

	public ParticleSystem KhoiTuyet;

	public ParticleSystem LightParticle;

	public ParticleSystem HoiSinhParticle;

	public ParticleSystem FiGParticle;

	public ParticleSystem AnMauParticle;

	public ParticleSystem ManhThungGo;

	public ParticleSystem BoomNoParticle;

	public ParticleSystem BulletSmoke;

	public AudioClip[] MauFiteuClips;

	public AudioClip[] CritClips;

	public AudioClip[] MauXanhClips;

	public AudioClip[] DatDaClips;

	public AudioClip[] TiaLuaClips;

	public AudioClip[] ManhGoClips;

	public AudioClip[] ManhSuClips;

	public AudioClip[] KhoiTuyetClips;

	public AudioClip[] TienVangClips;

	public AudioClip HoiSinhClip;

	public AudioClip AnMauClip;

	public AudioClip BVClip;

	public AudioClip DMClip;

	public AudioClip scrollClip;

	public AudioClip boomNoClip;

	public AudioClip boomXiClip;

	public AudioClip enterCpClip;

	public AudioClip damBayGauClip;

	public AudioClip LinhLongClip;

	public AudioClip LongRoiClip;

	public VongBaoVe vongBaoVe;

	private AudioSource AudioS;

	public AudioSource audioSRieng;

	public AudioSource AudioSThungGo;

	public AudioSource AudioSDM;

	public AudioSource AudioSCP;

	public AudioSource AudioSTrapGau;

	private float timeToPlaySound;
}
