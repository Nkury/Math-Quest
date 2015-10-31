using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestLogScript : MonoBehaviour {

	public Text questPrimaryText;
	public Text questMasteryText;

	//public Image questPrimaryStatusImage;
	//public Image questMasteryStatusImage;

	// Use this for initialization
	void Start () {
		questPrimaryText.text = "- Kill the bandits!";
		questMasteryText.text = "- Master addition!";

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setPrimaryText(string s) {
		questPrimaryText.text = s;
	}

	void setMasteryText(string s) {
		questMasteryText.text = s;
	}

	//void completePrimaryQuest() {
	//	questPrimaryStatusImage.
	//}

	//void completeMasteryQuest() {
	//}
}
