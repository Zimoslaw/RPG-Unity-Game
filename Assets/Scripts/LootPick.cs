using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPick : GuiWindow
{
	public Transform clickedObject = null;

	public void Refresh() //usuwa poprzednie przedmioty z gui_pick
	{
		foreach (GameObject o in itemButtons) {
			o.SetActive(false);
		}
	}

	void Update() {
		if (player != null && clickedObject != null) //zamyka okno jeśli gracz oddali się za bardzo
		{
			if (Vector3.Distance(player.position, clickedObject.position) > 3) {
				Close();
				player.GetComponent<playerControl>().camTarget.GetComponent<GUI>().CloseItemDescription();
			}
		}
	}
}
