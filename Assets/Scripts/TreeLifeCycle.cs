using UnityEngine;
using System.Collections;

public class TreeLifeCycle : Photon.MonoBehaviour {

	public float timeToDestroyAfterRelease = 30f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void countDownDestroy(){
		Invoke("PhotonSelfDestroy", timeToDestroyAfterRelease);
	}

	public void cancelDestroy(){
		CancelInvoke("PhotonSelfDestroy");
	}

	void PhotonSelfDestroy(){
		photonView.RPC("selfTreeDestroy", PhotonTargets.All, null);
	}

	[RPC]
	public void selfTreeDestroy(){
		Destroy(this.gameObject);
	}
}
