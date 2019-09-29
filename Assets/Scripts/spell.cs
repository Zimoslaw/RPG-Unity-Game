using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
	public float attackDamage = 0;
	public float magicDamage = 0;
	public float speed;
	float deathTimer;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.Translate(new Vector3(0,0,speed));
		deathTimer += Time.deltaTime;
		if(deathTimer >= 1) {
			Destroy(gameObject);
		}
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Alive")) {
			if(attackDamage!=0)
			collision.collider.GetComponent<alive>().currentHp -= (int)attackDamage;
			if(magicDamage!=0)
			collision.collider.GetComponent<alive>().currentHp -= (int)Mathf.Floor(magicDamage /  collision.collider.GetComponent<alive>().spirit);
		}
		Destroy(gameObject);
	}
}
