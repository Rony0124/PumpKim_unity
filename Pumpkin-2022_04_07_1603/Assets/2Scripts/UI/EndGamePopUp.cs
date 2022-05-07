using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePopUp : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        goldText.text = UIController.Instance.txt.text;
        scoreText.text = StageMng.Instance.currentStage.ToString();
    }
    private void OnDisable()
    {
        goldText.text = "";
        scoreText.text = "";
    }
}
