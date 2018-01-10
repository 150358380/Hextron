using UnityEngine;
using System.Collections;

/**
 * @file LevelManager.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to manage the loading
 * of the game and transitioning between different game scenes.
 * */
public class LevelManager : MonoBehaviour {

	/**
	 * @brief The purpose of this function is to load an inputted
	 * level. This is determined by button presses for reaching different
	 * menus.
	 * */
	public void LoadLevel(string name){
		Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel (name);
	}

	/**
	 * @brief The purpose of this function is to handle
	 * the quit function when it is requested.This only works
	 * in an executable file, and has been verified multiple times.
	 * */
	public void QuitRequest(){
		Debug.Log ("I WANT TO QUIT!");
		Application.Quit ();
	}
}
