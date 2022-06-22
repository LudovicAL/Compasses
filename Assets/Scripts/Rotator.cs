using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour {

	public enum RotationMode {
		MaskableGraphicOffset,
		ImageOffset,
		MeshOffset
	}

	[SerializeField]
	private RotationMode rotationMode;
	[SerializeField]
	private float initialPosition;
	private Transform playerTransform;
	private Material material;

	// Start is called before the first frame update
	void Start() {
		playerTransform = GameObject.Find("Player").transform;
		switch (rotationMode) {
			case RotationMode.MaskableGraphicOffset:
			case RotationMode.MeshOffset:
				material = GetComponent<MaskableGraphic>().material;
				break;

			case RotationMode.ImageOffset:
				material = GetComponent<Image>().material;
				break;

			default:
				//Do nothing
				break;
		}
	}

	// Update is called once per frame
	void Update() {
		Vector2 rotation = new Vector2(playerTransform.rotation.eulerAngles.y / 360f + initialPosition, 0f);
		switch (rotationMode) {
			case RotationMode.ImageOffset:
			case RotationMode.MeshOffset:
				material.mainTextureOffset = rotation;
				break;

			case RotationMode.MaskableGraphicOffset:
				material.SetVector("_offset", rotation);
				break;

			default:
				//Do nothing
				break;
		}
	}
}
