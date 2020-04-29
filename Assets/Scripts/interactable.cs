using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public float interactionTime = 1;
	public GameObject[] pickables;
	public string interactionText;
	public bool isLoot;
	public GameObject playerThatClicked;

	private bool isRemoving = false; // czy obiekt znika z gry
	private Color fadedColor; // docelowy kolor (bezbarwny)
    
    void Start()
    {
		for(int i=0; i < pickables.Length; i++)
		{
			pickables[i].GetComponent<Pickable>().indexInWindow = i;
			pickables[i].GetComponent<Pickable>().parentObj = gameObject;
		}

		//inicjalizacja docelowego koloru materiału potrzebnego do zniknięcia obiektu (color.alpha = 0)
		fadedColor = gameObject.GetComponentInChildren<MeshRenderer>().material.color;
		fadedColor.a = 0;
    }

    void Update()
    {
		if(isRemoving)
		{
			gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(gameObject.GetComponentInChildren<MeshRenderer>().material.color, fadedColor, 2 * Time.deltaTime);
			if(gameObject.GetComponentInChildren<MeshRenderer>().material.color.a < .1f)
				Destroy(gameObject);
		}
    }

	public void RemoveSelf() // znikanie obiektu ze świata
	{
		isRemoving = true;
	}
}
