using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
	public GameObject[] obstacles;

	public float startLength = 10; // 开始位置
	public float minLength = 30;
	public float maxLength = 60;

	private Transform player;
	private WayPoint wayPoints;
	private int targetWayPointIndex;

	void Awake()
	{
		GameObject playerGo = GameObject.FindGameObjectWithTag(Tags.player);
		if (playerGo != null)
		{
			player = playerGo.transform;
		}
		wayPoints = transform.Find("wayPoints").GetComponent<WayPoint>();
		targetWayPointIndex = wayPoints.points.Length - 2;
	}

	void Start()
	{
		GenerateObstacles();
	}

	// Update is called once per frame
	/*void Update()
	{
		if (player.position.z > (transform.position.z + 20))
		{
			Camera.main.SendMessage("UpadatePath");
			GameObject.Destroy(this.gameObject);
		}
	}*/

	void GenerateObstacles()
	{
		float z = startLength;
		while (true)
		{
			float length = Random.Range(minLength, maxLength);
			z += length;
			if (z > 400) break;
			Vector3 position = GetWayPoint(z);
			int obsIndex = Random.Range(0, obstacles.Length);
			GameObject go = GameObject.Instantiate(obstacles[obsIndex], position, Quaternion.identity);
			go.transform.parent = this.transform;
		}
	}

	Vector3 GetWayPoint(float z) // 确定障碍物的坐标
	{
		Transform[] wps = wayPoints.points;
		int index = GetIndex(z);
		return Vector3.Lerp(wps[index + 1].position, wps[index].position, (z + wps[wps.Length - 1].position.z - wps[index + 1].position.z) / (wps[index].position.z - wps[index + 1].position.z));
	}

	int GetIndex(float z) // 找到waypoint（路径点）的下标
	{
		Transform[] wps = wayPoints.points;
		float startZ = wps[wps.Length - 1].position.z;
		int index = 0;
		for(int i = 0; i < wps.Length; i++)
		{
			if (wps[i].position.z - startZ >= z)
			{
				index = i;
			}
			else break;
		}
		return index;
	}

	public Vector3 GetNextTargetPoint() // 得到目标点
	{
		while (true)
		{
			if((wayPoints.points[targetWayPointIndex].position.z - player.position.z) < 2)
			{
				targetWayPointIndex--;
				if (targetWayPointIndex < 0)
				{
					targetWayPointIndex = 0;

					Camera.main.SendMessage("UpadatePath", SendMessageOptions.DontRequireReceiver);
					Destroy(this.gameObject);
					return wayPoints.points[0].position;
				}
			}
			else
			{
				return wayPoints.points[targetWayPointIndex].position;
			}
		}
		
	}
}
