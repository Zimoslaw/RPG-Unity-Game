﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
	public byte type;
	public GameObject item;
	public GameObject frame;
	public Transform player;
	public Transform interactable;
	readonly Color32[] rarityColors = new Color32[] { new Color(0.9f, 0.9f, 0.9f), new Color(0.9f, 0.8f, 0f), new Color(1f, 0.4f, 0f), new Color(0.8f, 0f, 0.9f) };

	void Start()
    {
		gameObject.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<pickable>().icon;
		frame.GetComponent<UnityEngine.UI.Image>().color = rarityColors[item.GetComponent<pickable>().rarity];

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ItemClicked()
	{
		if(item.GetComponent<pickable>().parentObj.CompareTag("Interactable")) //gdy jest w obiekcie do interakcji
		{
			int j = 0;
			player.GetComponent<inventory>().AddItem(item, gameObject, interactable);
			for(int i = 0; i < transform.parent.childCount; i++)
			{
				if(!transform.parent.GetChild(i).gameObject.activeInHierarchy)
					j++;
			}
			if(j == 6)
				gameObject.GetComponentInParent<guiWindow>().Close();
		}
		else if(item.GetComponent<pickable>().parentObj.CompareTag("Player")) //gdy jest w ekwipunku
		{
			player.GetComponent<playerControl>().camTarget.GetComponent<gui>().DisplayItemMenu(item.GetComponent<pickable>().indexInWindow);
		}
	}
}
