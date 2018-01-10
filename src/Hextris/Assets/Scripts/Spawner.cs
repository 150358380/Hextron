using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @file Spawner.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to implement the spawning 
 * functionality for the trapezoids.
 * */
public class Spawner : MonoBehaviour {
	public GameObject[] coloredTrapezoids;
	public GameObject blackTrapezoid;
	public GameObject rainbowTrapezoid;
	GameObject hexagon;
	GameObject canvas;
	public float spawnSpeed;
	public static float angle = 0;
	public int countForBlack = 0;
	public int countForRainbow = 0;
	/**
	 * @brief The Start() function is used to initialize all the
	 * variables and GameObjects needed to spawn a trapezoid. 
	 * */
	void Start () {
		hexagon = GameObject.Find ("Black Hexagon");
		canvas = GameObject.Find ("Canvas");
		spawnSpeed = 2f;
		InvokeRepeating ("spawnOne", 2f, spawnSpeed);
		InvokeRepeating ("increaseRate", 10f, 10f);
	}


	/**
	 * @brief This function randomly generates one trapezoid. It 
	 * randomly chooses one of 6 places to spawn and it also
	 * randomly chooses the colour between the four colours.
	 * */
	void spawnOne(){
		
		if (countForRainbow == 20) {
			int randomRotation = Random.Range (0, 6);
			Vector3 properPosition = hexagon.transform.position;
			properPosition.y += 50.77f;

			angle = 60f * randomRotation;
			GameObject newObject = Instantiate (rainbowTrapezoid, properPosition, Quaternion.identity);

			newObject.transform.parent = canvas.transform;
			countForRainbow = 0;
		}
		//Takes a random object type and rotation within the range of min (inclusive) and 
		//max (exclusive)
		else if (countForBlack == 10) {
			
			int randomRotation = Random.Range (0, 6);
			Vector3 properPosition = hexagon.transform.position;
			properPosition.y += 50.77f;

			angle = 60f * randomRotation;
			GameObject newObject = Instantiate (blackTrapezoid, properPosition, Quaternion.identity);

			newObject.transform.parent = canvas.transform;
			countForBlack = 0;
		} else {
			int randomObject = Random.Range (0, coloredTrapezoids.Length);
			int randomRotation = Random.Range (0, 6);
			Vector3 properPosition = hexagon.transform.position;
			properPosition.y += 50.77f;
			angle = 60f * randomRotation;
			GameObject newObject = 
				Instantiate (coloredTrapezoids [randomObject], properPosition, Quaternion.identity);
			newObject.transform.parent = canvas.transform;
			countForBlack++;
			countForRainbow++;
		}

	}

	/**
	 * @brief This function increases the rate of spawning after a given period of time and reinvokes the
	 * spawning function given a new speed to implement. 
	 * */
	void increaseRate(){
		float speedIncrease = 0.2f;

		CancelInvoke ("spawnOne");
		spawnSpeed = spawnSpeed - speedIncrease;
		if (spawnSpeed < 0.4f) {
			spawnSpeed = 0.4f;
		}
		InvokeRepeating ("spawnOne", spawnSpeed, spawnSpeed);
	}
}
