using UnityEngine;
using UnityEngine.UI;

public class CompassMesh : MaskableGraphic {
	[SerializeField]
	private float thickness;
	[SerializeField]
	private float corner;
	[SerializeField]
	Texture m_Texture;

	//Make it such that Unity will trigger our UI element to redraw whenever we change the texture in the Inspector
	public Texture texture {
		get {
			return m_Texture;
		}

		set {
			if (m_Texture == value) {
				return;
			}
			m_Texture = value;
			SetVerticesDirty();
			SetMaterialDirty();
		}
	}

	//Method called when the tranform's dimensions are changed
	protected override void OnRectTransformDimensionsChange() {
		base.OnRectTransformDimensionsChange();
		SetVerticesDirty();
		SetMaterialDirty();
	}

	//Method called when the object's texture is to be set
	public override Texture mainTexture {
		get {
			return m_Texture == null ? s_WhiteTexture : m_Texture;
		}
	}

	//Method called when the object's mesh is to be constructed
	protected override void OnPopulateMesh(VertexHelper vh) {
		vh.Clear();
		float halfWidth = rectTransform.rect.width / 2f;
		float halfHeight = rectTransform.rect.height / 2f;
		float widthSqr = thickness * thickness;
		float distanceSqr = widthSqr / 2f;
		float distance = Mathf.Sqrt(distanceSqr);
		UIVertex vertex = UIVertex.simpleVert;
		vertex.color = color;

		float interiorCornerLength = Mathf.Sqrt(Mathf.Pow(distance + corner, 2f) + Mathf.Pow(distance + corner, 2f));
		float verticalSideLength = rectTransform.rect.height - (distance * 2f) - corner;
		float horizontalSideLength = rectTransform.rect.width - (distance * 2f) - corner;

		float circleLength = verticalSideLength * 2f + horizontalSideLength * 2f + interiorCornerLength * 4f;

		float cornerProportion = interiorCornerLength / circleLength;
		float verticalSideProportion = verticalSideLength / circleLength;
		float horizontalSideProportion = horizontalSideLength / circleLength;

		//Box corners:
		//	4			6			8			9
		//				5			7
		//	3		2					10		11
		//
		//	1/21	0/20				12		13
		//				17			15
		//	19			18			16			14

		//0
		vertex.position = new Vector3(-halfWidth + distance, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(0f, 0f);
		vh.AddVert(vertex);
		//1
		vertex.position = new Vector3(-halfWidth, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(0f, 1f);
		vh.AddVert(vertex);
		//2
		vertex.position = new Vector3(-halfWidth + distance, halfHeight - distance - corner);
		vertex.uv0 = new Vector2(verticalSideProportion, 0f);
		vh.AddVert(vertex);
		//3
		vertex.position = new Vector3(-halfWidth, halfHeight - distance - corner);
		vertex.uv0 = new Vector2(verticalSideProportion, 1f);
		vh.AddVert(vertex);
		//4
		vertex.position = new Vector3(-halfWidth, halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion + (cornerProportion / 2f), 1f);
		vh.AddVert(vertex);
		//5
		vertex.position = new Vector3(-halfWidth + distance + corner, halfHeight - distance);
		vertex.uv0 = new Vector2(verticalSideProportion + cornerProportion, 0f);
		vh.AddVert(vertex);
		//6
		vertex.position = new Vector3(-halfWidth + distance + corner, halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion + (cornerProportion / 2f) * 2, 1f);
		vh.AddVert(vertex);
		//7
		vertex.position = new Vector3(halfWidth - distance - corner, halfHeight - distance);
		vertex.uv0 = new Vector2(verticalSideProportion + cornerProportion + horizontalSideProportion, 0f);
		vh.AddVert(vertex);
		//8
		vertex.position = new Vector3(halfWidth - distance - corner, halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion + (cornerProportion / 2f) * 2 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//9
		vertex.position = new Vector3(halfWidth, halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion + (cornerProportion / 2f) * 3 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//10
		vertex.position = new Vector3(halfWidth - distance, halfHeight - distance - corner);
		vertex.uv0 = new Vector2(verticalSideProportion + cornerProportion * 2 + horizontalSideProportion, 0f);
		vh.AddVert(vertex);
		//11
		vertex.position = new Vector3(halfWidth, halfHeight - distance - corner);
		vertex.uv0 = new Vector2(verticalSideProportion + (cornerProportion / 2f) * 4 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//12
		vertex.position = new Vector3(halfWidth - distance, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + cornerProportion * 2 + horizontalSideProportion, 0f);
		vh.AddVert(vertex);
		//13
		vertex.position = new Vector3(halfWidth, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 4 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//14
		vertex.position = new Vector3(halfWidth, -halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 5 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//15
		vertex.position = new Vector3(halfWidth - distance - corner, -halfHeight + distance);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + cornerProportion * 3 + horizontalSideProportion, 0f);
		vh.AddVert(vertex);
		//16
		vertex.position = new Vector3(halfWidth - distance - corner, -halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 6 + horizontalSideProportion, 1f);
		vh.AddVert(vertex);
		//17
		vertex.position = new Vector3(-halfWidth + distance + corner, -halfHeight + distance);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + cornerProportion * 3 + horizontalSideProportion * 2, 0f);
		vh.AddVert(vertex);
		//18
		vertex.position = new Vector3(-halfWidth + distance + corner, -halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 6 + horizontalSideProportion * 2, 1f);
		vh.AddVert(vertex);
		//19
		vertex.position = new Vector3(-halfWidth, -halfHeight);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 7 + horizontalSideProportion * 2, 1f);
		vh.AddVert(vertex);
		//20
		vertex.position = new Vector3(-halfWidth + distance, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + cornerProportion * 4 + horizontalSideProportion * 2, 0f);
		vh.AddVert(vertex);
		//21
		vertex.position = new Vector3(-halfWidth, -halfHeight + distance + corner);
		vertex.uv0 = new Vector2(verticalSideProportion * 2 + (cornerProportion / 2f) * 8 + horizontalSideProportion * 2, 1f);
		vh.AddVert(vertex);

		vh.AddTriangle(0, 1, 3);
		vh.AddTriangle(0, 3, 2);
		vh.AddTriangle(2, 3, 4);
		vh.AddTriangle(2, 4, 5);
		vh.AddTriangle(5, 4, 6);
		vh.AddTriangle(5, 6, 7);
		vh.AddTriangle(6, 8, 7);
		vh.AddTriangle(7, 8, 9);
		vh.AddTriangle(7, 9, 10);
		vh.AddTriangle(10, 9, 11);
		vh.AddTriangle(10, 11, 12);
		vh.AddTriangle(11, 13, 12);
		vh.AddTriangle(12, 13, 14);
		vh.AddTriangle(12, 14, 15);
		vh.AddTriangle(15, 14, 16);
		vh.AddTriangle(15, 16, 17);
		vh.AddTriangle(16, 18, 17);
		vh.AddTriangle(17, 18, 19);
		vh.AddTriangle(17, 19, 20);
		vh.AddTriangle(20, 19, 21);
	}
}
