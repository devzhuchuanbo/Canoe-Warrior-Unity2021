using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkipAnim : MonoBehaviour {

    public GameObject SkipBtn;
    public Transform loadingPanel;

    void Start()
    {
        SkipBtn.SetActive(false);

        StartCoroutine(Wait2());
       
    }
    public void FinishStoryAnimation()
    {
        SkipBtn.SetActive(false);
        this.loadingPanel.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("truc1");
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(5);
        SkipBtn.SetActive(true);
    }
}
