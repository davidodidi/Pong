using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour {

	public float speed = 30;

	void FixedUpdate () { //fixedupdate is used for rigid bodies

		float vertPress = Input.GetAxisRaw ("Vertical"); //used to get input from the vertical axis
		GetComponent<Rigidbody2D> ().velocity = new Vector2(0, vertPress) * speed; //used to multiply the speed and the Y axis

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
