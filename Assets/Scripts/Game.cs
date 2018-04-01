using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public Bubble bubblePrefab;
	public GameObject mass;
	private int numBubbles = 1;

	void Start () {
		// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		StartLevel();
	}
	
	void Update () {
		
	}

	private void StartLevel() {
		for (int i = 0; i < numBubbles; i++) {
			Bubble bubble = Instantiate(bubblePrefab) as Bubble;
			HingeJoint2D joint = bubble.GetComponent<HingeJoint2D>();
			joint.connectedBody = mass.GetComponent<Rigidbody2D>();
			joint.connectedAnchor = new Vector2(0, 0);
			bubble.transform.parent = mass.transform;
		}
	}
}
