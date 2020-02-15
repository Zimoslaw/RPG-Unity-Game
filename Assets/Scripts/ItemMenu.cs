using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : guiWindow
{
	public int itemIndex;

	public void ButtonClicked(int button) //wywołuje akcję gdy jest wybrana opcja z ItemMenu
	{
		if (button == 0) //użyj przedmiotu
		{
			player.GetComponent<inventory>().AddUsable(itemIndex);
			Close();
		}
		if (button == 1) //wyrzuć przedmiot
		{
			player.GetComponent<inventory>().ThrowOut(itemIndex);
			player.GetComponent<playerControl>().camTarget.GetComponent<gui>().inventory.GetComponent<InventoryWindow>().itemButtons[itemIndex].SetActive(false);
			Close();
		}
	}
}
