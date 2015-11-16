using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuestLogManager : MonoBehaviour {

	public Image primaryQuestImage;
	public Image primaryQuestBackgroundImage;
	public Text masteryQuestCount;

	public ArrayList questPrimaryQuests = new ArrayList();

	private int currentPrimaryQuest = 0;
	private int finalPrimaryQuest = 0;

	private int currentMasteryProgress = 0;
	private int goalMasteryProgress = 0;

	// Use this for initialization
	void Start () {
		QuestDelegate.onMasteryQuestProgressUpdated += this.masteryQuestStatusUpdated;
		QuestDelegate.onPrimaryQuestProgressUpdated += this.primaryQuestStatusUpdated;

		masteryQuestCount.text = "0";

		initMasteryProgress(0, 1000);
		initPrimaryProgress();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void initPrimaryProgress() {

	}

	public void initMasteryProgress(int initial, int goal) {
		currentMasteryProgress = initial;
		goalMasteryProgress = goal;
	}

	public void primaryQuestStatusUpdated() {
		primaryQuestBackgroundImage.color = new Color(0, 1, 0, 1);
	}

	public void nextPrimaryQuest() {
		if(currentPrimaryQuest < finalPrimaryQuest) {
			currentPrimaryQuest++;
			primaryQuestBackgroundImage.color = new Color(1, 0, 0, 1);
		}
	}

	public void masteryQuestStatusUpdated() {
		Console.WriteLine("Test");
		if (currentMasteryProgress < goalMasteryProgress) {
			currentMasteryProgress++;
			masteryQuestCount.text = Convert.ToString(currentMasteryProgress);
		}
	}
}
