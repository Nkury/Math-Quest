using UnityEngine;
using System;

public class ButtonController : MonoBehaviour {
	
	private System.Random randomNumber = new System.Random();


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
		QuestDelegate.masteryQuestUpdated(randomNumber.Next(1,4));
	}
}
