using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Bird : MonoBehaviour
{
    
    public Sprite[] birdSprites;// 存儲小鳥不同狀態的圖片
    public float score = 0;//分數
    [SerializeField]TextMeshProUGUI scoreText;// 顯示分數的TextMeshProUGUI元件
    public Vector2 bird_veloicity_y = new Vector2(1,5);// 小鳥的垂直速度向量
    private bool hasEnteredScoreZone = false;
    void Start()
    {
        // 如果分數文本不為空，將其設置為初始分數
        if (scoreText != null)
        {
            scoreText.text= score.ToString();
        }

        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;//開始時重力為0
    }


    void Update()
    {
        // 如果遊戲狀態不是運行中，則返回
        if (GameManager._gameManager.gameSate != GameSate.Running)
        {
            return;
        }

        // 取得小鳥的Rigidbody2D元件
        //Rigidbody2D rigid = this.gameObject.GetComponent<Rigidbody2D>();

        // 取得滑鼠位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 設置小鳥位置為滑鼠位置
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);


        
    }

    

    //碰撞(當小鳥碰撞時的處理)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 遊戲進入結束狀態(死亡)
        // GameManager._gameManager.gameSate = GameSate.GameOver;
        //this.GetComponent<SpriteRenderer>().sprite = birdSprites[2];//死亡後小鳥圖變死掉的樣子

        // 隱藏重新開始按鈕
        //GameManager._gameManager.restart.gameObject.SetActive(false);//隱藏restart按鈕
    }

        // 當進入特定區域時的處理
        private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞物件標籤為"scorepipeline"
        if (collision.gameObject.CompareTag("scorepipeline") && !hasEnteredScoreZone)
        {
            // 設置標誌為 true
            hasEnteredScoreZone = true;

            //加分
            score += 1;
            if (scoreText != null)
            {
                // 更新分數顯示
                scoreText.text = score.ToString();
            }
        }
    }

    // 在離開計分區域時重置標誌
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("scorepipeline"))
        {
            hasEnteredScoreZone = false;
        }
    }

}
