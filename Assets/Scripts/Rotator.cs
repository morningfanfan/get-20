using UnityEngine;
using System.Collections;


public class Rotator : MonoBehaviour
{
	
	private Vector3 targetPosition, currPosition;
	private float vary = 2f;
	private int speed = 5;
	private Vector3 velocity = Vector3.zero;

	void Start () {
		targetPosition = transform.position;
	}

	void Update ()
	{
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

		currPosition = transform.position;

		if (currPosition != targetPosition) {
			transform.position = Vector3.SmoothDamp (currPosition, targetPosition, ref velocity, speed * Time.deltaTime);
		} else {

			float angle = Vector3.Angle (currPosition, targetPosition);

			while (true) {
				
				float angleDiff = Random.Range (-180.0f, 180.0f);

				angle += angleDiff;


				float degree = angle * Mathf.PI / 180.0f;

				targetPosition = new Vector3 (
					transform.position.x + vary * Mathf.Cos (degree),
					1, 
					transform.position.z + vary * Mathf.Sin (degree)
				);

				// if the target is in the arena, break the loop
				if (-9 <= targetPosition.x && targetPosition.x <= 9 && -9 <= targetPosition.z && targetPosition.z <= 9) {
					break;
				}
			}
				
		}
	}
		


	}
		