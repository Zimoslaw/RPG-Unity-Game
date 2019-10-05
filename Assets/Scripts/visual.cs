using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visual : MonoBehaviour
{
	float timer = 0;
	public float time = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;
		if(timer >= time + Mathf.Epsilon) {
			Destroy(gameObject);
		}
    }
}
