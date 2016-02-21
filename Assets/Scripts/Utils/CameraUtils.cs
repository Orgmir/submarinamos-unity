using UnityEngine;
using System.Collections;
using System;

public class CameraUtils : MonoBehaviour {

	public static Vector2 GetCameraSize() {
		var cameraCorners = GetCameraCorners();
		var cameraRightTop = cameraCorners[0];
		var cameraLeftBottom = cameraCorners[1];
		var cameraWidth = cameraRightTop.x - cameraLeftBottom.x;
		var cameraHeight = cameraRightTop.y - cameraLeftBottom.y;
		return new Vector2 (cameraWidth, cameraHeight);
	}

	public static Vector2[] GetCameraCorners() {
		var result = new Vector2[2];
		//RightTop
		result[0] = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
		//LeftBottom
		result[0] = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		return result;
	}
}