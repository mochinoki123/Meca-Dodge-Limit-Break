using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemymove : MonoBehaviour
{
    public float EnemyPosi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyPosi = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, EnemyPosi + Mathf.PingPong(Time.time*2, 3f), transform.position.z);
    }

    
}
