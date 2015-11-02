using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestLogManager : MonoBehaviour {

	public Text questPrimaryDescription;
	public Text questMasteryDescription;

	public Text questPrimaryProgress;
	public Text questMasteryProgress;

	public int questPrimaryID;
	public int questMasteryID;

	private int currentPrimaryProgress = 0;
	private int goalPrimaryProgress = 0;

	private int currentMasteryProgress = 0;
	private int goalMasteryProgress = 0;

	// Use this for initialization
	void Start () {
		QuestDelegate.onMasteryQuestProgressUpdated += this.masteryQuestUpdated;
		QuestDelegate.onPrimaryQuestProgressUpdated += this.primaryQuestUpdated;

		// This should be removed whe
		questPrimaryDescription.text = "Find the dynamite";
		questMasteryDescription.text = "Complete 5 addition problems";
		
		initMasteryProgress(0, 5);
		initPrimaryProgress(0, 1);
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void setPrimaryText(string s) {
		questPrimaryDescription.text = s;
	}

	public void setMasteryText(string s) {
		questMasteryDescription.text = s;
	}

	public void initPrimaryProgress(int initial, int goal) {
		currentPrimaryProgress = initial;
		goalPrimaryProgress = goal;

		questPrimaryProgress.text = initial + " / " + goal; 
	}

	public void initMasteryProgress(int initial, int goal) {
		currentMasteryProgress = initial;
		goalMasteryProgress = goal;

		questMasteryProgress.text = initial + " / " + goal; 
	}

	public void primaryQuestUpdated() {
		if (currentPrimaryProgress < goalPrimaryProgress) {
			currentPrimaryProgress++;
			questPrimaryProgress.text = currentPrimaryProgress + " / " + goalPrimaryProgress;
		}
	}

	public void masteryQuestUpdated() {
		if (currentMasteryProgress < goalMasteryProgress) {
			currentMasteryProgress++;
			questMasteryProgress.text = currentMasteryProgress + " / " + goalMasteryProgress;
		}
	}
}
