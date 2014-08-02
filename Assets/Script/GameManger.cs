using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {
	
	public enum Game_Status {Normal, Over, Pass, Pause};
	public static Game_Status game_status = Game_Status.Normal;

	void Start () {
		game_status = Game_Status.Normal;
	}
	
	void Update () {
		switch (game_status) {
		case Game_Status.Over:
			Application.LoadLevel(Application.loadedLevel);
			break;
		case Game_Status.Pass:
			int currentLevel = Application.loadedLevel;
			int nextLevel = currentLevel +1;
			Application.LoadLevel(nextLevel);
			break;
		default:
			break;
		}	
	}
}
