
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {

        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    public Transform Logo;
    public RectTransform Buttons;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip buttonCliclSFX;
    public Animator SeceneTransition;
    
    // Start is called before the first frame update
    void Start()
    {
       
       // AudioMng.Instance.PlayMusicWithFade(audioClip);
        Logo.DOMove(Vector2.zero, 1f);
       
    }
    public void BtnMove()
    {
        Buttons.gameObject.SetActive(true);
        StartCoroutine(FadingInBtn());
        //Buttons.GetChild(0)
        //Buttons.DOMove(Vector2.zero, 0.8f);
    }
    IEnumerator FadingInBtn()
    {
        float a = 0f;
        Buttons.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, a);
        //yield return new WaitForEndOfFrame();

        while (a <= 1f)
        {
            Buttons.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, a);
            a += 0.05f;
            yield return new WaitForSeconds(0.07f);
        }
        Buttons.GetComponent<Button>().interactable = true;
    }
    public void OnClickStart()
    {
        //AudioMng.Instance.PlaySFX(buttonCliclSFX);
        StartCoroutine(FadeInOut1());
    }
    public void OnClickContinue()
    {
        PlayerData.Instance.LoadPlayer(SaveSystem.Load());//굳이?
        StartCoroutine(FadeInOut1());
    }
    
    public void OnClickExit()
    {
        Application.Quit();
    }
 
    IEnumerator FadeInOut1()
    {
        //SeceneTransition.gameObject.SetActive(true);
        SeceneTransition.SetTrigger("end");
       
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
