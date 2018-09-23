using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Traditional, simple camera controller class
public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    // Typical values used to control camera manipulation
    // Pitch-angle of camera, yaw-camera rotation when player turns, zoom-camera distance from player
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed = 100f;

    private float curZoom = 10f;
    private float curYaw = 0f;

    // Updates zoom when mouse wheel is used
    private void Update()
    {
        // -= will zoom, += will invert the zoom direction
        curZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        curYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = target.position - offset * curZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, curYaw);
	}
}
