using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounp : MonoBehaviour
{
    public float speed = 2;//移動速度
    public float backgrounp_width = 10;//背景寬度
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameManager.gameSate != GameSate.Running)
        {
            return;
        }

        this.gameObject.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;//背景移動
        if (this.gameObject.transform.position.x <= -backgrounp_width)
        {
            this.gameObject.transform.position = new Vector3(this.transform.position.x+2* backgrounp_width, this.transform.position.y, this.transform.position.z);
        }
    }
}
