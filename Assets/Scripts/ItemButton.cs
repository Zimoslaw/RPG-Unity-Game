using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public byte type;
	public GameObject item;
	public GameObject frame;
	public Transform player;
	public Transform interactable;
	readonly Color32[] rarityColors = new Color32[] { new Color(0.7f, 0.7f, 0.7f), new Color(0.9f, 0.8f, 0f), new Color(1f, 0.4f, 0f), new Color(0.8f, 0f, 0.9f) };

	void Start()
    {
		RefreshIcon();

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ItemClicked()
	{
		if(item.GetComponent<Pickable>().parentObj.CompareTag("Interactable")) //gdy jest w obiekcie do interakcji
		{
			int j = 0;
			player.GetComponent<inventory>().AddItem(item, gameObject, interactable);

			for(int i = 0; i < transform.parent.childCount; i++) // zamknij, gdy był to ostatni przedmiot
			{
				if(!transform.parent.GetChild(i).gameObject.activeInHierarchy)
					j++;
			}
			if(j == 6)
			{
				interactable.GetComponent<Interactable>().RemoveSelf();
				gameObject.GetComponentInParent<GuiWindow>().Close();
			}
		}
		else if(item.GetComponent<Pickable>().parentObj.CompareTag("Player")) //gdy jest w ekwipunku
		{
				player.GetComponent<playerControl>().camTarget.GetComponent<GUI>().DisplayItemMenu(item.GetComponent<Pickable>().indexInWindow);
		}
	}

	public void RefreshIcon()
	{
		gameObject.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<Pickable>().icon;
		frame.GetComponent<UnityEngine.UI.Image>().color = rarityColors[item.GetComponent<Pickable>().rarity];
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		player.GetComponent<playerControl>().camTarget.GetComponent<GUI>().DisplayItemDescription(item);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		player.GetComponent<playerControl>().camTarget.GetComponent<GUI>().CloseItemDescription();
	}
}
