using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image chr;
    public Text cardName;
    Animator animator;
    public int ownNumber;
    public bool isChosen;
    public Card card;
    public CardUI[] cardUIs;
    //[SerializeField] public AudioClip buttonCliclSFX;
    private void Start()
    {
        animator = GetComponent<Animator>();
        //cardUIs = FindObjectsOfType<CardUI>();
    }
    // 카드의 정보를 초기화
    public void CardUISet(Card _card, int i)
    {
        chr.sprite = _card.cardImage;
        cardName.text = _card.cardName;
        ownNumber = i;
        card = _card;
    }
    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("CardAnimation")) 
        {
            //AudioMng.Instance.PlaySFX(buttonCliclSFX);
            animator.SetTrigger("Flip");
            isChosen = true;
            RandomSelect.instance.FilpAll();
        }
     
        //RandomSelect.instance.chosenCard = card;
    }
}
