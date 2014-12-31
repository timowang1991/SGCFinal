using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public string flammableLayerName = "Flammable";
	public float stayBurningTime;

	public string terrainTagName;
	public GameObject groundFire;

	bool isOnFire;
	float onFireTimer;

	// Use this for initialization
	void Start () {
		onFireTimer = 0.0f;
		isOnFire = particleSystem.playOnAwake;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOnFire) {
			onFireTimer += Time.deltaTime;

			if(onFireTimer < stayBurningTime){
				fixRotation();
			} else{
				particleSystem.Stop();
				isOnFire = false;
			}
		}
	}

	void fixRotation(){
		Quaternion target = Quaternion.Euler (-90.0f, 0, 0);
		transform.rotation = target;
	}

	void OnTriggerEnter(Collider other){
		if(isOnFire){
			OnParticleCollision (other.gameObject);
		}
	}

	void OnParticleCollision(GameObject other){
		if(other.layer == LayerMask.NameToLayer(flammableLayerName)){
			Fire otherFire = other.GetComponentInChildren<Fire>();
			if(otherFire == null)
				return;
			otherFire.caughtFire();
		}

		if(other.tag == terrainTagName){
			GameObject fireOnGround = GameObject.Instantiate(groundFire) as GameObject;
			Vector3 groundFirePosition = new Vector3(transform.position.x,
			                                         40,
			                                         transform.position.z);
			fireOnGround.transform.position = groundFirePosition;
		}
	}

	// called by other Fire objects
	public void caughtFire(){
		if(isOnFire){
			onFireTimer = 0.0f;
			return;
		}

		isOnFire = true;
		onFireTimer = 0.0f;
		particleSystem.Play ();
	}
}
