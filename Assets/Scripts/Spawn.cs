//// dnSpy decompiler from Assembly-UnityScript.dll class: Spawn
//using System;
//using Boo.Lang.Runtime;
//using UnityEngine;
//using UnityEngine.UI;

//[Serializable]
//public class Spawn : MonoBehaviour
//{
//	public Spawn()
//	{
//		this.prefabIndex = -1;
//	}

//	public virtual void SpawnSolo()
//	{
//		GameObject original = this.enemyPrefabs[this.prefabIndex];
//		UnityEngine.Object.Instantiate<GameObject>(original);
//		this.Texttime.text = this.prefabIndex.ToString();
//	}

//	public virtual void SpawnEnemies()
//	{
//		if (this.prefabIndex >= this.enemyPrefabs.Length)
//		{
//			this.prefabIndex = 0;
//		}
//		GameObject original = this.enemyPrefabs[this.prefabIndex];
//		UnityEngine.Object.Instantiate<GameObject>(original);
//		this.Texttime.text = this.prefabIndex.ToString();
//	}

//	public virtual void Update()
//	{
//		if (Input.GetButtonDown("Fire2"))
//		{
//			this.DestroyAllObjectsWithTag("Fx");
//			this.SpawnSolo();
//		}
//		if (Input.GetButtonDown("Fire1"))
//		{
//			this.DestroyAllObjectsWithTag("Fx");
//			this.prefabIndex++;
//			this.SpawnEnemies();
//		}
//	}

//	public virtual void DestroyAllObjectsWithTag(string tag)
//	{
//		foreach (object obj in GameObject.FindGameObjectsWithTag(tag))
//		{
//			object obj3;
//			object obj2 = obj3 = obj;
//			if (!(obj2 is GameObject))
//			{
//				obj3 = RuntimeServices.Coerce(obj2, typeof(GameObject));
//			}
//			GameObject obj4 = (GameObject)obj3;
//			UnityEngine.Object.Destroy(obj4);
//		}
//	}

//	public virtual void Main()
//	{
//	}

//	public GameObject[] enemyPrefabs;

//	public int prefabIndex;

//	public Text Texttime;
//}
