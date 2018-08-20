using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public Transform[] points;

	void OnDrawGizmos()
	{
		iTween.DrawPath(points);
	}
}
