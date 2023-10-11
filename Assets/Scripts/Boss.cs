using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float _velocBoss = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Mathf.PingPong (Time.time, 5)
        transform.position = new Vector3(0, move, 0);   
    }
}
