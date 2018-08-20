using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	private Transform player;
	private Vector3 offset = Vector3.zero;

	public float moveSpeed = 5;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset = transform.position - player.position;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tagetPos = player.position + offset;
		transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);
	}
}
