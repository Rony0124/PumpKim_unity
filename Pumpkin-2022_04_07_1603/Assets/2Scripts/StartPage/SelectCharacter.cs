using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public Character character;
    SpriteRenderer sr;
    public SelectCharacter[] characters;
    // Update is called once per frame
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (DataMng.instance.currentCharacter == character) OnSelect();
        else OnDeSelect();
    }
    private void OnMouseUpAsButton()
    {
        DataMng.instance.currentCharacter = character;
        OnSelect();
        for (int i =0; i < characters.Length; i++)
        {
            if (characters[i] != this) characters[i].OnDeSelect();
        }
    }
    void OnSelect()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    void OnDeSelect()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
    }
}
