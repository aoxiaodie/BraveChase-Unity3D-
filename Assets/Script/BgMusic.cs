using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour {

	public AudioSource bgMusic;

	void Update()
	{
		if (GameController.gameState == GameState.End)
		{
			bgMusic.Stop();
		}
	}
}
