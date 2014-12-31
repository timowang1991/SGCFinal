using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabScript : MonoBehaviour {

	public float speedValveToThrow = 350.0f;

	public float ratio = 2.0f;

	public int maxNumOfObjectsOnHand = 1;

	string grabbableLayerName = "Grabbable";

	List<Collider> colliderList = new List<Collider>();

	List<Vector3> positions = new List<Vector3> ();

	Vector3 displacement;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		recordPosition ();
		computeVelocity ();
		throwObjects ();
	}

	void recordPosition(){
		positions.Add (transform.position);
		if (positions.Count > 10) {
			positions.RemoveAt(0);
		}
	}

	void computeVelocity(){
		displacement = transform.position - positions[0];
//		Debug.Log ("displacement : " + displacement);
//		Debug.Log ("displacement magnitude : " + displacement.magnitude);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("OnTriggerEnter : object" + other.gameObject.name);
		if (other.gameObject.layer == LayerMask.NameToLayer (grabbableLayerName) &&
		    colliderList.Count < maxNumOfObjectsOnHand) {
			Debug.Log("OnTriggerEnter : " + other.gameObject.name + "after if");
			colliderList.Add (other);
			other.transform.parent = this.transform;
			other.rigidbody.isKinematic = true;
			other.rigidbody.useGravity = false;
		}
	}
	
	void OnTriggerExit(Collider other){
		if(!colliderList.Contains(other)){
			return;
		}

		if(other.gameObject.layer == LayerMask.NameToLayer(grabbableLayerName)){
			colliderList.Remove (other);
			other.transform.parent = null;
			other.rigidbody.useGravity = true;
			other.rigidbody.isKinematic = false;
		}
	}

	void throwObjects(){
		if (colliderList.Count == 0)
			return;

		if(displacement.magnitude >= speedValveToThrow &&
		   displacement.y < 0 &&
		   displacement.z < 0){
			foreach(Collider collider in colliderList){
				collider.transform.parent = null;
				collider.rigidbody.useGravity = true;
				collider.rigidbody.isKinematic = false;
				collider.rigidbody.velocity = displacement * ratio;
			}
		}
	}
}
