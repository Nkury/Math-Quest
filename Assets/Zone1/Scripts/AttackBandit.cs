using UnityEngine;
using System.Collections;

public class AttackBandit : MonoBehaviour {
    public NPCWander wander;
    public bool test; 
	

    void OnTriggerEnter(Collider other)
    {
        test = true;
        if (other.gameObject.tag == "Player")
        {
            GameObject g = GameObject.FindGameObjectWithTag("attackBandit");
            wander = g.GetComponent<NPCWander>();
            wander.attackBandit = true;
        }

    }
}
