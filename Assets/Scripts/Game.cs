using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public Bubble bubblePrefab;
	public GameObject mass;

	private int[] numBubblesPerRow = new int[] { 6, 12, 19 };
	private int rowCount = 0;
	private float radius;

	void Start () {
		// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		radius = Bubble.GetDiameter ();
		StartLevel ();
	}

	void Update () {

	}

	private void StartLevel () {
		while (rowCount < numBubblesPerRow.Length) {
			for (int i = 0; i < numBubblesPerRow[rowCount]; i++) {
				//https://www.reddit.com/r/Unity2D/comments/2rrapx/instantiating_multiple_objects_in_a_circle/
				float theta = i * 2 * Mathf.PI / numBubblesPerRow[rowCount];
				float x = Mathf.Sin (theta) * radius;
				float y = Mathf.Cos (theta) * radius;

				Bubble bubble = Instantiate (bubblePrefab) as Bubble;
				HingeJoint2D joint = bubble.GetComponent<HingeJoint2D> ();
				joint.connectedBody = mass.GetComponent<Rigidbody2D> ();
				joint.connectedAnchor = new Vector2 (0, 0);
				bubble.transform.parent = mass.transform;

				bubble.transform.position = new Vector3 (x, y, 0);
			}
			rowCount++;
			radius += Bubble.GetDiameter ();
		}
	}
}