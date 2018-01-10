using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @file LocalLeaderboard.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date December 6thth, 2017
 * @brief The purpose of this file is to update and store
 * data to reflect highscores. 
 * */
public class LocalLeaderboard : MonoBehaviour {

	// Use this for initialization
	/**
	 * @brief The Start() function is used to update the highscores
	 * in the event of a new highscore.
	 * */
	void Start () {
		Text scoreBoard = GetComponent<Text> ();
		scoreBoard.text = "LOCAL SCORES\n\n" + "1st: " + PlayerPrefs.GetInt ("1st Place", 0) + "\n2nd: "
		+ PlayerPrefs.GetInt ("2nd Place", 0) + "\n3rd: " + PlayerPrefs.GetInt ("3rd Place", 0) + "\n4th: " +
		PlayerPrefs.GetInt ("4th Place", 0) + "\n5th: " + PlayerPrefs.GetInt ("5th Place", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
