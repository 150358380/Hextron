using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @file TrapezoidCollision.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to detect collisions between a gameObject and either
 * the center black hexagon, or with a child of the hexagon (i.e. a trapezoid that is already on
 * top of the hexagon). 
 * */
public class TrapezoidCollision : MonoBehaviour {
	

	public static int score = 0;
	public static GameObject[,] grid = new GameObject[6,8];
	public static bool[,] visited = new bool[6, 8];
	public static ArrayList connectedGameObjectsRow = new ArrayList();
	public static ArrayList connectedGameObjectsSide = new ArrayList();
	public static int enteringSide = 0;
	public static int enteringRow = 0;
	public float hexagonHeightPixels = 480f;


	/**
	 * @brief The purpose of this function is to detect a collision between two different
	 * rigidbodies with colliders inside of the child rectangles for the particular trapezoid.
	 * @detail This detects whether a collision with the black hexagon or any of the child trapezoids take place.
	 * If the gameObject that is hit is the black hexagon, the gameObject that this script is referring to
	 * will become a child of the hexagon. If not, then it will become a child of whichever trapezoid it hits.
	**/
	void OnCollisionEnter2D (Collision2D coll){

		gameObject.GetComponentInParent<Rigidbody2D> ().velocity = Vector2.zero;
		TrapezoidTransform swagger = gameObject.GetComponentInParent<TrapezoidTransform>();
		swagger.move = false;



		GameObject blackHexagon = GameObject.Find ("Black Hexagon");
		if (coll.gameObject.name.Equals ("Black Hexagon")) {


			swagger.gravityFall = false;

			gameObject.transform.parent.SetParent (coll.transform);

			Vector3 areaOne = Quaternion.Euler (0, 0, -60) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
			Vector3 areaTwo = Quaternion.Euler (0, 0, -120) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
			Vector3 areaThree = Quaternion.Euler (0, 0, -180) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
			Vector3 areaFour = Quaternion.Euler (0, 0, -240) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
			Vector3 areaFive = Quaternion.Euler (0, 0, -300) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;
			Vector2 zeroArea = HexagonBehaviour.areaZero;
			Vector2 oneArea = areaOne;
			Vector2 twoArea = areaTwo;
			Vector2 threeArea = areaThree;
			Vector2 fourArea = areaFour;
			Vector2 fiveArea = areaFive;

			if (Physics2D.OverlapCircle (zeroArea, 3) != null && Physics2D.OverlapCircle (zeroArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 0;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {
					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}

				ContinueFlowOfGame ();
			} else if (Physics2D.OverlapCircle (oneArea, 3) != null && Physics2D.OverlapCircle (oneArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 1;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {
					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}
				ContinueFlowOfGame ();
			} else if (Physics2D.OverlapCircle (twoArea, 3) != null && Physics2D.OverlapCircle (twoArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 2;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {
					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}
				ContinueFlowOfGame ();

			} else if (Physics2D.OverlapCircle (threeArea, 3) != null && Physics2D.OverlapCircle (threeArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 3;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {
					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}

				ContinueFlowOfGame ();
			} else if (Physics2D.OverlapCircle (fourArea, 3) != null && Physics2D.OverlapCircle (fourArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 4;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {
					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}
				ContinueFlowOfGame ();
			} else if (Physics2D.OverlapCircle (fiveArea, 3) != null && Physics2D.OverlapCircle (fiveArea, 3).gameObject == gameObject) {
				TrapezoidTransform trapezoidScript = gameObject.GetComponentInParent<TrapezoidTransform> ();
				trapezoidScript.side = 5;
				trapezoidScript.row = 0;

				if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
					grid [trapezoidScript.side, trapezoidScript.row] = gameObject.transform.parent.gameObject as GameObject;
					connectedGameObjectsRow.Add (trapezoidScript.row);
					connectedGameObjectsSide.Add (trapezoidScript.side);
					destroyAllAroundHexagon (trapezoidScript.side, trapezoidScript.row);
				} else {

					//grid [trapezoidScript.side,trapezoidScript.row] = gameObject as GameObject;
					addSelfAndChildren (gameObject.transform.parent.gameObject, trapezoidScript.side, trapezoidScript.row);

					for (int i = trapezoidScript.row; i < grid.GetLength (1); i++) {
						if (grid [trapezoidScript.side, i] == null) {
							continue;
						} else {
							enteringSide = trapezoidScript.side;
							enteringRow = i;
							if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
								floodfill (swagger.side, i, grid [swagger.side, i].name);
							}
						}
					}
				}
				ContinueFlowOfGame ();
			}
			
		} else if (coll.gameObject.transform.IsChildOf (blackHexagon.transform) && (gameObject.transform.parent.transform.childCount == 3)
		           && (!gameObject.transform.IsChildOf (blackHexagon.transform) ||
				((gameObject.GetComponentInParent<TrapezoidTransform> ().move == true
					|| gameObject.GetComponentInParent<TrapezoidTransform>().gravityFall == true) && gameObject.transform.IsChildOf (blackHexagon.transform)))) {
			

			GameObject topOfStack = topObject (coll.gameObject);
		
			//gameObject.transform.parent.SetParent (coll.transform);
			gameObject.transform.parent.SetParent (topOfStack.transform);
			gameObject.transform.parent.transform.position = topOfStack.transform.position + topOfStack.transform.up * 5.50f;

			TrapezoidTransform referenceScript = topOfStack.GetComponent <TrapezoidTransform> ();
			swagger.side = referenceScript.side;
			swagger.row = referenceScript.row + 1;

			if (gameObject.transform.parent.name.Equals ("RainbowTrapezoid(Clone)")) {
				grid [swagger.side, swagger.row] = gameObject.transform.parent.gameObject as GameObject;
				connectedGameObjectsRow.Add (swagger.row);
				connectedGameObjectsSide.Add (swagger.side);
				destroyAllAroundRegular (swagger.side, swagger.row);
			} else {
				swagger.gravityFall = false;
				addSelfAndChildren (gameObject.transform.parent.gameObject, swagger.side, swagger.row);

				for (int i = swagger.row; i < grid.GetLength (1); i++) {
					if (grid [swagger.side, i] == null) {
						continue;
					} else {
						enteringSide = swagger.side;
						enteringRow = i;
						if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
							floodfill (swagger.side, i, grid [swagger.side, i].name);
						}
					}
				}

				//checks every row and column of the grid after all blocks destroyed from floodfills of all children
				ContinueFlowOfGame ();
			}


		
			
				
		} else if (coll.gameObject.transform.IsChildOf (blackHexagon.transform) && (gameObject.transform.parent.transform.childCount == 4)
		           && 
		           (gameObject.GetComponentInParent<TrapezoidTransform> ().gravityFall == true && gameObject.transform.parent.transform.IsChildOf (blackHexagon.transform))) {

			gameObject.transform.parent.SetParent (coll.transform);

			TrapezoidTransform reference = coll.gameObject.GetComponent<TrapezoidTransform> ();
			swagger.side = reference.side;
			swagger.row = reference.row + 1;


			swagger.gravityFall = false;
			addSelfAndChildren (gameObject.transform.parent.gameObject, swagger.side, swagger.row);

			for (int i = swagger.row; i < grid.GetLength (1); i++) {
				if (grid [swagger.side, i] == null) {
					continue;
				} else {
					enteringSide = swagger.side;
					enteringRow = i;
					if (!grid [swagger.side, i].name.Equals ("BlackTrapezoid(Clone)")) {
						floodfill (swagger.side, i, grid [swagger.side, i].name);
					}

				}
			}

			//checks every row and column of the grid after all blocks destroyed from floodfills of all children
			ContinueFlowOfGame ();


		}
	}

	/**
	 * @brief This function determines the top object inside of a stack. This is used
	 * for when a large stack is rotated into a currently falling trapezoid.
	 * */
	public GameObject topObject(GameObject trapezoid){
		if (trapezoid.transform.childCount == 4) {
			return topObject (trapezoid.transform.GetChild (3).gameObject);
		} else {
			return trapezoid;
		}

	}


	public void destroyAllAroundHexagon(int side, int row){
		int leftSide = 0;
		int rightSide = 0;
		if (side == 0) {
			leftSide = 5;
		} else {
			leftSide = side - 1;
		}
			
		if (side == 5) {
			rightSide = 0;
		} else {
			rightSide = side + 1;
		}

			//Top left
			if (grid [leftSide, row + 1] != null) {
				connectedGameObjectsRow.Add (row + 1);
				connectedGameObjectsSide.Add (leftSide);
			}

			//Top Right
			if (grid [rightSide, row + 1] != null) {
				connectedGameObjectsRow.Add (row + 1);
				connectedGameObjectsSide.Add (rightSide);
			}

			//Left
			if (grid [leftSide, row] != null) {
				connectedGameObjectsRow.Add (row);
				connectedGameObjectsSide.Add (leftSide);
			}

			//Right
			if (grid [rightSide, row] != null) {
				connectedGameObjectsRow.Add (row);
				connectedGameObjectsSide.Add (rightSide);
			}
			
		print (connectedGameObjectsRow.Count);
			score += Mathf.CeilToInt(connectedGameObjectsRow.Count * 10f * Time.timeSinceLevelLoad);
			PauseManager.text.text = "" + score;
			for (int i = 0; i < connectedGameObjectsRow.Count; i++) {
				

				if (grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.childCount > 3){
					grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.GetChild(3).
					SetParent(PauseManager.canvas.transform);
				}
				Destroy(grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]]);
				EffectScript.playDestroy ();
				grid [(int)connectedGameObjectsSide [i], (int)connectedGameObjectsRow [i]] = null; 

			}

		connectedGameObjectsRow = new ArrayList ();
		connectedGameObjectsSide = new ArrayList ();
		
		ContinueFlowOfGame ();
	}


	public void destroyAllAroundRegular(int side, int row){
		int leftSide = 0;
		int rightSide = 0;
		if (side == 0) {
			leftSide = 5;
		} else {
			leftSide = side - 1;
		}

		if (side == 5) {
			rightSide = 0;
		} else {
			rightSide = side + 1;
		}

		//Top left
		if (grid [leftSide, row + 1] != null) {
			connectedGameObjectsRow.Add (row + 1);
			connectedGameObjectsSide.Add (leftSide);
		}

		//Top Right
		if (grid [rightSide, row + 1] != null) {
			connectedGameObjectsRow.Add (row + 1);
			connectedGameObjectsSide.Add (rightSide);
		}

		//Left
		if (grid [leftSide, row] != null) {
			connectedGameObjectsRow.Add (row);
			connectedGameObjectsSide.Add (leftSide);
		}

		//Right
		if (grid [rightSide, row] != null) {
			connectedGameObjectsRow.Add (row);
			connectedGameObjectsSide.Add (rightSide);
		}

		//Bottom Left
		if (grid [leftSide, row - 1] != null) {
			connectedGameObjectsRow.Add (row - 1);
			connectedGameObjectsSide.Add (leftSide);
		}

		//Bottom Middle
		if (grid [side, row - 1] != null) {
			connectedGameObjectsRow.Add (row - 1);
			connectedGameObjectsSide.Add (side);
		}

		//Bottom Right
		if (grid [rightSide, row - 1] != null) {
			connectedGameObjectsRow.Add (row - 1);
			connectedGameObjectsSide.Add (rightSide);
		}

		score += Mathf.CeilToInt(connectedGameObjectsRow.Count * 10f * Time.timeSinceLevelLoad);
		PauseManager.text.text = "" + score;

		for (int i = 0; i < connectedGameObjectsRow.Count; i++) {
			if (grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.childCount > 3){
				grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.GetChild(3).
				SetParent(PauseManager.canvas.transform);
			}
			Destroy(grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]]);
			EffectScript.playDestroy ();
			grid [(int)connectedGameObjectsSide [i], (int)connectedGameObjectsRow [i]] = null;

		}

		connectedGameObjectsRow = new ArrayList ();
		connectedGameObjectsSide = new ArrayList ();

		ContinueFlowOfGame ();
	}



	/**
	 * @brief This function adds the trapezoid that collides and recursively calls each of its children to add to the 
	 * grid system as well.
	 * */
	void addSelfAndChildren(GameObject trapezoidObject, int side, int row){
		grid[side, row] = trapezoidObject;
		if (trapezoidObject.transform.childCount == 4) {
			TrapezoidTransform childScript = trapezoidObject.transform.GetChild (3).GetComponent<TrapezoidTransform> ();
			childScript.move = false;
			childScript.gravityFall = false;
			childScript.side = side;
			childScript.row = row + 1;
			addSelfAndChildren (trapezoidObject.transform.GetChild (3).gameObject, side, row + 1);
		}
	}

	/**
	 * @brief This function is a recursive algorithm that destroys blocks that are within
	 * a connection of three or greater. After all checks are completed, the game objects are destroyed.
	 * */
	void floodfill(int side, int row, string rootObjectType){
		//adds the trapezoid game object to the 
		connectedGameObjectsRow.Add(row);
		connectedGameObjectsSide.Add (side);
		visited[side, row] = true;

		//Right Side 
		if (side == 5) {
			int wrapAround = 0;
			if (grid [wrapAround, row] != null && grid [wrapAround, row].name ==
				rootObjectType && visited [wrapAround, row] == false) {
				floodfill (wrapAround, row, rootObjectType);
			}
		} else {
			if (grid [side + 1, row] != null && grid [side + 1, row].name == 
				rootObjectType && visited [side + 1, row] == false) {
				floodfill (side + 1, row, rootObjectType);
			}
		}

		//left side
		if (side == 0) {
			int leftWrapAround = 5;
			if (grid [leftWrapAround, row] != null && grid [leftWrapAround, row].name 
				== rootObjectType && visited [leftWrapAround, row] == false) {
				floodfill (leftWrapAround, row, rootObjectType);
			}
		} else {
			if (grid [side - 1, row] != null && grid [side - 1, row].name 
				== rootObjectType && visited [side - 1, row] == false) {
				floodfill (side - 1, row, rootObjectType);
			}
		}


		//down
		if (row > 0) {
			if (grid [side, row - 1] != null && grid [side, row - 1].name 
				== rootObjectType && visited [side, row - 1] == false) {
				floodfill (side, row - 1, rootObjectType);
			}
		}

		//up 
		if (row < 7) {
			if (grid [side, row + 1] != null && grid [side, row + 1].name 
				== rootObjectType && visited [side, row + 1] == false) {
				floodfill (side, row + 1, rootObjectType);
			}
		}

		//Ending and checking
		if (side == enteringSide && row == enteringRow) {
			if (connectedGameObjectsRow.Count >= 3) {
				score += Mathf.CeilToInt(connectedGameObjectsRow.Count * 10f * Time.timeSinceLevelLoad);
				PauseManager.text.text = "" + score;
				for (int i = 0; i < connectedGameObjectsRow.Count; i++) {
					if (grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.childCount > 3){
						grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]].transform.GetChild(3).
						SetParent(PauseManager.canvas.transform);
					}
					Destroy(grid[(int) connectedGameObjectsSide[i], (int) connectedGameObjectsRow[i]]);
					EffectScript.playDestroy ();
					grid [(int)connectedGameObjectsSide [i], (int)connectedGameObjectsRow [i]] = null;

				}
			}


			//Resets the visited array and connected game objects array
			visited = new bool[6,8];
			connectedGameObjectsRow = new ArrayList ();
			connectedGameObjectsSide = new ArrayList ();
		}
			
	}

	/**
	 * @brief This function continues the game after a floodfill is performed. 
	 * It checks if a gameObject on the grid has nothing beneath it. If it doesn't, then the 
	 * object and its children are removed from the grid, parented to the hexagon (so that it can
	 * rotate with the hexagon), and move towards the hexagon to move down however many 
	 * rows necessary. Game over conditions are also checked!
	 * */
	void ContinueFlowOfGame(){
		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 1; j < grid.GetLength (1); j++) {
					//if the current position on the grid is not null (a game object) and there 
					//is no game object beneath it (the grid is null).

					if (grid [i, j - 1] == null && grid [i, j] != null) {
						//Move that particular object, parent it to the canvas and remove its children from the grid as well.
						Transform objectTransform = grid [i, j].transform;
						objectTransform.SetParent (HexagonBehaviour.blackHexagon.transform);
						TrapezoidTransform objectScript = grid [i, j].GetComponent<TrapezoidTransform> ();
						objectScript.move = true;

						objectScript.gravityFall = true;
						grid [i, j] = null;
						for (int k = j + 1; k < grid.GetLength (1); k++) {
							//if the children chain stops (where the null value is reached from the j value).
							if (grid [i, k] == null) {
								break;
							} else {
								//remove it from from the grid (since it moves with its parent).
								objectScript = grid[i,k].GetComponent<TrapezoidTransform> ();
								objectScript.move = true;
								grid [i, k] = null;
							}
						}
					} 
				
			}
		}

		// Checks all sides after floodfill to check if 8 are stacked. If they are, load "game over" screen.
		// It also repositions the top five high scores if the final score of the game belongs in it locally.
		for (int i = 0; i < grid.GetLength (0); i++) {
			bool redFlag = false;
			for (int j = 0; j < grid.GetLength (1); j++) {
				if (grid [i, j] == null) {
					redFlag = true;
				}
			}

			//Checks if the column
			if (redFlag == true) {
				continue;
			} else {
				int topScore = PlayerPrefs.GetInt ("1st Place", 0);
				int secondScore = PlayerPrefs.GetInt ("2nd Place", 0);
				int thirdScore = PlayerPrefs.GetInt ("3rd Place", 0);
				int fourthScore = PlayerPrefs.GetInt ("4th Place", 0);
				int fifthScore = PlayerPrefs.GetInt ("5th Place", 0);

				if (score > topScore) {
					PlayerPrefs.SetInt ("5th Place", PlayerPrefs.GetInt ("4th Place", 0));
					PlayerPrefs.SetInt ("4th Place", PlayerPrefs.GetInt ("3rd Place", 0));
					PlayerPrefs.SetInt ("3rd Place", PlayerPrefs.GetInt ("2nd Place", 0));
					PlayerPrefs.SetInt ("2nd Place", PlayerPrefs.GetInt ("1st Place", 0));
					PlayerPrefs.SetInt ("1st Place", score);
				} else if (topScore >= score && score > secondScore) {
					PlayerPrefs.SetInt ("5th Place", PlayerPrefs.GetInt ("4th Place", 0));
					PlayerPrefs.SetInt ("4th Place", PlayerPrefs.GetInt ("3rd Place", 0));
					PlayerPrefs.SetInt ("3rd Place", PlayerPrefs.GetInt ("2nd Place", 0));
					PlayerPrefs.SetInt ("2nd Place", score);
				} else if (secondScore >= score && score > thirdScore) {
					PlayerPrefs.SetInt ("5th Place", PlayerPrefs.GetInt ("4th Place", 0));
					PlayerPrefs.SetInt ("4th Place", PlayerPrefs.GetInt ("3rd Place", 0));
					PlayerPrefs.SetInt ("3rd Place", score);
				} else if (thirdScore >= score && score > fourthScore) {
					PlayerPrefs.SetInt ("5th Place", PlayerPrefs.GetInt ("4th Place", 0));
					PlayerPrefs.SetInt ("4th Place", score);
				} else if (fourthScore >= score && score > fifthScore) {
					PlayerPrefs.SetInt ("5th Place", score);
				}
			
				//Resets score
				score = 0;
				Application.LoadLevel ("GameOver");
			}
		}
	}

}
