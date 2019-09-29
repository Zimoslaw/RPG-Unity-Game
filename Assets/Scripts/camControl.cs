using UnityEngine;
using System.Collections;

public class camControl : MonoBehaviour {

	public bool isControlledByPlayer = true; //przełącznik sterowania kamerą przez gracza lub przez grę

	float yRotation = 0; //ruch myszy w osi poziomej, wykorzystywany w trybie obrotu kamery
	public float cameraSensitivity = 5f; //czułość obracania przy trybie obrotu kamery
	public float minCameraDistance = 5f; //maksymalna odległość na jaką można oddalić widok
	public float maxCameraDistance = 30f; //minimalna odległość na jaką można przybliżyć widok
	public float cameraSpeed = 10f; //prędkość podążania kamery za graczem
	float scroll = 0; //w którą stronę scroll jest kręcony
	public float playerRotSpeed = 5f; //prędkość obrotu gracza

	public Vector3 offset; //odległość przewodnika od targetu

	public Transform player; //transform gracza
	public Transform playerGuide; //celownik

	public GameObject guide; //przewodnik kamery
	public GameObject cam; //kamera gracza

	Ray mouseRay; //promien wychodzący z kursora myszy
	RaycastHit mouseHit; //gdzie myszka wskazuje (na scenie)

	void Start() {
		offset = guide.transform.position - transform.position; //obliczenie odległości przewodnika od targetu
	}

	void FixedUpdate() {
		if (isControlledByPlayer) {
			//--------------bierzące wejścia-------------------
			yRotation = Input.GetAxis("Mouse X"); //do trybu obracania
			scroll = Input.GetAxis("Mouse ScrollWheel"); //do oddalania kamery

			//--------------wszystkie tryby--------------------
			transform.position = player.position; //pozycja celownika taka sama jak gracza
			playerGuide.position = player.position; //pozycja przewodnika gracza taka sama jak gracza
			cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, guide.transform.position.x, cameraSpeed * Time.deltaTime), Mathf.Lerp(cam.transform.position.y, guide.transform.position.y, cameraSpeed * Time.deltaTime), Mathf.Lerp(cam.transform.position.z, guide.transform.position.z, cameraSpeed * Time.deltaTime)); //kamera podąża za przewodnikiem
			cam.transform.LookAt(transform);


			if (Input.GetMouseButton(1)) {
				RotationMode();
			} else {
				PlayerRotating();
				Cursor.visible = true;
			}

		} else {
			cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, guide.transform.position.x, cameraSpeed * Time.deltaTime), Mathf.Lerp(cam.transform.position.y, guide.transform.position.y, cameraSpeed * Time.deltaTime), Mathf.Lerp(cam.transform.position.z, guide.transform.position.z, cameraSpeed * Time.deltaTime)); //kamera podąża za przewodnikiem
			Cursor.visible = false;
		}
	}

	//--------------obracanie gracza-----------------
	void PlayerRotating() {
		mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); //wysłanie promienia
		player.rotation = Quaternion.Lerp(player.rotation, playerGuide.rotation, playerRotSpeed * Time.deltaTime); //płynne obracanie gracza w stronę, w którą celuje celownik
		if (Physics.Raycast(mouseRay, out mouseHit)) { //gdzie w scenie padł promień
			playerGuide.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(mouseHit.point.x - playerGuide.position.x, mouseHit.point.z - playerGuide.position.z) * Mathf.Rad2Deg, 0)); //obracanie celownika w stronę punktu uderzenia promienia
		}
	}

	//--------------tryb obrotu kamery-----------------
	void RotationMode() {
		guide.transform.RotateAround(transform.position, Vector3.up, yRotation * cameraSensitivity); //obracaj przewodnik wokół targetu

		if (scroll < 0) {
			if (Vector3.Distance(transform.position, cam.transform.position) < maxCameraDistance) { //jeżeli kamera nie osiągneła maksymalnego dystansu
				offset = guide.transform.position - transform.position; //zaktualizowanie odległości przewodnika od targetu
				guide.transform.position += offset * 0.1f; //oddalenie przewodnika od targetu

			}
		}
		if (scroll > 0) {
			if (Vector3.Distance(transform.position, cam.transform.position) > minCameraDistance) { //jeżeli kamera nie osiągneła minimalnego dystansu
				offset = guide.transform.position - transform.position; //zaktualizowanie odległości przewodnika od targetu
				guide.transform.position -= offset * 0.1f; //przybliżenie przewodnika do tagetu

			}
		}

		Cursor.visible = false;
	}
}
