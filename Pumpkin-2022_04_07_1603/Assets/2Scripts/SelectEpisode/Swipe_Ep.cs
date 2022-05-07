using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Swipe_Ep : MonoBehaviour
{
    public Animator SeceneTransition;
    public GameObject scrollbar;
    public GameObject alarmPopup;
    public Text text;
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public AudioClip buttonCliclSFX;
    [SerializeField] public AudioClip buttonCliclSFX2;
    public float scroll_pos = 0;
    public float[] pos;
    public int sceneIndex;
    public bool isActive;
    //public DescriptionBehaviour descp;
    // Start is called before the first frame update
    void Start()
    {
       
        AudioMng.Instance.PlayMusicWithFade(audioClip);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                //descp.ScrollUp();
                sceneIndex = i + 3;
                if (PlayerData.Instance.records.Count >= i + 1)
                {
                    isActive = true;
                    ShowScore(i);
                }

                else
                {
                    transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.7f);
                    text.text = "Not Active";
                    isActive = false;
                }

                for (int a=0; a< pos.Length; a++)
                {
                    if(a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        transform.GetChild(a).GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    void ShowScore(int ep)
    {
        //text.transform.GetComponent<TextMeshPro>().text = "Best Score: 0";
        if (PlayerData.Instance.records[ep] == null)
        {
            text.text= "Open Next Ep Best Stage: 0";
            return;
        }
        else
        {
            Debug.Log("PlayerData best stage " + PlayerData.Instance.records[ep].bestStage);
            int bestStage = PlayerData.Instance.records[ep].bestStage;
            text.text = "Best Stage: "+ bestStage.ToString();
            return;
        }
    }

    public void NextScene()
    {
        if (isActive)
        {
            AudioMng.Instance.PlaySFX(buttonCliclSFX2);
            PlayerData.Instance.currentEpisode = sceneIndex - 2;
            //StageMng.Instance.currentEp = PlayerData.Instance.currentEpisode;
            StartCoroutine(FadeInOut1());
        }
        else
        {
            alarmPopup.SetActive(true);
            Debug.Log("Nope");
            //text.text += "/n You need to finish previous Episode";
        }
        
    }

    public void ClickNext()
    {
        if (scroll_pos >= 1f)
        {
            scroll_pos = 0f;
        }
        if (scroll_pos < 1f && scroll_pos >= 0f)
        {
            scroll_pos += 0.25f;
        }
        
    }
    public void ClickPrev()
    {
        if(scroll_pos <= 0f)
        {
            scroll_pos = 1f;
        }
        if (scroll_pos <= 1f && scroll_pos > 0f)
        {
            scroll_pos -= 0.25f;
        }
        
    }
    IEnumerator FadeInOut1()
    {
        //SeceneTransition.gameObject.SetActive(true);
        SeceneTransition.SetTrigger("end");

        yield return new WaitForSeconds(1.5f);
        //not active면 uicontroller로 창띄우기
        SceneManager.LoadScene(sceneIndex);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene(1);
    }
}
