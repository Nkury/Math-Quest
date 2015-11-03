using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {
    
	public GameObject enemyHealthObject; // Game object responsible for displaying enemy health
   
	public GameObject missedObject; // Game object responsible for notifying the player that they missed their
									// attack (it will probably be later represented through animation)

    public GameObject slimeModel;   // Game object representing the slime prefab
    public GameObject banditModel;  // Game object representing the bandit prefab
    public GameObject trollModel;   // Game object representing the troll prefab
    public GameObject playerModel;
    public GameObject player;

    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public GameObject A1_Button;
    public GameObject A2_Button;
    public GameObject A3_Button;
    public GameObject Attack_Button;

	// holds the numbers of the buttons selected by the player. If none are selected, it remains at -1.
	private bool missed = false; // boolean for turning on and off the missedObject game object
	public int enemyHealth;	// the amount of health the enemy has
	private int randHealth; // this variable stores the passed in random number (depending on what enemy the player runs into)
    public int playerHealth; // this variable stores the number of hearts the player has
    private int EnemyType;

    // variables used for flickering of player sprite
    private bool damageTaken = false;
    private bool playerAttack = false;
    private bool spacePress = false;
    private int interval = 0;



	// Use this for initialization
	void Start () {
        playerHealth = 3;
        player = GameObject.Find("Cha_Knight");
        playerHealthBar.GetComponent<HealthBarManager>().InitHealthBar(playerHealth);
        enemyHealth = PlayerPrefs.GetInt("enemy_health");
        int monsterType = PlayerPrefs.GetInt("monster_type");
        setBattle(enemyHealth, monsterType);
        GameObject battleGUI = GameObject.Find("BattleGUI");
        battleGUI.GetComponent<ButtonManager>().AttackButtonClickedEvent += setButtons;
        assignButtons();
	}
	
	// Update is called once per frame
	void Update () {
        interval++;

		enemyHealthObject.GetComponent<TextMesh> ().text = enemyHealth.ToString ();
		if (!missed)
			missedObject.GetComponent<Renderer> ().enabled = false;
		else
			missedObject.GetComponent<Renderer> ().enabled = true;

        if (damageTaken)
        {
            if (interval % 5 == 0)
                playerModel.GetComponent<SkinnedMeshRenderer>().enabled = !playerModel.GetComponent<SkinnedMeshRenderer>().enabled;
        }
        else if(!playerAttack && !damageTaken)
        {
            playerModel.GetComponent<SkinnedMeshRenderer>().enabled = true;
            player.GetComponent<Animation>().Play("Wait");
        }

        // victory conditions. Since I haven't gotten the buttons to work out, just press P to win and test it out.
        if (Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Space) && spacePress))
        {
            GameObject zone1storage = GameObject.Find("PersistentZone1");
            zone1storage.GetComponent<Zone1Start>().destroyList.Add(PlayerPrefs.GetString("monster_name"));            
            Application.LoadLevel("nizar Zone #1");
        }

        if (playerHealth <= 0)
            StartCoroutine(gameOverScreen());
	}

    public void assignButtons()
    {   
        // first attack is a random number between 1 and enemy health - 1 (since max is exclusive)
        int attack1 = Random.Range(1, enemyHealth);
        // second attack is the difference of the enemy health and attack1
        int attack2 = enemyHealth - attack1;
        // third attack is a random number between 1 and enemyHealth + 1 and makes sure it's not the same as the other
        // numbers (I set it to attack1 so that way we can start the following while loop)
        int attack3 = attack1;

        // keep finding a number for attack3 that is not the same as either attack1 or 2 and its sum with either 
        // attack1 or 2 will not lead to the enemy having 1 health.
        while ((attack3 == attack1 || attack3 == attack2) || ((attack1 + attack3 == (enemyHealth-1)) || 
                (attack2 + attack3 == (enemyHealth-1))))
                    attack3 = Random.Range(1, enemyHealth + 2);

        int[] attacks = {attack1, attack2, attack3};
        GameObject battleGUI = GameObject.Find("BattleGUI");
        battleGUI.GetComponent<ButtonManager>().InitActionButtons(attacks);
        }

        public void setButtons(int[] values)
        {
            // attack misses and player suffers one heart damage
            if (values[0] + values[1] > enemyHealth) {
				StartCoroutine(attackMissed());               
                StartCoroutine(AttackAndGotHit(true, 0, 0));
			} else {
				enemyHealth -= values[0] + values[1];
                
				if (enemyHealth > 0) {
					assignButtons ();
                    StartCoroutine(AttackAndGotHit(false, values[0], values[1]));
				} else{
					enemyHealth = 0;
                    StartCoroutine(AttackAndKill());
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
        EnemyType = enemyType;
        // SLIME
		if (enemyType == 1) {
            Instantiate(slimeModel, new Vector3(111.51f, -.2f, 29.43f), new Quaternion());
		} 
        // BANDIT
        else if (enemyType == 2) {
            Instantiate(banditModel, new Vector3(109.51f, -.2f, 26.36f), banditModel.transform.rotation);
		} 
        // TROLL
        else if (enemyType == 3) {
            Instantiate(trollModel, new Vector3(110.1894f, 0, 27.26139f), trollModel.transform.rotation);
		}

        enemyHealthBar.GetComponent<HealthBarManager>().InitHealthBar(enemyHealth);
	}

    IEnumerator gameOverScreen()
    {
        // enable game over screen in future
        GameObject zone1storage = GameObject.Find("PersistentZone1");
        // have all the enemies respawn
        zone1storage.GetComponent<Zone1Start>().destroyList.Clear();
        Destroy(zone1storage);
        // start the player back at the starting position
        PlayerPrefs.SetFloat("player_x", 15.3f);
        PlayerPrefs.SetFloat("player_y", 10.35779f);
        PlayerPrefs.SetFloat("player_z", 32.2f);
        missedObject.GetComponent<TextMesh>().text = "Game Over";
        StartCoroutine(attackMissed());
        yield return new WaitForSeconds(3);
        Application.LoadLevel("nizar Zone #1");
    }

    IEnumerator AttackAndGotHit(bool missed, int attack1, int attack2)
    {
        GameObject mainCamera = GameObject.Find("Main Camera");
        AudioSource[] audios = mainCamera.GetComponents<AudioSource>();
        GameObject enemy;
        A1_Button.SetActive(false);
        A2_Button.SetActive(false);
        A3_Button.SetActive(false);
        Attack_Button.SetActive(false);
        playerAttack = true;
        player.GetComponent<Animation>().Play("Attack");
        yield return new WaitForSeconds(.5f);
        playerAttack = false;
        if (EnemyType == 1 && !missed)
        {
            enemy = GameObject.Find("Cha_Slime battle (Clone)");
            enemy.GetComponent<Animation>().Play("Damage");
        }
    
        if (!missed) {
            audios[1].Play();
            audios[2].volume = .15f;
            enemyHealthBar.GetComponent<HealthBarManager>().InitHealthBar(enemyHealth);
        }

        yield return new WaitForSeconds(.5f);
        audios[2].volume = .97f;
        yield return new WaitForSeconds(1);

        if (EnemyType == 1)
        {
            enemy = GameObject.Find("Cha_Slime battle (Clone)");
            enemy.GetComponent<Animation>().Play("Attack");
        }
        else if (EnemyType == 2)
        {
            enemy = GameObject.Find("Battle jack(Clone)");
            enemy.GetComponent<Animation>().Play("Lumbering");
        }
        else if (EnemyType == 3)
        {
            // play animation for troll if we make one
        }
        yield return new WaitForSeconds(.5f);
        player.GetComponent<Animation>().Play("Damage");
       
        audios[0].Play(); // OUCH sound effect
        audios[1].Play(); // damage sound effect
        audios[2].volume = .17f;
        playerHealth--;
        playerHealthBar.GetComponent<HealthBarManager>().InitHealthBar(playerHealth);
        //yield return new WaitForSeconds(.5f);
        if (EnemyType == 1)
        {
            enemy = GameObject.Find("Cha_Slime battle (Clone)");
            enemy.GetComponent<Animation>().Play("Wait");
        }
        else if (EnemyType == 2)
        {
            enemy = GameObject.Find("Battle jack(Clone)");
            enemy.GetComponent<Animation>().Play("Idle");
        }
        else if (EnemyType == 3)
        {
            // play animation for troll if we make one
        }
        yield return new WaitForSeconds(.5f);
        audios[2].volume = .95f;
        if (playerHealth == 1)
            audios[2].pitch = 1.5f;
        damageTaken = true;
        yield return new WaitForSeconds(3);
        damageTaken = false;
        A1_Button.SetActive(true);
        A2_Button.SetActive(true);
        A3_Button.SetActive(true);
        Attack_Button.SetActive(true);
    }

    IEnumerator AttackAndKill()
    {
        GameObject mainCamera = GameObject.Find("Main Camera");
        AudioSource[] audios = mainCamera.GetComponents<AudioSource>();
        GameObject enemy;
        A1_Button.SetActive(false);
        A2_Button.SetActive(false);
        A3_Button.SetActive(false);
        Attack_Button.SetActive(false);
        playerAttack = true;
        player.GetComponent<Animation>().Play("Attack");
        yield return new WaitForSeconds(.5f);
        playerAttack = false;
        if (EnemyType == 1)
        {
            enemy = GameObject.Find("Cha_Slime battle (Clone)");
            enemy.GetComponent<Animation>().Play("Dead");
        }

        audios[1].Play();
        audios[2].volume = 0;
        audios[3].Play();
        enemyHealthBar.GetComponent<HealthBarManager>().InitHealthBar(0);
        yield return new WaitForSeconds(2);
        if (EnemyType == 1 && !missed)
        {
            enemy = GameObject.Find("Cha_Slime battle (Clone)");
            Destroy(enemy);
        }
        else if (EnemyType == 2)
        {
            enemy = GameObject.Find("Battle jack(Clone)");
            Destroy(enemy);
        }
        yield return new WaitForSeconds(2);
        spacePress = true;
        if(playerHealth == 3)
            missedObject.GetComponent<TextMesh>().text = "Press Space to Continue!\nRating: ***";
        else if(playerHealth == 2)
            missedObject.GetComponent<TextMesh>().text = "Press Space to Continue!\nRating: **";
        else if (playerHealth == 1)
            missedObject.GetComponent<TextMesh>().text = "Press Space to Continue!\nRating: *";
        missed = true;
        
    }
}
