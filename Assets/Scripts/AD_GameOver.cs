using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AD_GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        
    }
}
