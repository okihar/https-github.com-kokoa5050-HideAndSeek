using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // 残り時間の初期値
    public int timeLimit = 30;
    private Text text;


    // 残り時間
    private float timeRemaining;
    // タイマー動作フラグ
    private bool timerStarted;

    void Start()
    {
        text = GetComponent<Text>();
        ResetTimer();
    }

    //  タイマーをリセット
    public void ResetTimer()
    {
        timeRemaining = timeLimit;
        timerStarted = false;
    }

    //  タイマーを開始
    public void StartTimer()
    {
        timerStarted = true;
    }

    //  タイマーを停止
    public void StopTimer()
    {
        timerStarted = false;
    }

    //  残り時間を取得
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    void Update()
    {
        if (timerStarted)
        {
            // 残り時間を引いてゆく
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                //残り時間が0以下ならタイマーを停止する
                timeRemaining = 0;
                timerStarted = false;
            }
        }
        // テキストを更新
        text.text = "Time:" + timeRemaining;
    }
}
