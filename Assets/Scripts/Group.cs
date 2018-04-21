using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {

	private List<Bubble> group = new List<Bubble>();
	private int count = 0;
	
	public bool Complete() {
		return count >= 3;
	}

	public void AddToGroup(Bubble bubble) {
		// Debug.Log("added bubble to group");
		group.Add(bubble);
		count++;
		// Debug.Log("count is " + count);
		if (Complete() && !bubble.IsOriginal()) {
			foreach (Bubble b in group) {
				b.Leave();
				Game.GainPoints();
			}
		}
	}
}
