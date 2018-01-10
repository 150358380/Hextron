using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @file TrapezoidTransform.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date October 26, 2017
 * @brief The purpose of this file is to define the behaviour for each color of trapezoid.
 * This includes both the motion, scaling, and position of the trapezoids.
 * */
public class TrapezoidTransform : MonoBehaviour {

	public int side = 0;
	public int row = 0;
	public bool move = true;
	public bool gravityFall = false;
	public float pixelsPerUnit = 100f;
	public float rectangleHeightPixels = 271f;
	public float rectangleWidthPixels = 373f;
	public float hexagonHeightPixels = 480f;
	public float scalePerUnit = 0;
	public float halfHexagonWorldUnit = 0;
	public float hexagonSideWorldUnit = 0;
	public float rectangleScaleOnSide = 0;
	GameObject rectangle;
	GameObject leftSide;
	GameObject rightSide;
	public string objectType;
	GameObject hexagon;
	// Use this for initialization

	/**
	 * The start method, which instantiates all trapezoid gameObjects with specific positions relative 
	 * to the player. 
	 * @brief This is the intial setup the script does. One time run.
	 * */

	void Start () {
		//Determines if the transform of the object with the attached script goes by the name 
		//YellowTrapezoid. If so, then the code in the conditional statement executes. 

		objectType = gameObject.name;

		Vector3 v3 = transform.position;
		float rectanglesDistance = 20f;


		float worldUnitHeight = rectangleHeightPixels / pixelsPerUnit * transform.lossyScale.y * 2;
		//adds to the y component the relative distance in rectangles given
		//by the second multiplier.

		v3.y += worldUnitHeight * rectanglesDistance;
		//Creates a new transform position that adds the y-component. 
		transform.position = v3;

		hexagon = GameObject.Find ("Black Hexagon");

		halfHexagonWorldUnit = hexagonHeightPixels / pixelsPerUnit * hexagon.transform.lossyScale.y / 2;
		hexagonSideWorldUnit = halfHexagonWorldUnit * Mathf.Tan (Mathf.PI / 6) * 2;
		rectangleScaleOnSide = hexagonSideWorldUnit / (rectangleWidthPixels / pixelsPerUnit);
		scalePerUnit = rectangleScaleOnSide / (halfHexagonWorldUnit + (rectangleHeightPixels / pixelsPerUnit) / 2);

		if (objectType.Length >= 15 && objectType.Substring (0, 15).Equals ("YellowTrapezoid")) {
			definingTrapezoid ("YellowRectangle", "YellowSide1", "YellowSide2");
		} else if (objectType.Length >= 13 && objectType.Substring (0, 13).Equals ("BlueTrapezoid")) {
			definingTrapezoid ("BlueRectangle", "BlueSide1", "BlueSide2");
		} else if (objectType.Length >= 14 && objectType.Substring (0, 14).Equals ("GreenTrapezoid")) {
			definingTrapezoid ("GreenRectangle", "GreenSide1", "GreenSide2");
		} else if (objectType.Length >= 12 && objectType.Substring (0, 12).Equals ("RedTrapezoid")) {
			definingTrapezoid ("RedRectangle", "RedSide1", "RedSide2");
		} else if (objectType.Length >= 14 && objectType.Substring (0, 14).Equals ("BlackTrapezoid")) {
			definingTrapezoid ("BlackRectangle", "BlackSide1", "BlackSide2");
		} else if (objectType.Length >= 16 && objectType.Substring (0, 16).Equals ("RainbowTrapezoid")) {
			definingTrapezoid ("RainbowRectangle", "RainbowSide1", "RainbowSide2");
		}
			
		transform.RotateAround (hexagon.transform.position, Vector3.forward, Spawner.angle);
	}
	
	/**
	 * The update function defines the scaling factor that is required for every 
	 * world unit of distance the rectangle is away from the player hexagon. 
	 * It also defines the position of the two side triangles of yellow trapezoid
	 * to ensure that it they are always to the left and right of the rectangle.
	 * @brief This runs every frame.
	 * */
	void Update () {
		
			//Presets a certain speed in which the yellow trapezoid approaches the center of the black hexagon.
			float speed = 60f;
			Vector3 direction = (hexagon.transform.position - transform.position).normalized;

			Rigidbody2D swagger = GetComponent<Rigidbody2D> ();

			if (move == true) {
				swagger.MovePosition (transform.position + direction * speed * Time.deltaTime);
			} else {
				swagger.velocity = Vector2.zero;
			}
			
			//This finds the distance to the black hexagon placed within the centre of the Game Scene.
			float distanceToCenter = Vector3.Distance (transform.position, hexagon.transform.position);

			//This scales the child rectangle game object inside of the script. 
			rectangle.transform.localScale = new Vector3 (distanceToCenter * scalePerUnit,
				rectangle.transform.localScale.y, rectangle.transform.localScale.z);


			LeftSideStick (leftSide);
			RightSideStick (rightSide);


	}

	public void LeftSideStick(GameObject leftSide){
		
		leftSide.transform.position = rectangle.transform.position + 
			(-rectangle.transform.right * (1.57f + rectangle.transform.localScale.x * 3.73f/2f));
	}

	public void RightSideStick(GameObject rightSide){

		rightSide.transform.position = rectangle.transform.position +
			(rectangle.transform.right * (1.57f + rectangle.transform.localScale.x * 3.73f / 2f));
	}

	public void definingTrapezoid(string rectangleName, string leftSideName, string rightSideName){
		rectangle = transform.Find(rectangleName).gameObject;
		leftSide = transform.Find(leftSideName).gameObject;
		rightSide = transform.Find (rightSideName).gameObject;
	}
}
