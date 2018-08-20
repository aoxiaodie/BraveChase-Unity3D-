using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvnGenerator : MonoBehaviour {

	public GameObject[] pathpack;

	private int pathCount = 2;

	public Path path_01;
	public Path path_02;

	// Use this for initialization
	void Start()
	{

	}

	Path GeneratePath()
	{
		pathCount++;
		float z = 400 * pathCount;
		int type = Random.Range(0, 2);
		GameObject newPath = GameObject.Instantiate(pathpack[type], new Vector3(0, 0, z), Quaternion.identity);
		return newPath.GetComponent<Path>();
	}

	public void UpadatePath()
	{
		path_01 = path_02;
		path_02 = GeneratePath();
	}
}
