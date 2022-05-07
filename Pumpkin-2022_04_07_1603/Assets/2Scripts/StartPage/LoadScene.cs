using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Animator SeceneTransition;
    public void StartClick(string sceneName)
    {
        PlayerData.Instance.CalcDmg();
        StartCoroutine(FadeInOut1());
    }
    IEnumerator FadeInOut1()
    {
        //SeceneTransition.gameObject.SetActive(true);
        SeceneTransition.SetTrigger("end");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
