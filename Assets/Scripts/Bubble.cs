﻿using UnityEngine;
using System;
using System.Collections;

public class Bubble : MonoBehaviour {
	public enum BubbleColor {Blue, Green, Red, Purple};
	private int numBubbleColors;
	private static Sprite[] bubbleSprites;
	private const string bubbleSpriteFile = "bubbles";

	private BubbleColor color;
	public bool grouped = true;
	public bool leaving = false;
	private float leaveDelay = 6f;

	private static float diameter = .85f;
	
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
