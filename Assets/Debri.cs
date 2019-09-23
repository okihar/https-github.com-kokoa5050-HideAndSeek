using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    //1秒あたりの回転速度
    public float angle = 30F;
    //破壊時の得点
    public int score = 10;

    //回転の中心座標
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        //シーンの中のEarthオブジェクトへアクセスしてEarthオブジェクトのTransformコンポーネントにアクセスする
        Transform target = GameObject.Find("Earth").transform;
        //Earthオブジェクトの位置情報を取得しておく
        targetPos = target.position;
        //宇宙ゴミの正面の向きをアースオブジェクトに向ける
        transform.LookAt(target);
        //宇宙ゴミを0から360の範囲でZ軸を中心に回転させておく
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)), Space.World);


    }

    // Update is called once per frame
    void Update()
    {
        //アースを中心に宇宙ゴミの現在方向を、毎秒angle分だけ回転する
        Vector3 axis = transform.TransformDirection(Vector3.up);
        transform.RotateAround(targetPos, axis, angle * Time.deltaTime);
        
    }

     void OnMouseDown()
    {
        //スコアを加算する
        GameObject.Find("Score").SendMessage("AddScore", score);
        //クリックされたら宇宙ゴミを消す
        Destroy(gameObject);
        
    }
}
