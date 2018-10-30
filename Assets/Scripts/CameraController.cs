using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    private GameObject cameraTarget;
    private Vector3 offset;

    void Start () {
        if (this.CompareTag("Camera1")){
            cameraTarget = player1;
        }
        if (this.CompareTag("Camera2")){
            cameraTarget = player2;
        }
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
