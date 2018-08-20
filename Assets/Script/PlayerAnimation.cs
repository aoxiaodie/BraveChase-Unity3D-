using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState
{
	Idle,
	Run,
	Jump,
	Slide,
	Death
}

public class PlayerAnimation : MonoBehaviour {
	public Animator animator;
	public PlayerMove playerMove;

	public AnimationState animationState = AnimationState.Idle;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (GameController.gameState == GameState.Menu)
		{
			animator.SetBool("isMove",false);
			animationState = AnimationState.Idle;
		}
		else if (GameController.gameState == GameState.Playing)
		{
			animator.SetBool("isMove", true);
			animationState = AnimationState.Run;
			if (playerMove.isJumping == true)
			{
				animator.SetInteger("flag", 1);
				animationState = AnimationState.Jump;
			}
			if (playerMove.isSliding == true)
			{
				animator.SetInteger("flag", 2);
				animationState = AnimationState.Slide;
			}
			if (playerMove.isJumping == false || playerMove.isSliding == false)
			{
				animator.SetInteger("flag", 0);
				animationState = AnimationState.Run;
			}
		}
		else if (GameController.gameState == GameState.End)
		{
			animator.SetBool("isOver", true);
			animationState = AnimationState.Death;
		}
	}
}

/*
 * 
 * public Animation _animation;
	private AnimationState animationState = AnimationState.Idle;

	void Awake()
	{
		_animation = transform.Find("Armature").GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameController.gameState == GameState.Menu)
		{
			animationState = AnimationState.Idle;
		}
		else if (GameController.gameState == GameState.Playing)
		{
			animationState = AnimationState.Run;
		}
	}

	void LateUpdate()
	{
		switch (animationState)
		{
			case AnimationState.Idle:
				PlayIdle();
				break;
			case AnimationState.Run:
				break;
		}
	}

	private void PlayIdle()
	{
		if (!_animation.IsPlaying("FreeVoxelGirl-idle") == false)
		{
			_animation.Play("FreeVoxelGirl-idle");
		}
	}

 */
