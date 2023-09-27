using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        PipelineBorn();//生成下一個水管
    }

    void Update()
    {
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
}
