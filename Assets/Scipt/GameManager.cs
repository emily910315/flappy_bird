using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 定義遊戲狀態的列舉型別
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
    public float pipelinePos_y = 4;// 下一個水管高度

    public float minRandom_y = 0.5f;// 隨機高度的最小值
    public float maxRandom_y = 1.5f;// 隨機高度的最大值

    public float timer = 0;// 計時器
    public float randomTime = 2;// 隨機生成水管的時間間隔
    public float minRandomTime = 0.5f;// 隨機生成時間的最小值
    public float maxRandomTime = 1.5f;// 隨機生成時間的最大值

    public GameSate gameSate = GameSate.Ready;// 遊戲狀態
    public Button start;// 開始按鈕
    //public Button restart;// 重新開始按鈕

    public static GameManager _gameManager;// GameManager的靜態實例
    public Rigidbody2D birdRigi;// 小鳥的Rigidbody2D元件
    public float birdGravity = 0.5f;// 小鳥的重力

    [SerializeField] GameObject bg;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject win;

    public void Quit()
    {
        Application.Quit();
    }
    void gamemeunopen()
    {
        Time.timeScale = 0f;
        bg.SetActive(true);
    }

    public void gamemeunclose()
    {
        Time.timeScale = 1f;
        bg.SetActive(false);
        gameSate = GameSate.Running;
    }

    public void instructionopen()
    {
        Time.timeScale = 0f;
        bg.SetActive(false);
        instruction.SetActive(true);
    }

    public void instructionclose()
    {
        Time.timeScale = 1f;
        bg.SetActive(false);
        instruction.SetActive(false);
        gameSate = GameSate.Running;
    }

    public void restart()
    {
        win.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        _gameManager = this;// 設置GameManager的靜態實例
        gamemeunopen();
    }

    void Update()
    {
        // 如果遊戲狀態不是運行中，則返回
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

    // 生成水管的方法
    void PipelineBorn()
    {
        // 隨機設置水管的高度
        pipelinePos_y = Random.Range(minRandom_y, maxRandom_y);

        // 設置生成下一個水管的位置
        print("test");
        Vector3 pipelinePos = new Vector2(pipelinePos_x, pipelinePos_y);//生成下一個水管位置
        Instantiate(pipelineObj, pipelinePos, pipelineObj.transform.rotation);

        // 隨機設置下一次生成水管的時間
        randomTime = Random.Range(minRandomTime, maxRandomTime);//0.5-1.5秒內隨機產生生成水管
        timer = 0;// 重置計時器
    }

    // 遊戲開始的方法
    public void GameStart()
    {
        gameSate = GameSate.Running;// 將遊戲狀態設置為運行中
        PipelineBorn();//生成下一個水管

        gamemeunopen();

    }

}
