using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    private Text text;

     void Awake()
    {
        text = GetComponent<Text>();
    }

    void InitScore()
    {
        this.score = 0;
        UpdateText();
    }

    void AddScore(int score)
    {
        this.score += score;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = "Score:" + this.score;
    }


}
