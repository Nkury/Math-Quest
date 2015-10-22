using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {
	public GameObject enemyHealthObject;
	public GameObject missedObject;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;

	private int button1Num;
	private int button2Num;
	private int button3Num;
	private int action1 = -1;
	private int action2 = -1;
	private bool missed = false;
	public int enemyHealth;

	// Use this for initialization
	void Start () {
		enemyHealth = Random.Range (5, 10);
		enemyHealthObject.GetComponent<TextMesh>().text = enemyHealth.ToString ();
		assignButtons ();
	}
	
	// Update is called once per frame
	void Update () {
		enemyHealthObject.GetComponent<TextMesh> ().text = enemyHealth.ToString ();
		if (!missed)
			missedObject.GetComponent<Renderer> ().enabled = false;
		else
			missedObject.GetComponent<Renderer> ().enabled = true;
	}

	public void buttonClicked(int buttonNo){
		if (action1 == -1 || action2 == -1) {
			if (buttonNo == 1) {
				button1.SetActive (false);
				if (action1 == -1)
					action1 = button1Num;
				else if(action2 == -1)
					action2 = button1Num;
			} else if (buttonNo == 2) {
				button2.SetActive (false);
				if (action1 == -1)
					action1 = button2Num;
				else if (action2 == -1)
					action2 = button2Num;
			} else if (buttonNo == 3) {
				button3.SetActive (false);
				if (action1 == -1)
					action1 = button3Num;
				else if (action2 == -1)
					action2 = button3Num;
			}
		}

		if (action1 != -1 && action2 != -1 && buttonNo == 4) {
			if (action1 + action2 > enemyHealth) {
				StartCoroutine(attackMissed());
				button1.SetActive (true);
				button2.SetActive (true);
				button3.SetActive (true);
				assignButtons();
			} else {
				enemyHealth -= action1 + action2;
				if (enemyHealth > 0) {
					button1.SetActive (true);
					button2.SetActive (true);
					button3.SetActive (true);
					assignButtons ();
				} else{
					enemyHealth = 0;
					missedObject.GetComponent<TextMesh>().text = "Winner!";
					StartCoroutine(attackMissed());
				}
			}
			action1 = -1;
			action2 = -1;
		}
	}


		void assignButtons(){

		int attack1 = Random.Range (1, enemyHealth+1);
		int attack2 = enemyHealth - attack1;
		int attack3 = attack1;
		while(attack3 == attack1 || attack3 == attack2)
			attack3 = Random.Range (1, enemyHealth+2);
		int choice = Random.Range (1, 4);
		if (choice == 1) {
			button1.GetComponentInChildren <Text> ().text = attack1.ToString ();
			button1Num = attack1;
			choice = Random.Range (1, 3);
			if (choice == 1) {
				button2Num = attack2;
				button3Num = attack3;
				button2.GetComponentInChildren <Text> ().text = attack2.ToString ();
				button3.GetComponentInChildren <Text> ().text = attack3.ToString ();
			} else {
				button2Num = attack3;
				button3Num = attack2;
				button2.GetComponentInChildren <Text> ().text = attack3.ToString ();
				button3.GetComponentInChildren <Text> ().text = attack2.ToString ();
			}
		} else if (choice == 2) {
			button2.GetComponentInChildren <Text> ().text = attack1.ToString ();
			button2Num = attack1;
			choice = Random.Range (1, 3);
			if (choice == 1) {
				button1Num = attack2;
				button3Num = attack3;
				button1.GetComponentInChildren <Text> ().text = attack2.ToString ();
				button3.GetComponentInChildren <Text> ().text = attack3.ToString ();
			} else {
				button1Num = attack3;
				button3Num = attack2;
				button1.GetComponentInChildren <Text> ().text = attack3.ToString ();
				button3.GetComponentInChildren <Text> ().text = attack2.ToString ();
			}
		} else {
			button3.GetComponentInChildren <Text> ().text = attack1.ToString ();
			button3Num = attack1;
			choice = Random.Range (1, 3);
			if (choice == 1) {
				button1Num = attack2;
				button2Num = attack3;
				button1.GetComponentInChildren <Text> ().text = attack2.ToString ();
				button2.GetComponentInChildren <Text> ().text = attack3.ToString ();
			} else {
				button1Num = attack3;
				button2Num = attack2;
				button1.GetComponentInChildren <Text> ().text = attack3.ToString ();
				button2.GetComponentInChildren <Text> ().text = attack2.ToString ();
			}
		}
	}

	IEnumerator attackMissed(){
		missed = true;
		yield return new WaitForSeconds(2);
		missed = false;
	}
}
