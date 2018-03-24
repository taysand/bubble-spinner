using UnityEngine;
using System;

public class Bubble : MonoBehaviour {
	public enum BubbleColor {Blue, Green, Red, Purple};
	private int numBubbleColors;
	private static Sprite[] bubbleSprites;
	private const string bubbleSpriteFile = "bubbles";

	private BubbleColor color;
	public bool grouped = true;
	
	void Start () {
		numBubbleColors = Enum.GetNames(typeof(BubbleColor)).Length;
		color = (BubbleColor) Enum.ToObject(typeof(BubbleColor), UnityEngine.Random.Range (0, numBubbleColors));
		bubbleSprites = Resources.LoadAll<Sprite> (bubbleSpriteFile);
		GetComponent<SpriteRenderer> ().sprite = bubbleSprites[(int) color];
		// SwapGravity();
	}
	
	void Update () {
		
	}

	public BubbleColor GetColor() {
		return color;
	}

	private void SwapGravity() {
		if (grouped) {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
		} else {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		}
	}
}
