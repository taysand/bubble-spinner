using UnityEngine;
using System;
using System.Collections;

public class Bubble : MonoBehaviour {
	public enum BubbleColor {Blue, Green, Red, Purple};
	private int numBubbleColors;
	private static Sprite[] bubbleSprites;
	private const string bubbleSpriteFile = "bubbles";

	private BubbleColor color;
	public bool grouped = false;
	public bool leaving = false;
	private float leaveDelay = 6f;
	private HingeJoint2D joint;
	private static float diameter = .86f;

	void Awake() {
		joint = GetComponent<HingeJoint2D> ();
	}

	void Start () {
		numBubbleColors = Enum.GetNames(typeof(BubbleColor)).Length;
		color = (BubbleColor) Enum.ToObject(typeof(BubbleColor), UnityEngine.Random.Range (0, numBubbleColors));
		bubbleSprites = Resources.LoadAll<Sprite> (bubbleSpriteFile);
		GetComponent<SpriteRenderer> ().sprite = bubbleSprites[(int) color];
	}
	
	void Update () {
		if (leaving) {
			Leave();
		}
	}

	public BubbleColor GetColor() {
		return color;
	}

	private void Leave() {
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<HingeJoint2D>().enabled = false;
		leaving = false;
		Destroy(gameObject, 3f); //TODO: object pool
	}

	public static float GetDiameter() {
		return diameter;
	}
}
		leaving = false;
		Destroy (gameObject, 3f); //TODO: object pool
	}

	public static float GetDiameter () {
		return diameter;
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.GetComponent<Bubble> () != null) {
			// Debug.Log ("hit bubble");
			// Debug.Log ("grouped is " + grouped + " and connected body is " + GetComponent<HingeJoint2D> ().connectedBody);
			grouped = true;

		}
	}

	public void AddToMass () {
		joint.enabled = true;
		Debug.Log("mass is " + Game.staticMass);
		Debug.Log("setting connectedBody to " + Game.staticMass.GetComponent<Rigidbody2D> ());
		joint.connectedBody = Game.staticMass.GetComponent<Rigidbody2D> ();
		Vector3 position = transform.position;
		joint.connectedAnchor = new Vector2 (position.x, position.y);
		transform.parent = Game.staticMass.transform;
		grouped = true;
	}
}