using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone1Start : MonoBehaviour {

    // stores the names of the game objects that will be destroyed when the scene loads
    public List<string> destroyList = new List<string>();

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("Main Character");
        DontDestroyOnLoad(this);

        if (MonsterCollider.enemyCollide)
        {
            float x_pos = PlayerPrefs.GetFloat("player_x");
            float y_pos = PlayerPrefs.GetFloat("player_y");
            float z_pos = PlayerPrefs.GetFloat("player_z");
            float x_rot = PlayerPrefs.GetFloat("rotation_x");
            float y_rot = PlayerPrefs.GetFloat("rotation_y");
            float z_rot = PlayerPrefs.GetFloat("rotation_z");
            //float w_rot = PlayerPrefs.GetFloat("rotation_w");

            player.transform.position = new Vector3(x_pos-5, y_pos, z_pos-5);
            player.transform.rotation = new Quaternion(x_rot, y_rot, z_rot, 0);
            MonsterCollider.enemyCollide = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
