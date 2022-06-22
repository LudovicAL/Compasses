using UnityEngine;

public class CompassSelector : MonoBehaviour {

	[SerializeField]
	private GameObject[] compassesPanels;
	private int currentCompass = 0;

	// Start is called before the first frame update
	void Start() {
		ActivateCompassPanel();
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			SwitchToNextCompass(1);
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			SwitchToNextCompass(-1);
		}
	}

	//Switch to the next available compass
	private void SwitchToNextCompass(int modifier) {
		currentCompass += modifier;
		if (currentCompass >= compassesPanels.Length) {
			currentCompass = 0;
		} else if (currentCompass < 0) {
			currentCompass = compassesPanels.Length - 1;
		}
		ActivateCompassPanel();
	}

	//Activates only the current compass's panel and de-activate all other panels
	private void ActivateCompassPanel() {
		for (int i = 0, max = compassesPanels.Length; i < max; i++) {
			compassesPanels[i].SetActive(i == currentCompass);
		}
	}
}
