using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gui : MonoBehaviour
{
	public Camera cam;
	public UnityEngine.UI.Image pick;
	public UnityEngine.UI.Slider progressBar;
	public UnityEngine.UI.Text progressBarText;
	public UnityEngine.UI.Image progressBarFill;

	float duration = 1;
	float timer1 = 0;

	bool isProgress = false;

	void Start()
    {
		
    }
	
    void Update()
    {
		if(isProgress)
		{
			progressBar.value += Time.deltaTime / duration;
			if(progressBar.value >= 1 + Mathf.Epsilon)
			{
				progressBar.value = 1;
				progressBarFill.color = new Color(0,0.7f,0);
				timer1 += Time.deltaTime;
				if(timer1 >= 0.2f + Mathf.Epsilon)
				{
					timer1 = 0;
					isProgress = false;
					progressBar.gameObject.SetActive(false);
					progressBarFill.color = new Color(0, 0.5f, 0.9f);
				}
			}
		}
	}

	public void DisplayPick(GameObject[] pickables, Transform plyr, Transform clckdObj)
	{
		pick.GetComponent<guiWindow>().player = plyr;
		pick.GetComponent<guiWindow>().clickedObj= clckdObj;
		pick.GetComponent<guiWindow>().RefreshPickItems();//usuń poprzednie obiekty z gui_pick
		pick.rectTransform.anchoredPosition = Input.mousePosition;//pozycja okna taka jak pozycja kursora
		int x = 7, y = -25;
		foreach(GameObject p in pickables)
		{
			GameObject clone = Instantiate(p, pick.rectTransform);
			clone.transform.localPosition = new Vector3(x,y,0);
			if(x%73==0)//jeśli cały rząd jest zapełniony
			{
				y -= 66;//przejdź do następnego rzędu
				x = 7;
			}
			else
			{
				x += 66;
			}
		}
		pick.gameObject.SetActive(true);
	}

	public void DisplayProgressBar(float drtn, string text)
	{
		duration = drtn;
		progressBarText.text = text;
		progressBar.value = 0;
		progressBar.gameObject.SetActive(true);
		isProgress = true;
	}
}
