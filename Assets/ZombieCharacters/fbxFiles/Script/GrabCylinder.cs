using UnityEngine;
using System.Collections;

public class GrabCylinder : MonoBehaviour {

	public GameObject cylinder;
	public Vector3 positionOffset;
	public Quaternion localRotation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == cylinder) {
			other.transform.position = transform.position + positionOffset;
			other.transform.localRotation = localRotation;
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject == cylinder) {
			other.transform.position = transform.position + positionOffset;
			other.transform.localRotation = localRotation;
		}
	}
}
