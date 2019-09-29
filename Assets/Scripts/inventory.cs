using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
	public GameObject[] slots = new GameObject[24];

    // Start is called before the first frame update
    void Start() {
		for(int i=0; i<24; i++) {
			if(slots[i]!=null) {
				gameObject.GetComponent<alive>().basicHp += slots[i].GetComponent<pickable>().hp;
				gameObject.GetComponent<alive>().strenght += slots[i].GetComponent<pickable>().strenght;
				gameObject.GetComponent<alive>().agility += slots[i].GetComponent<pickable>().agility;
				gameObject.GetComponent<alive>().spirit += slots[i].GetComponent<pickable>().spirit;
				gameObject.GetComponent<alive>().intellect += slots[i].GetComponent<pickable>().intellect;
			}	
		}
		gameObject.GetComponent<alive>().UpdateStats();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void AddItem(GameObject x) {
		for(int i=8; i<24; i++) {
			if(slots[i]==null) {
				slots[i] = x;
				break;
			}
		}
	}

	public void AddUsable(int x, GameObject y) { //aktualizowanie statystyk przy zmianie używanej rzeczy

		if(slots[x]!=null) {
			gameObject.GetComponent<alive>().basicHp -= slots[x].GetComponent<pickable>().hp;
			gameObject.GetComponent<alive>().strenght -= slots[x].GetComponent<pickable>().strenght;
			gameObject.GetComponent<alive>().agility -= slots[x].GetComponent<pickable>().agility;
			gameObject.GetComponent<alive>().spirit -= slots[x].GetComponent<pickable>().spirit;
			gameObject.GetComponent<alive>().intellect -= slots[x].GetComponent<pickable>().intellect;
		}

		slots[x] = y;

		gameObject.GetComponent<alive>().basicHp += slots[x].GetComponent<pickable>().hp;
		gameObject.GetComponent<alive>().strenght += slots[x].GetComponent<pickable>().strenght;
		gameObject.GetComponent<alive>().agility += slots[x].GetComponent<pickable>().agility;
		gameObject.GetComponent<alive>().spirit += slots[x].GetComponent<pickable>().spirit;
		gameObject.GetComponent<alive>().intellect += slots[x].GetComponent<pickable>().intellect;

		gameObject.GetComponent<alive>().UpdateStats();
	}
}
