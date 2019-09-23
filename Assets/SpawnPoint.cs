using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //宇宙ゴミプレハブ
    public GameObject debri;
    //宇宙ゴミ発生間隔
    public float interval = 1F;

    //宇宙ゴミ発生中フラグ
    private bool spawnStarted = false;

    void StartSpawn()
    {
        if (!spawnStarted)
        {
            spawnStarted = true;
            StartCoroutine("SpawnDebris");
        }
    }

    //宇宙ゴミ発生停止
    void StopSpawn()
    {
        if (spawnStarted)
        {
            spawnStarted = false;
            StopCoroutine("SpawnDebris");
        }
    }
    void Start()
    {
        //SpawnDebris()　コルーチンを開始する
        StartCoroutine("SpawnDebris");
        
    }
    //宇宙ごみ発生
    IEnumerator SpawnDebris()
    {
        //無限ループ
        while (true)
        {
            //宇宙ゴミプレハブを、スポーンポイントオブジェクトの位置にインスタンス化する
            Instantiate(debri, transform.position, Quaternion.identity);
            //interval分だけ処理を停止する
            yield return new WaitForSeconds(interval);
        }
    }
}
