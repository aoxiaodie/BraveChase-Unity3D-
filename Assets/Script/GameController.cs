using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	Menu,
	Playing,
	End
}

public class GameController : MonoBehaviour {
	public static GameState gameState = GameState.Menu;
	public GameObject Tap_To_Start_UI;
	public GameObject Game_Over_UI;

	void Update()
	{
		if(gameState == GameState.Menu)
		{
			if (Input.GetMouseButtonDown(0))
			{
				gameState = GameState.Playing;
				Tap_To_Start_UI.SetActive(false);
			}
		}
		if(gameState == GameState.End)
		{
			Game_Over_UI.SetActive(true);
			if (Input.GetMouseButtonDown(0)){
				gameState = GameState.Menu;
				Application.LoadLevel(0);
			}
		}
	}
}
