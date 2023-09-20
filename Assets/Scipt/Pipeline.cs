using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float speed = 2;//移動速度
    public float x_Limit = 5;//水管寬度
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;//水管移動
        if (this.transform.position.x <= x_Limit)
        {
            Destroy(this.gameObject);
        }
    }
}
