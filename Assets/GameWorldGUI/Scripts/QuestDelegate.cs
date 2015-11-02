using UnityEngine;
using System.Collections;

public class QuestDelegate : MonoBehaviour {

	// Delegates
	public delegate void PrimaryQuestProgressHandler();
	public delegate void MasteryQuestProgressHandler();


	// Events
	public static event PrimaryQuestProgressHandler onPrimaryQuestProgressUpdated;
	public static event MasteryQuestProgressHandler onMasteryQuestProgressUpdated;

	public static void primaryQuestUpdated() {
		if (onPrimaryQuestProgressUpdated != null) {
			onPrimaryQuestProgressUpdated();
		}
	}

	public static void masteryQuestUpdated() {
		if (onMasteryQuestProgressUpdated != null) {
			onMasteryQuestProgressUpdated();
		}
	}
}
