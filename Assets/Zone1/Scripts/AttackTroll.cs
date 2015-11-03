using UnityEngine;
using System.Collections;

public class AttackTroll : MonoBehaviour
{
    public NPCWander wander;
    public bool test;


    void OnTriggerEnter(Collider other)
    {
        //test = true;
        if (other.gameObject.tag == "Player")
        {
            GameObject g = GameObject.FindGameObjectWithTag("troll");
            wander = g.GetComponent<NPCWander>();
            wander.attackTroll = true;
        }

    }
}