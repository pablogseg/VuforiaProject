using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour {

	float speed = 55f;


	public void DoInteraction(){
		StartCoroutine(doIt());
	}

	public IEnumerator doIt(){
		float angle = 360;
		while(angle > 0){
			transform.rotation = Quaternion.Euler(0,angle,0);
			angle -= speed * Time.deltaTime;
			yield return null;
		}
	}
}
