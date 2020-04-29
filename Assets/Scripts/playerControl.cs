using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class playerControl : MonoBehaviour {

    public float playerSpeed = 10; //prędkość ruszania się z miejsca
    //public float playerMaxSpeed = 256f; //maksymalna prędkość ruchu gracza
	public float flipCooldown = 3f; //cooldown przewrotu
    float flipTimer = 0; //licznik czasu od ostatniego przewrotu
	public float globalCooldown = 2.5f; //co ile można użyć umiejętności
	float cooldownTimer = 0;
	public int attackDamage = 0; //obrażenia fizyczne pobrane z ekwipunku + ze statystyk
	public int magicDamage = 0; //obrażenia magiczne pobrane z ekwipunku + ze statystyk
	public float range = 0; //zasięg broni białej
	public int angle = 0; //szerokość działania broni białej
	public float speed = 0; //prędkość pocisku czaru

    public Rigidbody self; //fizyka gracza
    public GameObject camTarget; //punkt odniesienia kamery
	public GameObject weapon; //wizualna akcja broni pobrana z ekwipunku

	public string weaponType; //typ broni z ekwipunku

    public Vector3 guide; //punkt pomocniczy

    void Start() {
		
	}

    void FixedUpdate() {
		
		if(camTarget.GetComponent<camControl>().isControlledByPlayer)
		{
			if (Input.GetKey(KeyCode.W))
			{
				ForwardMove(); //ruch do przodu
				guide = transform.position;
				camTarget.GetComponent<camControl>().CancelInteraProgress(); //anulowanie wykonywanej akcji
			} else
			{
				if (Input.GetKey(KeyCode.S))
				{
					BackwardsMove(); //ruch do tyłu
					guide = transform.position;
					camTarget.GetComponent<camControl>().CancelInteraProgress(); //anulowanie wykonywanej akcji
				} else
				{
					if (Input.GetKeyDown(KeyCode.D))
					{ //przewrót w prawo
						if (flipTimer >= flipCooldown + Mathf.Epsilon)
						{ //jeśli minęły 3 sekundy od ostatniego przewrotu
							FlipRight();
							flipTimer = 0;
							camTarget.GetComponent<camControl>().CancelInteraProgress(); //anulowanie wykonywanej akcji
						}
					}
					if (Input.GetKeyDown(KeyCode.A))
					{ //przewrót w lewo
						if (flipTimer >= flipCooldown + Mathf.Epsilon)
						{ //jeśli minęły 3 sekundy od ostatniego przewrotu
							FlipLeft();
							flipTimer = 0;
							camTarget.GetComponent<camControl>().CancelInteraProgress(); //anulowanie wykonywanej akcji
						}
					}
				}
			}
			if (flipTimer <= 0.5f + Mathf.Epsilon) {
				transform.position = new Vector3(Mathf.Lerp(transform.position.x, guide.x, Time.deltaTime + 0.1f), Mathf.Lerp(transform.position.y, transform.position.y, 1), Mathf.Lerp(transform.position.z, guide.z, Time.deltaTime + 0.1f)); //gracz podąża za punktem pomocniczym
			}
			flipTimer += Time.deltaTime;

			if(Input.GetKeyDown(KeyCode.I))
			{
				camTarget.GetComponent<GUI>().DisplayInventory(gameObject);
			}

			if(cooldownTimer >= globalCooldown + Mathf.Epsilon) {
				if(!camTarget.GetComponent<camControl>().lookingAt.CompareTag("Interactable") && camTarget.GetComponent<camControl>().lookingAt.CompareTag("Alive") && !EventSystem.current.IsPointerOverGameObject()) {
					if (Input.GetMouseButtonDown(0)) { //przycisk głównego ataku (broń w ręce)
						if(gameObject.GetComponent<inventory>().slots[0] != null)
						{
							weaponType = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().type;
							attackDamage = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().attackDamage + (gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().attackDamage * (int)Mathf.Floor(gameObject.GetComponent<Alive>().strenght * 0.01f * GetComponent<Alive>().attackDamage));
							magicDamage = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().magicDamage + (gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().magicDamage * (int)Mathf.Floor(gameObject.GetComponent<Alive>().spirit * 0.01f * GetComponent<Alive>().magicDamage));

							if(weaponType == "ranged weapon")
							{ //jeśli broń jest dystansowa
								speed = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().speed; //prędkość pocisku
								weapon = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().projectile; //pocisk
																															//gameObject.GetComponent<Skills.Ranged>().Cast(gameObject, speed, weapon, attackDamage, magicDamage); //gracz, pocisk, obrażenia
								gameObject.GetComponent<Skills>().CastRanged(gameObject, speed, weapon, attackDamage, magicDamage);
								cooldownTimer = 0;
							}
							if(weaponType == "melee weapon")
							{
								range = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().range;
								angle = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().angle;
								weapon = gameObject.GetComponent<inventory>().slots[0].GetComponent<Pickable>().projectile; //wizualna akcja
								gameObject.GetComponent<Skills>().CastMelee(gameObject, range, angle, weapon, attackDamage, magicDamage); //gracz, zasięg, obrażenia
								cooldownTimer = 0;
							}
						}
						else
						{
							camTarget.GetComponent<GUI>().DisplayInfo("Nie masz żadnej broni");
						}
					}
				}
			}
			cooldownTimer += Time.deltaTime;
		}
	}

    void ForwardMove() {
			gameObject.transform.Translate(new Vector3(0,0, 0.01f) * playerSpeed); //ruch do przodu
    }

    void BackwardsMove() {
			gameObject.transform.Translate(new Vector3(0,0, -0.01f) * playerSpeed * 0.5f); //ruch do tyłu
    }

    void FlipRight() {
        guide = transform.position + (transform.right * 3); //punkt pomocniczy znajduje się 3 jednostki w prawo
    }

    void FlipLeft() {
        guide = transform.position + (transform.right * -3); //punkt pomocniczy znajduje się 3 jednostki w lewo
    }
}
