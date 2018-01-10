using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
public class NewPlayModeTest {
	
	[UnityTest]
	public IEnumerator checkLoadingOfGameScene(){

		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
		//UnityEngine.Random.Range (0, 10).ReturnsForAnyArgs (10);
		//var blueTrapezoidPrefab = Resources.Load ("Prefabs/BlueTrapezoid");
		yield return null;

		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		//GameObject canvas = GameObject.Find ("Canvas");
		Assert.IsNotNull (blackHexagon);
		//GameObject blueTrapezoid = GameObject.FindWithTag ("BlueTrapezoid(Clone)");
		//Assert.AreEqual (blueTrapezoid.name, "BlueTrapezoid(Clone)");
	}

	[UnityTest]
	public IEnumerator checkLoadingOfStartScene(){
		
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Start");

		yield return null;

		GameObject musicPlayer = GameObject.Find ("MusicPlayer");
		Assert.IsNotNull (musicPlayer);
	}

	//Checks out to see if the gameObjects spawn 
	[UnityTest]
	public IEnumerator checkRandomSpawning(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (4);



		GameObject[] yellowTrapezoids = GameObject.FindGameObjectsWithTag ("YellowTrapezoid(Clone)");
		GameObject[] blueTrapezoids = GameObject.FindGameObjectsWithTag ("BlueTrapezoid(Clone)");
		GameObject[] redTrapezoids = GameObject.FindGameObjectsWithTag("RedTrapezoid(Clone)");
		GameObject[] greenTrapezoids = GameObject.FindGameObjectsWithTag("GreenTrapezoid(Clone)");

		Assert.IsTrue ((yellowTrapezoids.Length + blueTrapezoids.Length + 
			redTrapezoids.Length + greenTrapezoids.Length) == 2);
	}

	//Checks change in spawn rate
	[UnityTest]
	public IEnumerator checkChangeInSpawnRate(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		yield return new WaitForSeconds (11.8f);

		//Prevents all input from being run.


		GameObject[] yellowTrapezoids = GameObject.FindGameObjectsWithTag ("YellowTrapezoid(Clone)");
		GameObject[] blueTrapezoids = GameObject.FindGameObjectsWithTag ("BlueTrapezoid(Clone)");
		GameObject[] redTrapezoids = GameObject.FindGameObjectsWithTag("RedTrapezoid(Clone)");
		GameObject[] greenTrapezoids = GameObject.FindGameObjectsWithTag("GreenTrapezoid(Clone)");

		Assert.IsTrue((yellowTrapezoids.Length + blueTrapezoids.Length + 
			redTrapezoids.Length + greenTrapezoids.Length) == 5);
	}

	[UnityTest]
	public IEnumerator checkBlackHexagonUpdate(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (4);
	
		Assert.IsTrue(firstRed.GetComponent<TrapezoidTransform> ().row == 0 && firstRed.GetComponent<TrapezoidTransform>().side == 5);
	}

	[UnityTest]
	public IEnumerator checkThreeConnection(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");
		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (0.5f);
		Spawner.angle = 120f;
		GameObject secondRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (0.5f);
		Spawner.angle = 180f;
		GameObject thirdRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (4);

		//Assures all gameObjects loaded into the scene are destroyed.
		Assert.IsTrue (GameObject.FindGameObjectsWithTag ("RedTrapezoid(Clone)").Length == 0);

	}



	[UnityTest]
	public IEnumerator checkGravityAfterDestroy(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");
		 
		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);
		GameObject firstBlue = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject droppingYellow = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		Spawner.angle = 120f;

		GameObject underneathBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/GreenTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);

		GameObject finalBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (3f);
		Assert.IsTrue (droppingYellow.GetComponent<TrapezoidTransform> ().row == 1 && droppingYellow.GetComponent<TrapezoidTransform> ().side == 5);
	}



	[UnityTest]
	public IEnumerator checkTwistCorrection(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);
		GameObject firstBlue = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject droppingYellow = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (4f);

		Spawner.angle = 120f;

		GameObject lastBlock = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);

		blackHexagon.transform.Rotate (0, 0, 60);
		HexagonBehaviour.areaZero = Quaternion.Euler (0, 0, 60) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;

		yield return new WaitForSeconds (1f);
		Assert.IsTrue (lastBlock.GetComponent<TrapezoidTransform> ().row == 4 && lastBlock.GetComponent<TrapezoidTransform> ().side == 5);
	}

	[UnityTest]
	public IEnumerator checkGameOverCondition(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);
		GameObject firstBlue = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject droppingYellow = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);
		GameObject thirdBlue = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject fourthBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject gameOverBlock = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);


		yield return new WaitForSeconds (3f);

		GameObject checkNoBlackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");

		yield return new WaitForSeconds (2f);

		Assert.IsNull (checkNoBlackHexagon);

	}

	[UnityTest]
	public IEnumerator checkRainbowDestroy(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		yield return new WaitForSeconds (1f);
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);

		GameObject secondRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		Spawner.angle = 180f;

		yield return new WaitForSeconds (1f);
		GameObject thirdRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);

		GameObject fourthRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		Spawner.angle = 120f;

		yield return new WaitForSeconds (1f);
		GameObject rainbow = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RainbowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (5f);

		GameObject[] noReds = GameObject.FindGameObjectsWithTag ("RedTrapezoid(Clone)");

		Assert.IsTrue (noReds.Length == 0);

	}

	[UnityTest]
	public IEnumerator checkStackOnTop(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		GameObject firstRed = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/RedTrapezoid"), canvas.transform);
		yield return new WaitForSeconds (1f);
		GameObject firstBlue = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondBlue = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/BlueTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject droppingYellow = GameObject.Instantiate((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (4f);

		Spawner.angle = 120f;

		GameObject lastBlock = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/YellowTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1.5f);

		blackHexagon.transform.Rotate (0, 0, 60);
		HexagonBehaviour.areaZero = Quaternion.Euler (0, 0, 60) * (HexagonBehaviour.areaZero - blackHexagon.transform.position) + blackHexagon.transform.position;

		yield return new WaitForSeconds (1f);
		Assert.IsTrue (lastBlock.GetComponent<TrapezoidTransform> ().row == 4 && lastBlock.GetComponent<TrapezoidTransform> ().side == 5);
	}

	//Note: Create a setUp() method for different test cases.
	[UnityTest]
	public IEnumerator checkBlackTrapezoidsNotDestroying(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");

		yield return new WaitForSeconds (0.1f);

		GameObject spawner = GameObject.Find ("SpawningObject");
		GameObject.Destroy (spawner);
		GameObject blackHexagon = GameObject.FindGameObjectWithTag ("Black Hexagon");
		HexagonBehaviour preventInput = blackHexagon.GetComponent<HexagonBehaviour> ();
		preventInput.enabled = false;

		GameObject canvas = GameObject.Find ("Canvas");

		yield return new WaitForSeconds (1f);
		Spawner.angle = 60f;
		GameObject firstBlack = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlackTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject secondBlack = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlackTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (1f);
		GameObject thirdBlack = GameObject.Instantiate ((GameObject)Resources.Load ("Prefabs/BlackTrapezoid"), canvas.transform);

		yield return new WaitForSeconds (4f);


		Assert.IsTrue (GameObject.Find ("BlackTrapezoid(Clone)") != null);
	}
}
