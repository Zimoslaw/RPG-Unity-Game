using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guiWindow : MonoBehaviour
{
	public Transform player = null, clickedObj = null;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(player != null && clickedObj != null) //zamyka okno jeśli gracz oddali się za bardzo
		{
			if (Vector3.Distance(player.position, clickedObj.position) > 3)
			{
				Close();
			}
		}
    }

	public void RefreshPickItems() //usuwa poprzednie przedmioty z gui_pick
	{
		foreach (Transform child in transform)
		{
			if (child.GetComponent<pickable>() != null)
			{
				Destroy(child.gameObject);
			}
		}
	}

	void Close() //zamyka okno
	{
		gameObject.SetActive(false);
	}
}
