using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
	public Camera cam;
	public UnityEngine.UI.Image inventory; //okno ekwipunku
	public UnityEngine.UI.Image pick; //okno lootu
	public UnityEngine.UI.Slider progressBar; //pasek postępu
	public UnityEngine.UI.Text progressBarText; //text wyświetlany na pasku postępu
	public UnityEngine.UI.Image progressBarFill; //wypełnienie paska postępu
	public UnityEngine.UI.Image itemMenu; //okno menu przedmiotu w ekwipunku
	public UnityEngine.UI.Text infos; //text wiadomości dla gracza
	public UnityEngine.UI.Image description; //okienko opisu przedmiotu

	float duration = 1;
	float timer1 = 0;
	float timer2 = 0;

	bool isProgress = false;
	bool isProgressCancelled = false;
	bool isInfoDisplayed = false;

	void Start()
    {
		
    }
	
    void Update()
    {
		if(isProgress) //odliczanie postępu czynności
		{
			progressBar.value += Time.deltaTime / duration;
			if(progressBar.value >= 1 + Mathf.Epsilon)
			{
				progressBar.value = 1;
				if(!isProgressCancelled) //jeśli czynność nie została anulowana
					progressBarFill.color = new Color(0,0.7f,0); //zielony
				else
					progressBarFill.color = new Color(0.7f, 0, 0); //czerwony
				timer1 += Time.deltaTime;
				if(timer1 >= 0.2f + Mathf.Epsilon)
				{
					timer1 = 0;
					isProgress = false;
					isProgressCancelled = false;
					progressBar.gameObject.SetActive(false);
					progressBarFill.color = new Color(0, 0.5f, 0.9f);
				}
			}
		}

		if(isInfoDisplayed) //odliczanie czasu widoczności wiadomości
		{
			timer2 += Time.deltaTime;
			if(timer2 >= 2 + Mathf.Epsilon)
			{
				timer2 = 0;
				isInfoDisplayed = false;
				infos.gameObject.SetActive(false);
			}
		}
	}

	public void DisplayPick(GameObject[] pickables, Transform plyr, Collider clckdObj) //wyświetlenie okna z lootem
	{
		int i = 0;
		pick.GetComponent<LootPick>().Refresh();//usuń poprzednie obiekty z gui_pick

		foreach(GameObject p in pickables)
		{
			if(p != null)
			{
				pick.GetComponent<LootPick>().itemButtons[i].GetComponent<ItemButton>().item = p;
				pick.GetComponent<LootPick>().itemButtons[i].GetComponent<ItemButton>().interactable = clckdObj.transform;
				pick.GetComponent<LootPick>().itemButtons[i].GetComponent<ItemButton>().RefreshIcon();
				pick.GetComponent<LootPick>().itemButtons[i].SetActive(true);
				i++;
			}
		}
		if(i != 0) // jeśli był jakikolwiek przedmiot do wyświetlenia
		{
			pick.rectTransform.anchoredPosition = Input.mousePosition;//pozycja okna taka jak pozycja kursora
			pick.gameObject.SetActive(true);
		}
		else
		{
			clckdObj.enabled = false; // wyłącza interakcję z obiektem
			DisplayInfo("Brak przedmiotów do zebrania");
		}
	}

	public void DisplayProgressBar(float drtn, string text) //wyswietlenie paska postępu czynności
	{
		duration = drtn;
		progressBarText.text = text;
		progressBar.value = 0;
		progressBar.gameObject.SetActive(true);
		isProgress = true;
	}

	public void CancelProgress() //anulowanie czynności
	{
		pick.GetComponent<GuiWindow>().Close();
		if (isProgress)
		{
			progressBar.value = 1;
			progressBarText.text = "Anulowano";
			isProgressCancelled = true;
		}
	}

	public void DisplayItemMenu(int index)
	{
		itemMenu.GetComponent<ItemMenu>().itemIndex = index;
		if(index < 8) // jeśli przedmiot jest używany przez gracza
			itemMenu.GetComponent<ItemMenu>().useButton.interactable = false;
		else
			itemMenu.GetComponent<ItemMenu>().useButton.interactable = true;
		itemMenu.rectTransform.anchoredPosition = Input.mousePosition;//pozycja okna taka jak pozycja kursora
		itemMenu.gameObject.SetActive(true);
	}

	public void DisplayInventory(GameObject player)
	{
		if(!inventory.gameObject.activeInHierarchy)
		{
			int i = 0;
			GameObject[] items = player.GetComponent<inventory>().slots;
			foreach(GameObject x in items)
			{
				if(x != null)
				{
					inventory.GetComponent<InventoryWindow>().itemButtons[i].GetComponent<ItemButton>().item = x;
					inventory.GetComponent<InventoryWindow>().itemButtons[i].GetComponent<ItemButton>().RefreshIcon();
					inventory.GetComponent<InventoryWindow>().itemButtons[i].SetActive(true);
				}
				i++;
			}
			inventory.gameObject.SetActive(true);
		}
		else
		{
			inventory.gameObject.SetActive(false);
			itemMenu.gameObject.SetActive(false);
			CloseItemDescription();
		}
	}

	public void DisplayItemDescription(GameObject item)
	{
		description.GetComponent<ItemDescription>().UpdateItemInfo(item);
		description.GetComponent<UnityEngine.UI.Image>().rectTransform.anchoredPosition = Input.mousePosition + new Vector3(1, 1, 0);
		description.gameObject.SetActive(true);
	}

	public void CloseItemDescription()
	{
		description.gameObject.SetActive(false);
	}

	public void DisplayInfo(string s) //wyświetlenie wiadomości do gracza na ekran
	{
		infos.text = s;
		infos.gameObject.SetActive(true);
		isInfoDisplayed = true;
	}
}
