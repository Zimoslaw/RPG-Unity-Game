using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{

	public UnityEngine.UI.Text itemName;
	public UnityEngine.UI.Text type;
	public UnityEngine.UI.Text hpText;
	public UnityEngine.UI.Text strText;
	public UnityEngine.UI.Text sprtText;
	public UnityEngine.UI.Text agltText;
	public UnityEngine.UI.Text intText;
	public UnityEngine.UI.Text dmgText;
	public UnityEngine.UI.Text mdmgText;
	readonly Color32[] rarityColors = new Color32[] { new Color(0.7f, 0.7f, 0.7f), new Color(0.9f, 0.8f, 0f), new Color(1f, 0.4f, 0f), new Color(0.8f, 0f, 0.9f) }; //kolory rzadkości przedmiotu

	public void UpdateItemInfo(GameObject item)
	{
		int yPos = -34;
		gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta = new Vector2(256, 52);
		hpText.text = "";
		strText.text = "";
		sprtText.text = "";
		agltText.text = "";
		intText.text = "";
		dmgText.text = "";
		mdmgText.text = "";

		itemName.text = item.GetComponent<Pickable>().itemName;
		itemName.color = rarityColors[item.GetComponent<Pickable>().rarity];
		type.text = item.GetComponent<Pickable>().type;
		if(item.GetComponent<Pickable>().hp > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			hpText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			hpText.text = $"+{item.GetComponent<Pickable>().hp} Punktów życia";
		}

		if(item.GetComponent<Pickable>().strenght > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			strText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			strText.text = $"+{item.GetComponent<Pickable>().strenght} Siły";
		}

		if(item.GetComponent<Pickable>().spirit > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			sprtText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			sprtText.text = $"+{item.GetComponent<Pickable>().spirit} Duszy";
		}

		if(item.GetComponent<Pickable>().agility > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			agltText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			agltText.text = $"+{item.GetComponent<Pickable>().agility} Zręczności";
		}

		if(item.GetComponent<Pickable>().intellect > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			intText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			intText.text = $"+{item.GetComponent<Pickable>().intellect} Intelektu";
		}

		if(item.GetComponent<Pickable>().attackDamage > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			dmgText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			dmgText.text = "Obrażenia: " + item.GetComponent<Pickable>().attackDamage;
		}

		if(item.GetComponent<Pickable>().magicDamage > 0)
		{
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta += new Vector2(0, 16);
			yPos -= 16;
			mdmgText.rectTransform.anchoredPosition = new Vector2(10, yPos);
			mdmgText.text = "Obrażenia magiczne: " + item.GetComponent<Pickable>().attackDamage;
		}
	}

	// Update is called once per frame
	void Update()
    {
		if(EventSystem.current.IsPointerOverGameObject())
			gameObject.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = Input.mousePosition + new Vector3(1, 1, 0);
		else
			gameObject.SetActive(false);
	}
}
