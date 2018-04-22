using System;
using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour {
	public enum BubbleColor { Blue, Green, Red, Purple };
	private static int numBubbleColors;
	private static Sprite[] bubbleSprites;
	private const string bubbleSpriteFile = "bubbles";
	private static float leaveDelay = 6f;
	private static float diameter = .86f;

	public BubbleColor color;
	private HingeJoint2D joint;
	public bool inCenter = false;
	private bool original = false;

	private bool inGroup = false;
	private Group group;

	private int bounceCount = 0;
	private static int maxCount = 6;

	void Awake () {
		joint = GetComponent<HingeJoint2D> ();
		numBubbleColors = Enum.GetNames (typeof (BubbleColor)).Length;
		color = (BubbleColor) Enum.ToObject (typeof (BubbleColor), UnityEngine.Random.Range (0, numBubbleColors));
	}

	void Start () {
		bubbleSprites = Resources.LoadAll<Sprite> (bubbleSpriteFile);
		GetComponent<SpriteRenderer> ().sprite = bubbleSprites[(int) color];
		if (!inCenter) {
			joint.enabled = false;
		}
		Game.AddToBubbleList (this);
		GravityOn (false);
	}

	public BubbleColor GetColor () {
		return color;
	}

	public void Leave () {
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<HingeJoint2D> ().enabled = false;
		GravityOn (true);
		Destroy (gameObject, 3f); //TODO: object pool
	}

	public static float GetDiameter () {
		return diameter;
	}

	void OnCollisionEnter2D (Collision2D other) {
		Bubble bubble = other.gameObject.GetComponent<Bubble> ();
		if (bubble != null) {
			AddToMass ();
			// Debug.Log("color is " + color + " and other color is " + bubble.color);
			if (bubble.color == color) {
				// Debug.Log("doing group stuff");
				if (!bubble.InGroup () && !inGroup) {
					// Debug.Log("neither has a group");
					Group g = new Group ();
					bubble.AddToGroup (g);
					AddToGroup (g);
				} else if (bubble.InGroup () && !inGroup) {
					// Debug.Log("one has a group");
					AddToGroup (bubble.group);
				} else if (!bubble.InGroup () && inGroup) {
					// Debug.Log("the other has a group");
					bubble.AddToGroup (group);
				}
			}
		} else if (other.gameObject.tag == "wall") {
			if (inCenter) {
				Game.EndGame ();
			} else if (++bounceCount >= maxCount) {
				Leave ();
				GetComponent<Rigidbody2D> ().gravityScale = 2.5f;
				Game.LoseLife ();
			}
		} else if (other.gameObject.tag == "center") {
			AddToMass ();
		}
	}

	private void AddToGroup (Group g) {
		g.AddToGroup (this);
		group = g;
		inGroup = true;
	}

	public bool InGroup () {
		return inGroup;
	}

	public void AddToMass () {
		joint.enabled = true;
		// Debug.Log("mass is " + Game.staticMass);
		// Debug.Log("setting connectedBody to " + Game.staticMass.GetComponent<Rigidbody2D> ());
		joint.connectedBody = Game.staticMass.GetComponent<Rigidbody2D> ();
		Vector3 position = transform.position;
		joint.connectedAnchor = new Vector2 (position.x, position.y);
		transform.parent = Game.staticMass.transform;
		inCenter = true;
	}

	public void SetOriginal () {
		original = true;
	}

	public bool IsOriginal () {
		return original;
	}

	public static Bubble NewBubble (Vector2 position) {
		Bubble bubble = Instantiate (Game.bubblePrefab) as Bubble;
		bubble.transform.position = new Vector3 (position.x, position.y, 0);
		return bubble;
	}

	public void GravityOn (bool value) {
		float gravityScale = 0;
		if (value) {
			gravityScale = 1f;
		}
		GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
	}
}