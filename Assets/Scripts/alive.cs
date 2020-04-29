using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
	public string aliveName;

	public int hp = 1; //maksymalna żywotność (bazowa + mix siły i duszy)
	public int basicHp = 100; //bazowa żywotność
	public float currentHp = 1; //aktualna żywotność

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

	public bool canRegenerate = true;

	public bool hostile = false;

	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		if(canRegenerate && currentHp < hp)
		{
			currentHp += (currentStamina + strenght + spirit) * Time.deltaTime;
		}

		if(currentHp <= 0)
			Destroy(gameObject);
	}

	public void UpdateStats() {
		hp = (2 * strenght + 2 * spirit) + basicHp;
		stamina = (5 * strenght) + 10;
		gameObject.GetComponent<playerControl>().playerSpeed = (10 + (0.05f * agility)) * MoveSpeed;
		gameObject.GetComponent<playerControl>().globalCooldown = 2f - (agility * 0.01f);
		mana = (5 * spirit) + 10;
	}
}
