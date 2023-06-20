using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Mover : MonoBehaviour {

	public enum MoveType
	{
		MoveAllways = 0,
		MoveOneShot = 1,
		DontMove = -1
	};

	public bool circle = false;
	[SerializeField][@HideInInspector]
	public List<Vector2> points;
	public float speed = 3f;
	bool gettingBack = false;
	public MoveType moveType = MoveType.MoveAllways;
	public float delayOnFinish = 1f;
	public UnityEvent onFinish;

	int currPoint = 0;

	void Start()
	{
		if (points.Count > 0)
			transform.position = points[0];
	}

	public void SetMove(int type)
	{
		moveType = (MoveType)type;
	}

	void OnDrawGizmosSelected()
	{
		if (points.Count > 0)
		{
			for (int i = 0; i < points.Count; i++)
			{
				Gizmos.DrawWireSphere(new Vector3(points[i].x, points[i].y, 0f), 0.15f);
				if (i != 0)
				{
					Gizmos.DrawLine(new Vector3(points[i].x, points[i].y, 0f), new Vector3(points[i - 1].x, points[i - 1].y, 0f));
				}
				if (i == points.Count - 1)
				{
					if(circle)
					{
						Gizmos.DrawLine(new Vector3(points[i].x, points[i].y, 0f), new Vector3(points[0].x, points[0].y, 0f));
					}
				}
			}
		}

	}
	// Update is called once per frame
	void Update () {
		if (points.Count == 0 || moveType == MoveType.DontMove)
			return;

		if (moveType == MoveType.MoveAllways || moveType == MoveType.MoveOneShot)
		{
			transform.position = Vector2.MoveTowards(transform.position, points[currPoint], speed);

			if (Vector2.Distance(transform.position, points[currPoint]) <= 0.02f)
			{
				if (gettingBack)
				{
					if (currPoint != 0)
						currPoint--;
					else
					{
						gettingBack = false;
					}
				}
				if (currPoint != points.Count - 1 && !gettingBack)
				{
					currPoint++;
				}
				else if (currPoint == points.Count - 1)
				{
					if (circle)
						currPoint = 0;
					else
					{
						gettingBack = true;
						currPoint--;
					}
				}

				if (moveType == MoveType.MoveOneShot)
					moveType = MoveType.DontMove;

				onFinish.Invoke();
			}
		}
	}
}
