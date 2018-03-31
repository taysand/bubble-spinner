using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public Bubble bubblePrefab;
	public GameObject mass;
	private int numBubbles = 5;

	void Start () {
		// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		StartLevel();
	}
	
	void Update () {
		
	}

	private void StartLevel() {
		for (int i = 0; i < numBubbles; i++) {
			float theta = i * 2 * Mathf.PI / numBubbles;
			float x = Mathf.Sin(theta) * Bubble.GetDiameter();
			float y = Mathf.Cos(theta) * Bubble.GetDiameter();

			Bubble bubble = Instantiate(bubblePrefab) as Bubble;
			HingeJoint2D joint = bubble.GetComponent<HingeJoint2D>();
			joint.connectedBody = mass.GetComponent<Rigidbody2D>();
			joint.connectedAnchor = new Vector2(0, 0);
			bubble.transform.parent = mass.transform;

			bubble.transform.position = new Vector3(x, y, 0);
		}
	}
}
