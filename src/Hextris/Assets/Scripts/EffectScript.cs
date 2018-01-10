using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @file EffectScript.cs
 * @author Jason Li, Yousaf Shaheen, Scott Williams
 * @date November 8th, 2017
 * @brief The purpose of this file is to play sounds whenever
 * a series of three or more objects is destroyed from a floodfill inside
 * TrapezoidCollision.
 * */
public class EffectScript : MonoBehaviour {
	static AudioSource audioSrc;


	/**
	 * @brief The Start() function is used to obtain the AudioSource component of the gameObject
	 * that contains this script. 
	 * */
	void Start () {
		audioSrc = GetComponent<AudioSource> ();
	}
	

	/**
	 * @brief This function is called from TrapezoidCollision and
	 * plays the destroy block sound provided. 
	 * */
	public static void playDestroy(){
		audioSrc.Play ();
	}
}
