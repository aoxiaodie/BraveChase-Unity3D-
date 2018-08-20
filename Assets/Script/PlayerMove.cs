using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchDir{
	None,
	Left,
	Right,
	Top,
	Bottom
}

public class PlayerMove : MonoBehaviour {
	public float moveSpeed = 5;
	public float minTouchLength = 50;

	private EvnGenerator evnGenerator;
	private TouchDir touchDir = TouchDir.None;
	private Vector3 lastMouseDown = Vector3.zero;

	private int nowLaneIndex = 1;
	private int targetLaneIndex = 1;

	private float moveHorizontal = 0;
	public float horizontalMoveSpeed = 0.2f;

	public bool isJumping = false;
	public bool isUp = true;
	public float jumpHeight = 4;
	public float jumpSpeed = 5;
	public float haveHeight = 0; // 已经跳跃的高度

	public bool isSliding = false;
	public bool isDown = true;
	public float slideHeight = 0.1f;
	public float slideSpeed = 1;
	public float haveSlideHeight = 0;

	public float slideTime = 0.967f; // 滑动动画持续时间
	private float slideTimer = 0;

	private Transform player;

	private float[] xOffset = new float[3] { -2, 0, 2 }; // 三个跑道的偏移值

	void Awake()
	{
		evnGenerator = Camera.main.GetComponent<EvnGenerator>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.gameState == GameState.Playing)
		{
			Vector3 tagetPos = evnGenerator.path_01.GetNextTargetPoint();
			tagetPos = new Vector3(tagetPos.x + xOffset[targetLaneIndex], tagetPos.y, tagetPos.z);
			Vector3 moveDir = tagetPos - transform.position; // 方向
			transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

			

			MoveControl();
		}
	}

	private void MoveControl()
	{
		TouchDir dir = GetTouchDir();
		if (targetLaneIndex != nowLaneIndex)
		{
			float move = moveHorizontal * horizontalMoveSpeed; // 移动的距离
			moveHorizontal -= moveHorizontal * horizontalMoveSpeed;
			this.transform.position = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
			if (Mathf.Abs(moveHorizontal) < 0.5f)
			{
				this.transform.position = new Vector3(transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
				nowLaneIndex = targetLaneIndex;
			}
		}

		if (isSliding)
		{
			slideTimer += Time.deltaTime;
			if(slideTimer > slideTime)
			{
				isSliding = false;
				slideTimer = 0;
			}
			/*float MoveY = slideSpeed * Time.deltaTime;
			if (isDown)
			{
				player.position = new Vector3(player.position.x, player.position.y - MoveY, player.position.z);
				haveSlideHeight -= MoveY;
				if (Mathf.Abs(haveSlideHeight - 0) < 0.01f)
				{
					player.position = new Vector3(player.position.x, player.position.y - (haveSlideHeight - 0), player.position.z);
					isDown = false;
					haveSlideHeight = 0;
				}
			}
			else
			{
				player.position = new Vector3(player.position.x, player.position.y + MoveY, player.position.z);
				haveSlideHeight += MoveY;
				if (Mathf.Abs(slideHeight - haveSlideHeight) < 0.01f)
				{
					player.position = new Vector3(player.position.x, player.position.y + (slideHeight - haveSlideHeight), player.position.z);
					isSliding = false;
					haveSlideHeight = slideHeight;
				}
			}*/
		}

		if (isJumping)
		{
			float MoveY = jumpSpeed * Time.deltaTime;
			if (isUp)
			{
				player.position = new Vector3(player.position.x, player.position.y + MoveY, player.position.z);
				haveHeight += MoveY;
				if (Mathf.Abs(jumpHeight - haveHeight) < 0.1f)
				{
					player.position = new Vector3(player.position.x, player.position.y + jumpHeight - haveHeight, player.position.z);
					isUp = false;
					haveHeight = jumpHeight;
				}
			}
			else
			{
				player.position = new Vector3(player.position.x, player.position.y - MoveY, player.position.z);
				haveHeight -= MoveY;
				if (Mathf.Abs(haveHeight - 0) < 0.1f)
				{
					player.position = new Vector3(player.position.x, player.position.y - (haveHeight - 0), player.position.z);
					isJumping = false;
					haveHeight = 0;
				}
			}
		}
	}

	TouchDir GetTouchDir() // 移动方向
	{
		if (Input.GetMouseButtonDown(0))
		{
			lastMouseDown = Input.mousePosition;
			return TouchDir.None;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mouseUp = Input.mousePosition;
			Vector3 touchOffset = mouseUp - lastMouseDown; // 鼠标按下和抬起的偏移
			if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x > minTouchLength) // 向右滑动
			{
				if (targetLaneIndex < 2)
				{
					targetLaneIndex++;
					moveHorizontal = 2;
				}
				return TouchDir.Right;
			}
			if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x < -minTouchLength) // 向左滑动
			{
				if (targetLaneIndex > 0)
				{
					targetLaneIndex--;
					moveHorizontal = -2;
				}
				return TouchDir.Left;
			}
			if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y > minTouchLength) // 向上滑动
			{
				if(isJumping == false)
				{
					haveHeight = 0;
					isJumping = true;
					isUp = true;
				}
				return TouchDir.Top;
			}
			if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y < -minTouchLength) // 向下滑动
			{
				//haveSlideHeight = 0;
				slideTimer = 0;
				isSliding = true;
				//isDown = true;
				return TouchDir.Bottom;
			}
		}
		return TouchDir.None;
	}
}
