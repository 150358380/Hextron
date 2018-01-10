using UnityEngine;
using System.Collections;

/**
 * @file HexagonBehaviour.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to implement the behaviour
 * of the Hexagon, mostly the physical rotation of the Hexagon and pausing.
 * */
public class HexagonBehaviour : MonoBehaviour {
	public float transitionSpeed = 0.3f;
	public float degree = 0f;
	public bool timeStay = false;
	public static GameObject blackHexagon = null;
	public static Vector3 areaZero;
	public float hexagonHeightPixels = 480f;
	public bool pause = false;


	/**
	 * @brief The purpose of this function is to initialize the hexagon
	 * game object. It finds the GameObject and binds it to
	 * blackHexagon. An area defined as areaZero is used so that TrapezoidCollision
	 * can reference it for determine which position of the grid a trapezoid should go to.
	 * */
	void Start () {
		blackHexagon = GameObject.Find ("Black Hexagon");
		areaZero = 
			new Vector3 (blackHexagon.transform.position.x, blackHexagon.transform.position.y + hexagonHeightPixels/2 / 100 *
				blackHexagon.transform.lossyScale.y, 0);
		//Have to ensure that trapezoid collision's score is set to zero. 
		TrapezoidCollision.score = 0;
	}
	

	/**
	 * @brief The purpose of this function is to implement the
	 * rotation functionality of the hexagon. It checks if the
	 * gmae is paused, and if it isn't then it checks if the user
	 * pressed either the left or right arrow key. On the key
	 * press, it rotates the hexagon 60 degrees to the
	 * direction of the key. It also creates pausing of the game 
	 * by preventing rotation of the hexagon and stopping all translations.
	 * */
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.P)){
			if (Time.timeScale == 0) {
				pause = false;
				Time.timeScale = 1;
			} else {
				pause = true;
				Time.timeScale = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow) && pause == false) {
			transform.Rotate (0, 0, 60);
			areaZero = Quaternion.Euler (0, 0, 60) * (areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow) && pause == false) {
			transform.Rotate (0, 0, -60);
			areaZero = Quaternion.Euler (0, 0, -60) * (areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
		}


		

	}
		
}
