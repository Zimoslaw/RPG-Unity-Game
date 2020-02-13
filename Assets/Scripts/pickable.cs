using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable : MonoBehaviour
{
	public string type; //rodzaj przedmiotu
	public int rarity; //rzadkość
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
	public Sprite icon; //ikona w ekwipunku

	public GameObject parentObj; //przedmiot posiadający ten przedmiot
	public int indexInWindow;
}
