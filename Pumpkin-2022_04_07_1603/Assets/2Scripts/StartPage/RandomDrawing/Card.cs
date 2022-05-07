using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade { SSS, AA, B, F}

[System.Serializable]
public class Card 
{
    public int itemId;
    public string cardName;
    public Sprite cardImage;
    public float weight;
    public CardGrade cardGrade;

    public Card(Card card)
    {
        this.itemId = card.itemId;
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.weight = card.weight;
        this.cardGrade = card.cardGrade;
    }
}
