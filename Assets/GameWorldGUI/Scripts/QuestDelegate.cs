using UnityEngine;
using System.Collections;

public class QuestDelegate : MonoBehaviour {

	// Delegates
	public delegate void PrimaryQuestProgressHandler();
	public delegate void MasteryQuestProgressHandler(int stars);
	public delegate void NextPrimaryQuestRequestHandler();

	// Events
	public static event PrimaryQuestProgressHandler onPrimaryQuestProgressUpdated;
	public static event MasteryQuestProgressHandler onMasteryQuestProgressUpdated;
	public static event NextPrimaryQuestRequestHandler onNextPrimaryQuestRequest;


	public static void primaryQuestUpdated() {
		if (onPrimaryQuestProgressUpdated != null) {
			onPrimaryQuestProgressUpdated();
		}
	}

	public static void masteryQuestUpdated(int stars) {
		if (onMasteryQuestProgressUpdated != null) {
			onMasteryQuestProgressUpdated(stars);
		}
	}

	public static void nextPrimaryQuestRequest() {
		if (onNextPrimaryQuestRequest != null) {
			onNextPrimaryQuestRequest();
		}
	}
}
