using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guiWindow : MonoBehaviour
{
	public bool isGuiPick = false;
	public int itemIndex; //potrzebne przy ItemMenu
	public Transform player = null, clickedObj = null; //potrzebne przy ItemMenu i GuiPick
	public GameObject[] itemButtons; //potrzebne przy GuiPick

	void Update()
    {
		if(isGuiPick)
		{
			if(player != null && clickedObj != null) //zamyka okno jeśli gracz oddali się za bardzo
			{
				if(Vector3.Distance(player.position, clickedObj.position) > 3)
				{
					Close();
				}
			}
		}
    }

	public void RefreshPickItems() //usuwa poprzednie przedmioty z gui_pick
	{
		foreach (GameObject b in itemButtons)
		{
			b.SetActive(false);
		}
	}

	public void ItemMenuButtonClicked(int button) //wywołuje akcję gdy jest wybrana opcja z ItemMenu
	{
		if(button == 0) //użyj przedmiotu
		{
			player.GetComponent<inventory>().AddUsable(itemIndex);
			Close();
		}
		if(button == 1) //wyrzyć przedmiot
		{
			player.GetComponent<inventory>().ThrowOut(itemIndex);
			player.GetComponent<playerControl>().camTarget.GetComponent<gui>().inventory.GetComponent<guiWindow>().itemButtons[itemIndex].SetActive(false);
			Close();
		}
	}

	public void Close() //zamyka okno
	{
		gameObject.SetActive(false);
	}
}
