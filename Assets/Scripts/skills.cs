using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills : MonoBehaviour {

	public void CastMelee(GameObject character, float range, int angle, GameObject visual, int aDmg, int mDmg)
	{
		Collider[] colliders = Physics.OverlapSphere(character.transform.position, range);
		foreach(Collider x in colliders)
		{
			if(x.CompareTag("Alive"))
			{
				if(Vector3.Angle(character.transform.forward, x.transform.position) < angle)
				{ //jeśli kąt pomiędzy obiektem a graczem jest mniejszy niż kąt działania broni
					if(aDmg != 0)
						x.GetComponent<Alive>().currentHp -= (int)(aDmg + Mathf.Floor(Random.Range(-0.2f * aDmg, 0.2f * aDmg))); //odjęcie żywotności obiektowi (obrażenia zwykłe)
					if(mDmg != 0)
						x.GetComponent<Alive>().currentHp -= (int)Mathf.Floor(mDmg + Mathf.Floor(Random.Range(-0.5f * mDmg, 0.5f * mDmg)) / x.GetComponent<Alive>().spirit); //odjęcie żywotności obiektowi (obrażenia magiczne)
				}
			}
		}
		Instantiate(visual, character.transform.position, character.transform.rotation);
	}

	public void CastRanged(GameObject character, float speed, GameObject spell, int aDmg, int mDmg)
	{
		GameObject spellObject = Instantiate(spell, character.transform.position + (character.transform.forward * 2), character.transform.rotation);
		spellObject.GetComponent<Spell>().speed = speed;
		spellObject.GetComponent<Spell>().attackDamage = aDmg + Mathf.Floor(Random.Range(-0.2f * aDmg, 0.2f * aDmg));
		spellObject.GetComponent<Spell>().magicDamage = mDmg + Mathf.Floor(Random.Range(-0.5f * mDmg, 0.5f * mDmg));
	}
}
