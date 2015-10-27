using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {

	public GameObject enemyHealthObject; // Game object responsible for displaying enemy health

	public GameObject missedObject; // Game object responsible for notifying the player that they missed their
									// attack (it will probably be later represented through animation)

	public GameObject button1;		// Game object representing the first button
	public GameObject button2;		// Game object representing the second button
	public GameObject button3;		// Game object representing the third button

	private int button1Num;	// the number displayed on the first button
	private int button2Num;	// the number displayed on the second button
	private int button3Num;	// the number displayed on the third button

	// holds the numbers of the buttons selected by the player. If none are selected, it remains at -1.
	private int action1 = -1;
	private int action2 = -1;
	private bool missed = false; // boolean for turning on and off the missedObject game object
	public int enemyHealth;	// the amount of health the enemy has
	private int randHealth; // this variable stores the passed in random number (depending on what enemy the player runs into)

	// Use this for initialization
	void Start () {
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
					victoryScreen();
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

	// method takes in enemy health and type of enemy. For example, if enemy is a slime, then 
	// the enemyType parameter would be a 1. Refer to the content of the method to see the various
	// enemy models
	public void setBattle(int initialHealth, int enemyType){
		enemyHealth = initialHealth;
		if (enemyType == 1) {
			// load the slime model in the position of the red rectangle
		} else if (enemyType == 2) {
			// load the bandit model in the position of the red rectangle
		} else if (enemyType == 3) {
			// load the troll model in the position of the red rectangle
		}
	}

	public void victoryScreen(){
		// enable the victory screen panel and alter text to fit statistics

	}
}
