using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour
{
	public int hp = 1; //makszymalna żywotność (bazowa + mix siły i duszy)
	public int basicHp = 100; //bazowa żywotność
	public int currentHp = 1; //aktualna żywotność

	public int strenght = 1; //siła
	public int agility = 1; //zręczność
	public int intellect = 1; //inteligencja
	public int spirit = 1; //dusza

	public int stamina = 1; //punkty wytrzymałości (maks.)
	public int currentStamina = 1; //aktualna stamina
	public float attackDamage = 1f; //obrażenia ataku niemagicznego %
	public int mana = 1; //punkty many (maks.)
	public int currentMana = 1; //aktualna mana
	public float magicDamage = 1f; //obrażenia ataku magicznego %

	public float MoveSpeed = 1f; //prędkość poruszania się gracza %
	public float globalCooldown = 1f; //cooldown jakiejkolwiek umiejętności %
	public int social = 1; //umiejętnośći społeczne
	public int learning = 1; //nauka

	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UpdateStats() {
		hp = (2 * strenght + 2 * spirit) + basicHp;
		stamina = (5 * strenght) + 10;
		gameObject.GetComponent<playerControl>().playerMaxSpeed = 256 + (2 * agility);
		gameObject.GetComponent<playerControl>().globalCooldown = 2.5f - (agility * 0.01f);
		mana = (5 * spirit) + 10;
	}
}
