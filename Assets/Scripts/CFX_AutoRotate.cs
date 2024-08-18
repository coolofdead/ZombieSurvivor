using UnityEngine;
using System.Collections;

// Cartoon FX  - (c) 2015 Jean Moreno

// Indefinitely rotates an object at a constant speed

public class CFX_AutoRotate : MonoBehaviour
{
	// Rotation speed & axis
	public Vector3 rotation;
	public bool moveByStep;
	public float delayBetweenStep = 1;

	private float lastStep;
	
	// Rotation space
	public Space space = Space.Self;
	
	void Update()
	{
		if (moveByStep)
        {
			if (lastStep + delayBetweenStep > Time.time) return;

			lastStep = Time.time;
			transform.Rotate(rotation, space);
		}
		else
        {
			transform.Rotate(rotation * Time.deltaTime, space);
        }
	}
}
