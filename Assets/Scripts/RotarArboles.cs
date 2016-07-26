using UnityEngine;
using System.Collections;

public class RotarArboles : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (new Vector3(player.transform.position.x - transform.position.x,0,player.transform.position.z - transform.position.z));
	}
}
