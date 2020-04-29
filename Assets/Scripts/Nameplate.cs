using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nameplate : MonoBehaviour
{
	public Transform cam;
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Image background;
	public UnityEngine.UI.Slider hpSlider;
	public UnityEngine.UI.Text hpSliderText;
	// Start is called before the first frame update
	void Start()
    {
		string aliveName = GetComponentInParent<Alive>().aliveName;
		nameText.text = aliveName;
		background.rectTransform.sizeDelta = new Vector2((aliveName.Length + 1) * 40, 100);

	}

    // Update is called once per frame
    void Update()
    {
		float currentHp = GetComponentInParent<Alive>().currentHp;
		int maxHp = GetComponentInParent<Alive>().hp;
		float  HPNormal = currentHp / maxHp;
		hpSliderText.text = (int)currentHp + "/" + maxHp;
		hpSlider.value = HPNormal;
		gameObject.transform.LookAt(cam);
    }
}
