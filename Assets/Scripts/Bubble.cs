using UnityEngine;
using System;

public class Bubble : MonoBehaviour {
	public enum BubbleColor {Blue, Green, Red, Purple};
	private int numBubbleColors;
	private static Sprite[] sprites;
	private const string bubbleSpriteFile = "bubbles";

	private BubbleColor color;
	
	void Start () {
		numBubbleColors = Enum.GetNames(typeof(BubbleColor)).Length;
		color = (BubbleColor) Enum.ToObject(typeof(BubbleColor), UnityEngine.Random.Range (0, numBubbleColors));
		sprites = Resources.LoadAll<Sprite> (bubbleSpriteFile);
		GetComponent<SpriteRenderer> ().sprite = sprites[(int) color];
	}
	
	void Update () {
		
	}

	public BubbleColor GetColor() {
		return color;
	}
}
