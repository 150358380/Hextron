using UnityEngine;
using System.Collections;


/**
 * @file MusicPlayer.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to manage the music
 * and make sure the music is playing.
 * */
public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	// Use this for initialization

	/**
	 * @brief This function loads and plays the music
	 * file.
	**/
	void Awake() {
		if (instance != null) {
			//Destroys duplicate object
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

}
