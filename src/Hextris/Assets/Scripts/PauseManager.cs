using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * @file PauseManager.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to ensure that the canvas is always position towards the center of the 
 * camera and vice versa. 
 * */
public class PauseManager : MonoBehaviour {
	
	public static GameObject canvas;
	public static Text text;
	public static GameObject camera;
	// Use this for initialization

	/**
	 * @brief The purpose of this function is 
	 * to initialize the gameObjects within the context of this class.
	 * Canvas is static such that TrapezoidCollisions can refer to it 
	 * for parenting when trapezoids have nothing underneath them after 
	 * destroying.
	**/
	void Start () {
		camera = GameObject.Find ("Main Camera");
		canvas = GameObject.Find ("Canvas");
		text = canvas.transform.GetChild (2).GetComponent<Text> ();
	}
	
	// Update is called once per frame

	/**
	 * @brief The purpose of this function is always ensure
	 * the camera is in the middle of 
	 * the canvas for proper positioning.
	**/
	void Update () {


		camera.transform.position = canvas.transform.position;

	}
}
