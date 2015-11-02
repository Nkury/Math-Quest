using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject zone1storage = GameObject.Find("PersistentZone1");
        List<string> dList = zone1storage.GetComponent<Zone1Start>().destroyList;
        
        // if the list of names goes over 3, then respawn those enemies that the player fought in the beginning
        if (dList.Count > 3)
            dList.RemoveAt(0);

        // destroys all the objects the player has defeated
        foreach (string name in dList)
            Destroy(GameObject.Find(name));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
