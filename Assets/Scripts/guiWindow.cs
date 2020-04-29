using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiWindow : MonoBehaviour
{
	public Transform player = null; // gracz widzący GUI
	public GameObject[] itemButtons;

	public void Close() //zamyka okno
	{
		gameObject.SetActive(false);
	}
}
