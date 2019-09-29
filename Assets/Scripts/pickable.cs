using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable : MonoBehaviour
{
	public string type; //rodzaj przedmiotu
	public int hp;
	public int strenght;
	public int agility;
	public int spirit;
	public int intellect;
	public int attackDamage;
	public int magicDamage;
	public float range; //zasięg broni białej
	public float speed; //prędkość pocisku
	public GameObject apperance; //model obiektu
	public GameObject projectile; //wizualna akcja
	public Texture2D icon; //ikona w ekwipunku

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
