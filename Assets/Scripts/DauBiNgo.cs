using System;
using UnityEngine;

public class DauBiNgo : MonoBehaviour
{
    private void Start()
    {
        ShowHead();
        //string @string = PlayerPrefs.GetString("TransInUse");
        //if (@string == "bienhinh_bingo" || @string == "bienhinh_bingo2")
        //{
        //	this.dauThuong.gameObject.SetActive(false);
        //	this.dauBiNgo.gameObject.SetActive(true);
        //}
        //else
        //{
        //	this.dauThuong.gameObject.SetActive(true);
        //	this.dauBiNgo.gameObject.SetActive(false);
        //}
    }
    private void OnEnable()
    {
        ShowHead();
    }
    void ShowHead()
    {
        if (PlayerPrefs.GetInt("Skin", 0) == 0)
        {
            dauThuong.gameObject.SetActive(true); ;
            dauBiNgo.gameObject.SetActive(false);
        }
        else
        {
            dauThuong.gameObject.SetActive(false); ;
            dauBiNgo.gameObject.SetActive(true);
        }
    }

    public Transform dauThuong;

    public Transform dauBiNgo;
}
