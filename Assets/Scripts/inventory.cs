using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
	public GameObject[] slots = new GameObject[24];
	public GameObject camTarget;

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

	public void AddItem(GameObject x, GameObject y, Transform z) { //dodanie przedmiotu do ekwipunku
		bool isSpace = false;
		for(int i=8; i<24; i++) {
			if(slots[i]==null) {
				slots[i] = Instantiate(x) as GameObject; //skopiowanie przedmiotu do slotu w ekwipunku
				slots[i].GetComponent<pickable>().parentObj = gameObject;
				slots[i].GetComponent<pickable>().indexInWindow = i;
				y.SetActive(false);
				z.GetComponent<interactable>().pickables[x.GetComponent<pickable>().indexInWindow] = null;
				isSpace = true;
				break;
			}
		}
		if(!isSpace)
			camTarget.GetComponent<gui>().DisplayInfo("Ekwipunek jest pełny!");
	}

	public void AddUsable(int index) //aktualizowanie statystyk przy zmianie używanej rzeczy
	{
		int x = index;
		switch(slots[index].GetComponent<pickable>().type)
		{
			case "chest":
				x = 5;
			break;
			default:
				x = index;
			break;
		}
		if(x != index)
		{
			if(slots[x] != null) //jeśli na tym miejscu jest już założoy jakiś przedmiot
			{
				gameObject.GetComponent<alive>().basicHp -= slots[x].GetComponent<pickable>().hp;
				gameObject.GetComponent<alive>().strenght -= slots[x].GetComponent<pickable>().strenght;
				gameObject.GetComponent<alive>().agility -= slots[x].GetComponent<pickable>().agility;
				gameObject.GetComponent<alive>().spirit -= slots[x].GetComponent<pickable>().spirit;
				gameObject.GetComponent<alive>().intellect -= slots[x].GetComponent<pickable>().intellect;
			}

			slots[x] = Instantiate(slots[index]); //załóż przedmiot
			Destroy(slots[index]); //usuń z hierarchii
			slots[index] = null; //usuń z plecaka

			gameObject.GetComponent<alive>().basicHp += slots[x].GetComponent<pickable>().hp;
			gameObject.GetComponent<alive>().strenght += slots[x].GetComponent<pickable>().strenght;
			gameObject.GetComponent<alive>().agility += slots[x].GetComponent<pickable>().agility;
			gameObject.GetComponent<alive>().spirit += slots[x].GetComponent<pickable>().spirit;
			gameObject.GetComponent<alive>().intellect += slots[x].GetComponent<pickable>().intellect;

			gameObject.GetComponent<alive>().UpdateStats();
		}
	}

	public void UseItem(int index)
	{
		if(slots[index].GetComponent<pickable>().type == "special")
		{
			camTarget.GetComponent<gui>().DisplayInfo("Nie możesz teraz użyć tego przedmiotu");
		}
		else
		{
			AddUsable(index);
		}
	}

	public void ThrowOut(int index)
	{
		Destroy(slots[index]);
		slots[index] = null;
	}
}
