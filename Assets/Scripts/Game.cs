using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public Bubble publicBubblePrefab;
	public static Bubble bubblePrefab;
	public static GameObject staticMass;
	public GameObject mass;

	private static List<Bubble> allBubbles;

	private int[] numBubblesPerRow = new int[] { 6 }; //, 11, 19 };
	private int rowCount = 0;
	private float radius;

	private static int score = 0;
	private static int level = 1;
	private const int startingPointsPerBubble = 10;

	//text
	public Text publicScoreText;
	private static Text scoreText;
	public Text levelText;
	public Text publicLivesText;
	private static Text livesText;
	public Text publicDeadText;
	private static Text deadText;

	//lives
	private static int lives = 8;
	private static int maxLives = 8;

	void Awake () {
		staticMass = mass;
		scoreText = publicScoreText;
		livesText = publicLivesText;
		publicDeadText.enabled = false;
		deadText = publicDeadText;
		allBubbles = new List<Bubble> ();
		bubblePrefab = publicBubblePrefab;
	}

	void Start () {
		// GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		radius = Bubble.GetDiameter ();
		StartLevel ();
		UpdateScoreText ();
		UpdateLevelText ();
		UpdateLivesText ();
	}

	public static void AddToBubbleList (Bubble b) {
		allBubbles.Add (b);
	}

	public static void LoseLife () {
		lives--;
		if (lives <= 0) {
			//TODO: more bubbles 
			lives = maxLives;
		}
		UpdateLivesText ();
	}

	private static void UpdateLivesText () {
		livesText.text = "Lives: " + lives;
	}

	public static void EndGame () {
		deadText.enabled = true;
		foreach (Bubble bubble in allBubbles) {
			bubble.Leave ();
		}
	}

	public static void GainPoints () {
		score += startingPointsPerBubble * level;
		UpdateScoreText ();
	}

	private static void UpdateScoreText () {
		scoreText.text = "Score: " + score;
	}

	private void UpdateLevelText () {
		levelText.text = "Level: " + level;
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

				Bubble bubble = Bubble.NewBubble (new Vector2 (x, y));
				bubble.AddToMass ();
				bubble.SetOriginal ();

			}
			rowCount++;
			radius += Bubble.GetDiameter () + .04f;
			// Debug.Log("end of game start");
		}
	}
}