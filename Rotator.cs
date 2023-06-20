using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public bool alternateDirs = false;
    public bool rightFirst = true;
	public float speed = 200f;
    public float timeRotateLeft = 1f, timeRotateRight = 3f;
	public enum Direction
	{
		Left = -1,
		Right = 1
	};

	public Direction rotateDir = Direction.Right;
    private float timeSinceChange;


    void Start()
    {
        if (alternateDirs)
        {
            if (!rightFirst)
            {
                rotateDir = Direction.Left;
                timeSinceChange = timeRotateLeft;
            }


            timeSinceChange = timeRotateRight;
        }
    }

	// Update is called once per frame
	void Update () {

        if (alternateDirs)
        {
            timeSinceChange -= Time.deltaTime;

            if (timeSinceChange <= .0f)
            {
                if (rotateDir == Direction.Left)
                {
                    rotateDir = Direction.Right;
                    timeSinceChange = timeRotateRight;
                }
                else
                {
                    rotateDir = Direction.Left;
                    timeSinceChange = timeRotateLeft;
                }
            }
        }

		transform.Rotate(Vector3.forward * speed * (float)rotateDir * Time.deltaTime);
	}
}
