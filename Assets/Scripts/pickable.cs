using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable : MonoBehaviour
{
	public string type; //rodzaj przedmiotu
	public int rarity; //rzadkość
	Color32[] rarityColors = new Color32[] {new Color(0.5f,0.5f,0.5f), new Color(0.9f, 0.8f, 0f), new Color(1f, 0.4f, 0f), new Color(0.8f, 0f, 0.9f)};
	public int hp;
	public int strenght;
	public int agility;
	public int spirit;
	public int intellect;
	public int attackDamage;
	public int magicDamage;
	public float range; //zasięg broni białej
	public int angle; //szerokość działania broni białej
	public float speed; //prędkość pocisku
	public GameObject apperance; //model obiektu
	public GameObject projectile; //wizualna akcja
	public Texture2D icon; //ikona w ekwipunku

	// Start is called before the first frame update
	void Start() {
		gameObject.GetComponentsInChildren<UnityEngine.UI.Image>()[1].color = rarityColors[rarity];
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
