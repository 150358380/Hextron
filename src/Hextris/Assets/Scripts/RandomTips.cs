using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomTips : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text tipText = GetComponent<Text> ();
		int randomTip = Random.Range (0, 5);
		if (randomTip == 0) {
			tipText.text = "YOU LOST! \n\n" + "Tip: Try twisting stacks onto falling objects! It may save you a few times!";
		} else if (randomTip == 1) {
			tipText.text = "YOU LOST! \n\n" + "Tip: To increase your score, try connecting more objects at once!";
		} else if (randomTip == 2) {
			tipText.text = "YOU LOST! \n\n" + "Tip: Points scale with time, so maybe try to save simple connections for later!";
		} else if (randomTip == 3) {
			tipText.text = "YOU LOST! \n\n" + "Tip: Practice makes perfect! Coordinate position as spawn rate gets faster!";
		} else if (randomTip == 4) {
			tipText.text = "YOU LOST! \n\n" + "Tip: Try to leave open space so later connections can be made!";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
