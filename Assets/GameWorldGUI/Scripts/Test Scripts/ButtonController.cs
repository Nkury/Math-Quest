﻿using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Remove this when ready to implement
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdatePrimary(){
		QuestDelegate.primaryQuestUpdated();
	}

	public void UpdateMastery() {
		QuestDelegate.masteryQuestUpdated();
	}
}
