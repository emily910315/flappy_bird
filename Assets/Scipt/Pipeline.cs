using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed = 2;//移動速度
    public float x_Limit = 5;//水管寬度


    void Update()
    {
        // 如果遊戲狀態不是運行中，則返回
        if (GameManager._gameManager.gameSate != GameSate.Running)
        {
            return;
        }

        this.gameObject.transform.position += Vector3.left * speed * Time.deltaTime;//水管移動

        // 如果水管位置超過指定寬度限制
        if (this.transform.position.x <= x_Limit)
        {
            //Destroy(this.gameObject);// 刪除水管物件
        }
    }
}
