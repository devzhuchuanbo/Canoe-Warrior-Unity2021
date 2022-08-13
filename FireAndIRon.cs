using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Messaging;
using Firebase.Unity;
using AppsFlyerSDK;

public class FireAndIRon : MonoBehaviour {

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(this.gameObject);

#if UNITY_ANDROID
        string appKey = "ce46457d";
#elif UNITY_IPHONE
        string appKey = "ce46457d";
#else
        string appKey = "unexpected_platform";
#endif

        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.setUserId(AppsFlyer.getAppsFlyerId());
        IronSource.Agent.init(appKey);

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(continuationAction: task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
#if UNITY_ANDROID
        AppsFlyerAndroid.updateServerUninstallToken(token.Token);
#endif
    }


}
