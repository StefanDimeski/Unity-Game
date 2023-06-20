using UnityEngine;
using System.Collections;
using System;

public class Gate : MonoBehaviour {

	public int keysToOpen = 1;
	public float speed = 0.3f;
	public bool closeInTime = false;
	public float closeTime = 0f;
	public bool updatePos = true;
	public bool left = true;
	public bool openSpace;
	public int spaceCount;
	int spaceTmp = 1;

	int currKeys;

	// Use this for initialization
	void Start () {
		currKeys = keysToOpen;
	}

	public void SpaceOnCollision()
	{
		if (!openSpace)
			return;

		Vector3 localScale = transform.localScale;
		float times = 0.31f / spaceCount;
		if (spaceTmp == spaceCount)
		{
			localScale.x = 0.69f;
		}
		else
		{
			Debug.Log(times);
			localScale.x = 1f - (spaceTmp * times);
			spaceTmp++;
		}
		
		transform.localScale = localScale;
	}
	
	public void OpenGate()
	{
		if (currKeys > 0)
			currKeys--;

		if (currKeys > 0)
		{
			return;
		}
		else
		{
			StartCoroutine("OpenGraphics");

			if (closeInTime)
				Invoke("CloseGate", closeTime);
		}
	}

	public void CloseGate()
	{
		StartCoroutine("CloseGraphics");
	}

	IEnumerator OpenGraphics()
	{
		Vector3 scale = transform.localScale;

		while(true)
		{
			scale.x = Mathf.MoveTowards(scale.x, 0, speed * Time.deltaTime);

			if (scale.x < 0.01f)
			{
				scale.x = 0f;
				transform.localScale = scale;

				break;
			}

			transform.localScale = scale;

			yield return null;
		}
	}

	IEnumerator CloseGraphics()
	{
		Vector3 scale = transform.localScale;

		while(true)
		{
			scale.x = Mathf.MoveTowards(scale.x, 1f, speed * Time.deltaTime);

			if (scale.x > 1f)
			{
				scale.x = 1f;

				break;
			}

			transform.localScale = scale;

			yield return null;
		}
	}

	void OnDrawGizmosSelected()
	{
		if (updatePos)
		{
			Vector2 direction = Vector2.zero;

			if (left)
				direction = new Vector2(1f, 0f);
			else 
			{
				direction = new Vector2(-1f, 0f);
			}

			Transform child = transform.GetChild(0).GetComponent<Transform>();

			child.localPosition = direction * (child.localScale.x / 2f);

			updatePos = false;
		}
	}
}
