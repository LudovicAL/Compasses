using UnityEngine;

public class Zoomer : MonoBehaviour {
	[SerializeField]
	private float delay;
	[SerializeField]
	private float zoomSpeed;
	private float countDownStart;
	private Transform imageProgressBar;

	void Awake() {
		imageProgressBar = transform.Find("ImageProgressBar");
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			Initialize();
		}
		if (countDownStart + delay < Time.time) {
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, zoomSpeed * Time.deltaTime);
		} else {
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, zoomSpeed * Time.deltaTime);
			imageProgressBar.localScale = Vector3.MoveTowards(imageProgressBar.localScale, Vector3.one, (1f / delay) * Time.deltaTime);
		}
	}

	void OnEnable() {
		Initialize();
	}

	public void Initialize() {
		countDownStart = Time.time;
		transform.localScale = Vector3.zero;
		imageProgressBar.localScale = new Vector3(0f, 1f, 1f);
	}
}
