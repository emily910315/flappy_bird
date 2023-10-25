using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Bird : MonoBehaviour
{
    private bool hasEnteredScoreZone = false;
    public float score = 0;//分數
    [SerializeField] TextMeshProUGUI scoreText;// 顯示分數的TextMeshProUGUI元件
    //public Vector2 bird_veloicity_y = new Vector2(1,5);// 小鳥的垂直速度向量
    private Collider2D lastPipeCollider = null;
    private bool canScore = true;
    public float scoreDelay;

    private Vector2 mousePosition;
    private Vector2 distance;
    Rigidbody2D rb20;
    private Vector2 offset; // 用於記錄滑鼠按下位置和物體位置的偏移

    public float fallSpeed = 5.0f; // 落下的速度

    private bool isDragging = false;// 用來檢查是否正在拖拽
    private bool allowAutoFall = false;// 控制是否允許自動落下
    public float bufferTime = 10f;// 緩衝時間
    private Vector2 lastMousePosition;
    public float mouseMoveThreshold = 0.1f;

    void Start()
    {
        // 如果分數文本不為空，將其設置為初始分數
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        rb20 = GetComponent<Rigidbody2D>();
        rb20.gravityScale = 0;//開始時重力為0

        StartCoroutine(EnableAutoFall()); // 啟用自動落下
        lastMousePosition = Input.mousePosition;
        
    }

    IEnumerator EnableAutoFall()
    {
        yield return new WaitForSeconds(bufferTime);
        allowAutoFall = true; // 緩衝時間後允許自動落下
    }


    void Update()
    {
        // 如果遊戲狀態不是運行中，則返回
        if (GameManager._gameManager.gameSate != GameSate.Running)
        {
            return;
        }

        if (Input.GetMouseButton(0)) // 0 表示左鍵
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }

        Vector2 currentMousePosition = Input.mousePosition;

        if (Vector2.Distance(currentMousePosition, lastMousePosition) > mouseMoveThreshold)
        {
            // 如果滑鼠有移動，重置緩衝時間
            StartCoroutine(EnableAutoFall());
        }

        lastMousePosition = currentMousePosition;

        if (allowAutoFall && !isDragging)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
            transform.position = currentPosition;
        }

        


    }

    private void OnMouseDown()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = currentPosition - mousePosition;
        isDragging = true;
    }    

    private void OnMouseDrag()
    {
        Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentMousePos + offset;
        rb20.velocity = Vector2.zero;
    }

    private void OnMouseUpAsButton()
    {
        rb20.gravityScale = 3;
        isDragging = false;
    }


    //碰撞(當小鳥碰撞時的處理)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        // 遊戲進入結束狀態(死亡)
        GameManager._gameManager.gameSate = GameSate.GameOver;

        // 隱藏重新開始按鈕
        //GameManager._gameManager.restart.gameObject.SetActive(false);//隱藏restart按鈕
    }

    //當進入特定區域時的處理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰撞物件標籤為"scorepipeline"
        if (collision.gameObject.CompareTag("scorepipeline") && !hasEnteredScoreZone && canScore)
        {
            // 設置標誌為 true
            hasEnteredScoreZone = true;


            canScore = false;


            score += 1;

            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }


            Invoke("ResetCanScore", scoreDelay);
        }

    }

    private void ResetCanScore()
    {
        // Reset the scoring flag after the delay
        canScore = true;
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
