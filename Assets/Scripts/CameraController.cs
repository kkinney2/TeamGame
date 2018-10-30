using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject cameraTarget;

    private Vector3 offset;

    void Start () {
        offset = transform.position - cameraTarget.transform.position;
    }
	
	
	void Update () {
        transform.position = cameraTarget.transform.position + offset;
    }
    void LateUpdate()
    {
        offset = transform.position - cameraTarget.transform.position;
        transform.LookAt(cameraTarget.transform.position);
    }
}
