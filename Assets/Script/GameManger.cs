﻿using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {
	
	public enum Game_Status {Normal, Over, Pass, Pause};
	public static Game_Status game_status = Game_Status.Normal;

	void Start () {
		DontDestroyOnLoad(gameObject);
		game_status = Game_Status.Normal;
	}
	
	void Update () {
		switch (game_status) {
		case Game_Status.Over:
			game_status = Game_Status.Normal;
			print(Player.player_status);
			if(Player.player_status == Player.Player_Status.Over){
				StartCoroutine(WaitAndLoad(0.5f));
			}
			print ("over");
			break;
		case Game_Status.Pass:
			int currentLevel = Application.loadedLevel;
			int nextLevel = currentLevel +1;
			print(nextLevel);
			game_status = Game_Status.Normal;

			Application.LoadLevel(nextLevel);
			break;
		default:
			break;
		}	
	}

	IEnumerator WaitAndLoad(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(Application.loadedLevel);
	}
}
