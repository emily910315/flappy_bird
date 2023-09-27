using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bird : MonoBehaviour
{
    public Vector2 bird_veloicity_y = new Vector2(1,5);
    public Sprite[] birdSprites;
    public float score = 0;
    [SerializeField]TextMeshProUGUI scoreText;
    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text= score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody2D rigid = this.gameObject.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.velocity = bird_veloicity_y;
            }
        }
    }
    //碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //死亡
        this.GetComponent<SpriteRenderer>().sprite = birdSprites[2];//死亡後小鳥圖變死掉的樣子

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("scorepipeline"))
        {
            //加分
            score += 1;
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
        
    }
}
