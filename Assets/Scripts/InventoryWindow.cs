using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : GuiWindow
{
	public UnityEngine.UI.Slider hpSlider;
	public UnityEngine.UI.Slider stamSlider;
	public UnityEngine.UI.Slider manaSlider;
	public UnityEngine.UI.Text hp; // liczba hp
	public UnityEngine.UI.Text stam; // liczba staminy
	public UnityEngine.UI.Text mana; // liczba many
	public UnityEngine.UI.Text str; // liczba siły
	public UnityEngine.UI.Text sprt; // liczba duchowości
	public UnityEngine.UI.Text agl; // liczba zręczności
	public UnityEngine.UI.Text intl; // liczba inteligencji
	public UnityEngine.UI.Text soc; // liczba retoryki
	public UnityEngine.UI.Text lrn; // liczba wiedzy
	public UnityEngine.UI.Text dmg; // procent zadawanych obrażeń
	public UnityEngine.UI.Text magDmg; // procent zadawanych magicznych obrażeń

	void Update()
    {
		hpSlider.value = player.GetComponent<Alive>().currentHp / (float)player.GetComponent<Alive>().hp;
		stamSlider.value = (float)player.GetComponent<Alive>().currentStamina / (float)player.GetComponent<Alive>().stamina;
		manaSlider.value = (float)player.GetComponent<Alive>().currentMana / (float)player.GetComponent<Alive>().mana;

		hp.text = Mathf.Floor(player.GetComponent<Alive>().currentHp)+" / "+ player.GetComponent<Alive>().hp;
		stam.text = player.GetComponent<Alive>().currentStamina + " / " + player.GetComponent<Alive>().stamina;
		mana.text = player.GetComponent<Alive>().currentMana + " / " + player.GetComponent<Alive>().mana;

		str.text = ""+player.GetComponent<Alive>().strenght;
		sprt.text = "" + player.GetComponent<Alive>().spirit;
		agl.text = "" + player.GetComponent<Alive>().agility;
		intl.text = "" + player.GetComponent<Alive>().intellect;
		soc.text = "" + player.GetComponent<Alive>().social;
		lrn.text = "" + player.GetComponent<Alive>().learning;

		dmg.text = "" + player.GetComponent<Alive>().attackDamage * 100 + "%";
		magDmg.text = "" + player.GetComponent<Alive>().magicDamage * 100 + "%";
	}
}
