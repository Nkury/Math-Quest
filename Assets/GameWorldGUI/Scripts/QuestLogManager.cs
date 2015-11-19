using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class QuestLogManager : MonoBehaviour {

	public Image primaryQuestImage;
	public Image primaryQuestBackgroundImage;
	public Image primaryQuestDescriptionBG;
	public Text primaryQuestDescriptionText;
	public Text masteryQuestCount;

	public ArrayList questPrimaryQuests = new ArrayList();
	public float FadeDuration = 1f;

	private float lastColorChangeTime;
	private int currentPrimaryQuest = 0;
	private int finalPrimaryQuest = 0;

	private int currentMasteryProgress = 0;
	private int goalMasteryProgress = 0;

	private Color colorPrimaryQuestComplete = new Color(0, 1, 0, 1);
	private Color colorPrimaryQuestIncomplete = new Color(1, 0, 0, 1);

	private Color startColor = new Color(1, 0, 0, 1);
	private Color endColor = new Color(1, 1, 0, 1);

	private Image[] questImages = new Image[];

	// Use this for initialization
	void Start () {
		questImages = 

		QuestDelegate.onMasteryQuestProgressUpdated += this.masteryQuestStatusUpdated;
		QuestDelegate.onPrimaryQuestProgressUpdated += this.primaryQuestStatusUpdated;

		masteryQuestCount.text = "0";

		initMasteryProgress(0, 1000);
		initPrimaryProgress();
	}
	
	// Update is called once per frame
	void Update () {

		if (primaryQuestBackgroundImage.color != colorPrimaryQuestComplete) {
			var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
			ratio = Mathf.Clamp01(ratio);
			//primaryQuestBackgroundImage.color = Color.Lerp(startColor, endColor, ratio);
			//primaryQuestBackgroundImage.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
			primaryQuestBackgroundImage.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect
		
			if (ratio == 1f) {
				lastColorChangeTime = Time.time;
			
				// Switch colors
				var temp = startColor;
				startColor = endColor;
				endColor = temp;
			}
		}
	}

	public void initPrimaryProgress() {

	}

	public void initMasteryProgress(int initial, int goal) {
		currentMasteryProgress = initial;
		goalMasteryProgress = goal;
	}

	public void primaryQuestStatusUpdated() {
		primaryQuestBackgroundImage.color = colorPrimaryQuestComplete;
	}

	public void nextPrimaryQuest() {
		if(currentPrimaryQuest < finalPrimaryQuest) {
			currentPrimaryQuest++;
			primaryQuestBackgroundImage.color = new Color(1, 0, 0, 1);
		}
	}

	public void masteryQuestStatusUpdated(int stars) {
		Console.WriteLine("Test");
		if (currentMasteryProgress < goalMasteryProgress) {
			currentMasteryProgress+=stars;
			masteryQuestCount.text = Convert.ToString(currentMasteryProgress);
		}
	}
}
