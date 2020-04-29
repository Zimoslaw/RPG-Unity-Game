using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
	public GameObject[] slots = new GameObject[24];
	public GameObject camTarget;
	public GameObject throwOutObject;

    // Start is called before the first frame update
    void Start() {
		for(int i=0; i<8; i++) {
			if(slots[i]!=null) {
				gameObject.GetComponent<Alive>().basicHp += slots[i].GetComponent<Pickable>().hp;
				gameObject.GetComponent<Alive>().strenght += slots[i].GetComponent<Pickable>().strenght;
				gameObject.GetComponent<Alive>().agility += slots[i].GetComponent<Pickable>().agility;
				gameObject.GetComponent<Alive>().spirit += slots[i].GetComponent<Pickable>().spirit;
				gameObject.GetComponent<Alive>().intellect += slots[i].GetComponent<Pickable>().intellect;
			}	
		}
		gameObject.GetComponent<Alive>().UpdateStats();
	}

	public void AddItem(GameObject x, GameObject y, Transform z) { //dodanie przedmiotu do ekwipunku
		bool isSpace = false;
		for(int i=8; i<24; i++) {
			if(slots[i]==null) {
				slots[i] = Instantiate(x) as GameObject; //skopiowanie przedmiotu do slotu w ekwipunku
				slots[i].GetComponent<Pickable>().parentObj = gameObject;
				slots[i].GetComponent<Pickable>().indexInWindow = i;
				y.SetActive(false);
				z.GetComponent<Interactable>().pickables[x.GetComponent<Pickable>().indexInWindow] = null;
				isSpace = true;
				break;
			}
		}
		if(!isSpace)
			camTarget.GetComponent<GUI>().DisplayInfo("Ekwipunek jest pełny!");
	}

	public void AddUsable(int index) //aktualizowanie statystyk przy zmianie używanej rzeczy
	{
		int x = index;
		switch(slots[index].GetComponent<Pickable>().type)
		{
			case "ranged weapon":
				x = 0;
			break;
			case "melee weapon":
				x = 0;
			break;
			case "necklace":
				if(slots[2] == null)
					x = 2;
				else if(slots[3] == null)
					x = 3;
				else
					x = 2;
				break;
			case "ring":
				if(slots[2] == null)
					x = 2;
				else if(slots[3] == null)
					x = 3;
				else
					x = 2;
				break;
			case "head":
				x = 4;
			break;
			case "chest":
				x = 5;
			break;
			case "legs":
				x = 6;
			break;
			case "foots":
				x = 7;
			break;
			default:
				x = index;
			break;
		}
		if(x != index)
		{
			GameObject temp = null;
			if(slots[x] != null) //jeśli na tym miejscu jest już założoy jakiś przedmiot
			{
				gameObject.GetComponent<Alive>().basicHp -= slots[x].GetComponent<Pickable>().hp;
				gameObject.GetComponent<Alive>().strenght -= slots[x].GetComponent<Pickable>().strenght;
				gameObject.GetComponent<Alive>().agility -= slots[x].GetComponent<Pickable>().agility;
				gameObject.GetComponent<Alive>().spirit -= slots[x].GetComponent<Pickable>().spirit;
				gameObject.GetComponent<Alive>().intellect -= slots[x].GetComponent<Pickable>().intellect;
				temp = slots[x];
			}

			slots[x] = Instantiate(slots[index]); //załóż przedmiot
			Destroy(slots[index]); //usuń z hierarchii
			if(temp != null)
			{
				slots[index] = Instantiate(temp); //zamień przedmioty
				slots[index].GetComponent<Pickable>().indexInWindow = index;
				camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[index].GetComponent<ItemButton>().item = slots[index];
				camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[index].GetComponent<ItemButton>().RefreshIcon();
				Destroy(temp);
			}
			else
			{
				slots[index] = null; //usuń z plecaka
				camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[index].SetActive(false);
			}

			slots[x].GetComponent<Pickable>().indexInWindow = x;
			camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[x].GetComponent<ItemButton>().item = slots[x];
			camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[x].GetComponent<ItemButton>().RefreshIcon();
			camTarget.GetComponent<GUI>().inventory.GetComponent<InventoryWindow>().itemButtons[x].SetActive(true);

			gameObject.GetComponent<Alive>().basicHp += slots[x].GetComponent<Pickable>().hp;
			gameObject.GetComponent<Alive>().strenght += slots[x].GetComponent<Pickable>().strenght;
			gameObject.GetComponent<Alive>().agility += slots[x].GetComponent<Pickable>().agility;
			gameObject.GetComponent<Alive>().spirit += slots[x].GetComponent<Pickable>().spirit;
			gameObject.GetComponent<Alive>().intellect += slots[x].GetComponent<Pickable>().intellect;

			gameObject.GetComponent<Alive>().UpdateStats();
		}
	}

	public void UseItem(int index)
	{
		if(slots[index].GetComponent<Pickable>().type == "special")
		{
			camTarget.GetComponent<GUI>().DisplayInfo("Nie możesz teraz użyć tego przedmiotu");
		}
		else
		{
			AddUsable(index);
		}
	}

	public void ThrowOut(int index)
	{
		if(index < 8) //usuwanie statystyk przedmiotu
		{
			gameObject.GetComponent<Alive>().basicHp -= slots[index].GetComponent<Pickable>().hp;
			gameObject.GetComponent<Alive>().strenght -= slots[index].GetComponent<Pickable>().strenght;
			gameObject.GetComponent<Alive>().agility -= slots[index].GetComponent<Pickable>().agility;
			gameObject.GetComponent<Alive>().spirit -= slots[index].GetComponent<Pickable>().spirit;
			gameObject.GetComponent<Alive>().intellect -= slots[index].GetComponent<Pickable>().intellect;
			gameObject.GetComponent<Alive>().UpdateStats();
		}
		GameObject throwOut = Instantiate(throwOutObject, new Vector3(transform.localPosition.x, transform.position.y-.5f, transform.localPosition.z + 2), Quaternion.identity);
		throwOut.GetComponent<Interactable>().pickables[0] = Instantiate(slots[index]) as GameObject;
		Destroy(slots[index]);
		slots[index] = null;
	}
}
