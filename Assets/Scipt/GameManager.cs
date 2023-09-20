using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pipelineObj;//生成的下一個水管物件

    public float pipelinePos_x=5;//下一個水管寬度

    void Start()
    {
        PipelineBorn();//生成下一個水管
    }

    void Update()
    {
        
    }

    void PipelineBorn()
    {
        Vector3 pipelinePos = new Vector3(pipelinePos_x, pipelineObj.transform.position.y, pipelineObj.transform.position.z);//生成下一個水管位置
        Instantiate(pipelineObj,pipelinePos,pipelineObj.transform.rotation);
    }
}
