using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
	public float interactionTime = 1;
	public GameObject[] pickables;
	public string interactionText;
	public bool isLoot;
	public GameObject playerThatClicked;
    
    void Start()
    {
		for(int i=0; i < pickables.Length; i++)
		{
			pickables[i].GetComponent<pickable>().indexInWindow = i;
			pickables[i].GetComponent<pickable>().parentObj = gameObject;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
