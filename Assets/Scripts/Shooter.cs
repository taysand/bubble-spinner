using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	private Bubble currentBubble;
	public GameObject exit;

	// void Awake () {
	// 	transform.Rotate (new Vector3 (0, 0, 180));
	// }
	void Start () {
		GenerateBubble ();
	}

	private void Update () {
		LookAtMouse ();
		if (Input.GetMouseButtonDown (0)) {
			StartCoroutine (Shoot ());
		}
	}

	private void LookAtMouse () {
		//https://forum.unity.com/threads/2d-rotating-object-based-on-mouse-position.248167/
		Vector3 position = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 direction = Input.mousePosition - position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

	private void GenerateBubble () {
		Bubble bubble = Bubble.NewBubble (exit.transform.position);
		bubble.transform.parent = transform;
		currentBubble = bubble;
	}

	private IEnumerator Shoot () {
		Rigidbody2D rb = currentBubble.GetComponent<Rigidbody2D> ();
		if (rb != null) {
			Vector3 aimPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			aimPosition.z = 0;

			currentBubble.transform.parent = null;

			Vector2 direction = new Vector2 (aimPosition.x, 0f) - rb.position;
			direction.Normalize ();
			rb.velocity = direction * 50;

			yield return new WaitForSeconds (.3f);

			GenerateBubble ();
		} else {
			Debug.Log ("currentBubble has no Rigidbody2D");
		}
	}
}