using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletController : MonoBehaviour
{
    public static WalletController Instance;

    public GameObject NewUser;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //UniClipboard.SetText("text you want to clip");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
