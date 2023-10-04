using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameSate
{
    Ready = 0,
    Running = 1,
    Pause = 2,
    GameOver = 3,
}
public class GameManager : MonoBehaviour
{
    public GameObject pipelineObj;//生成的下一個水管物件

    public float pipelinePos_x = 5;//下一個水管寬度
    public float pipelinePos_y = 4;

    public float minRandom_y = 0.5f;
    public float maxRandom_y = 1.5f;

    public float timer = 0;
    public float randomTime = 2;
    public float minRandomTime = 0.5f;
    public float maxRandomTime = 1.5f;

    public GameSate gameSate = GameSate.Ready;
    public Button start;
    public Button restart;

    public static GameManager _gameManager;
    public Rigidbody2D birdRigi;
    public float birdGravity=1.5f;

    void Start()
    {
        _gameManager = this;
        start.gameObject.SetActive(true);//打開開始按鈕
        restart.gameObject.SetActive(false);//隱藏重新開始按鈕
    }

    void Update()
    {
        if (gameSate != GameSate.Running)
        {
            return;
        }

        //更新生成水管
        timer += Time.deltaTime;
        if (timer >= randomTime)//如果現在時間大於隨機生成時間
        {
            PipelineBorn();//生成水管
        }
    }

    void PipelineBorn()
    {
        pipelinePos_y = Random.Range(minRandom_y,maxRandom_y);

        Vector3 pipelinePos = new Vector3(pipelinePos_x, pipelinePos_y, pipelineObj.transform.position.z);//生成下一個水管位置
        Instantiate(pipelineObj,pipelinePos,pipelineObj.transform.rotation);

        randomTime = Random.Range(minRandomTime, maxRandomTime);//0.5-1.5秒內隨機產生生成水管
        timer = 0;
    }

    public void GameStart()
    {
        gameSate = GameSate.Running;//start時才可以做已下事情
        PipelineBorn();//生成下一個水管

        if (birdRigi != null)
        {
            birdRigi.gravityScale = birdGravity;//調整重力
        }

        start.gameObject.SetActive(false);//隱藏start按鈕
        restart.gameObject.SetActive(false);//隱藏restart按鈕
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("SampleScene");
        
    }
}
