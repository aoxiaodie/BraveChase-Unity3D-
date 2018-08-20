using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {
	public AudioSource end;
	private bool havePlayMusic = false;

	private Transform player;
	private Vector3 offset = Vector3.zero;

	public float moveSpeed = 5;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset = transform.position - player.position;
	}

	void Update()
	{
		Vector3 tagetPos = player.position + offset;
		transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);

		if(havePlayMusic == false && GameController.gameState == GameState.End)
		{
			end.Play();
			havePlayMusic = true;
		}
	}
}
