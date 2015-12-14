using UnityEngine;
using System.Collections;

public class IndicatorUpdate : MonoBehaviour {

	// Not used anymore; TODO: Remove this safely
	
	public void updateText(string[] attacks, UnityEngine.UI.Text myText) {
		// Initialize string
		string newText = "";
		// Concat each attack to it
		for (int i = 0; i < attacks.Length; i++) {
			if(attacks[i] == "slam") {
				newText += "> ";
			}
			else if(attacks[i] == "slash") {
				newText += "^ ";
			}
			else {
				newText += "O ";
			}
		}
		// Apply
		myText.text = newText.Substring (0, newText.Length);
	}
}
