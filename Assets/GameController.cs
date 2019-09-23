using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // タイトル画像
    public Image guiTitle;
    // タイムアップ画像
    public Image guiTimeup;

    // ゲームの状態定義
    public enum GameState
    {
        TITILE, // タイトル
        PLAYING,    // プレイ中
        TIMEUP, // タイムアップ
        TIMEUP_TO_TITLE,    // タイムアップからタイトルへ変更中
    }
    // ゲームの状態
    private GameState state;

    //  SpawnPointゲームオブジェクト 
    private GameObject spawnPoint;
    // Scoreゲームオブジェクト
    private GameObject score;
    // Timerコンポーネント
    private Timer timer;

    //  ゲームの初期化
    void Start()
    {
        // 状態をタイトルに
        state = GameState.TITILE;
        // タイトル画像を表示
        guiTitle.enabled = true;
        // タイムアップ画像を非表示
        guiTimeup.enabled = false;

        // SpawnPointゲームオブジェクトを取得      
        spawnPoint = GameObject.Find("SpawnPoint");
        // Scoreゲームオブジェクトを取得
        score = GameObject.Find("Score");
        // TimerゲームオブジェクトのTimerコンポーネントを取得
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    void Update()
    {
        switch (state)
        {
            case GameState.TITILE:
                // タイトル状態でマウスで左クリックされたらプレイ状態へ
                if (Input.GetMouseButtonUp(0))
                {
                    state = GameState.PLAYING;
                    // SpawnPointゲームオブジェクトにStartSpawn()関数を実行するようにメッセージを送る。宇宙ゴミの発生を開始
                    spawnPoint.SendMessage("StartSpawn");
                    // ScoreゲームオブジェクトにInitScore()関数を実行するようにメッセージを送る。スコアを初期化
                    score.SendMessage("InitScore");
                    // TimerコンポーネントのStartTimer()関数を実行。タイマーを開始
                    timer.StartTimer();
                    // タイトル画像を非表示
                    guiTitle.enabled = false;
                }
                break;
            case GameState.PLAYING:
                // プレイ中にTimerコンポーネントの残り時間が0になったらタイムアップ状態に
                if (timer.GetTimeRemaining() == 0)
                {
                    state = GameState.TIMEUP;
                    // SpawnPointゲームオブジェクトにStopSpawn()関数を実行するようにメッセージを送る。宇宙ゴミの発生を停止する
                    spawnPoint.SendMessage("StopSpawn");
                    // TimerコンポーネントのStopTimer()関数を実行。タイマーを停止
                    timer.StopTimer();
                    // 画面内の宇宙ゴミをすべて削除する
                    DestroyAllDebris();
                    // タイムアップ画像を表示
                    guiTimeup.enabled = true;
                }
                break;
            case GameState.TIMEUP:
                // タイムアップ状態でマウス左クリックで３秒後にタイトル状態にする
                if (Input.GetMouseButtonUp(0))
                {
                    state = GameState.TIMEUP_TO_TITLE;
                    StartCoroutine("ShowTitleDelayed", 3f);
                }
                break;
        }
    }

    //  シーン中のすべての宇宙ゴミを削除
    void DestroyAllDebris()
    {
        GameObject[] debris = GameObject.FindGameObjectsWithTag("debri");
        foreach (GameObject debri in debris)
        {
            Destroy(debri);
        }
    }

    //  delayTime秒後にタイトルを表示
    IEnumerator ShowTitleDelayed(float delayTime)
    {
        // delayTime秒処理を停止
        yield return new WaitForSeconds(delayTime);
        state = GameState.TITILE;
        // タイマーをリセット
        timer.ResetTimer();
        //  タイトル画像を表示
        guiTitle.enabled = true;
        // タイムアップ画像を非表示
        guiTimeup.enabled = false;
    }
}