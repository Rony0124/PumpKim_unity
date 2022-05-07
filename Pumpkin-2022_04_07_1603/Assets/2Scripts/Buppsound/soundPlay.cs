using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlay : MonoBehaviour
{
    AudioSource[] audioData;
    private int index = 0;

    // Start is called before the first frame update
    public void playSound()
    {
        audioData = GetComponents<AudioSource>();
        audioData[index++].Play();
        
        if(index == audioData.GetLength(0))
        {
            index = 0;
        }
        
    }
}
