using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounp : MonoBehaviour
{
    public float speed = 2;//移動速度
    public float backgrounp_width = 10;//背景寬度



    void Update()
    {
        // 如果遊戲狀態不是運行中，則返回
        if (GameManager._gameManager.gameSate != GameSate.Running)
        {
            return;
        }

        this.gameObject.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;//背景移動

        // 如果背景位置超過指定寬度限制
        if (this.gameObject.transform.position.x <= -backgrounp_width)
        {
            // 使背景回到初始位置
            this.gameObject.transform.position = new Vector3(this.transform.position.x+2* backgrounp_width, this.transform.position.y, this.transform.position.z);
        }
    }
}
