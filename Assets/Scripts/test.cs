using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    float xp = 0;
    float xp2;
    float yp = 0;
    float yp2;

    public float funcTime = 20;
    public float exisTime = 10;
    public float funcSpeed = 2;
    public float a = 1;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(xp<=funcTime) {
            xp2 = xp;
            xp += Time.deltaTime*funcSpeed;
            yp2 = yp;
            yp = func(xp);
            Debug.DrawLine(new Vector3(xp2, yp2, 0), new Vector3(xp, yp, 0), Color.red, exisTime);
        } else {
            xp = 0;
            yp = 0;
            a += 1f;
        }
    }

    float func(float x) {
        return Mathf.Log(x,2)*a;
    }
}
