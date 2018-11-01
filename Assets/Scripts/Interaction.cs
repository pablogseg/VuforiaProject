using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	void Update () {
		if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {            
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position); 
			ShootRay(ray);
        }
	}

	void ShootRay (Ray ray){  
		RaycastHit rhit;
		GameObject target;
		if (Physics.Raycast (ray, out rhit, 1000.0f)) {
			Debug.Log("Ray Shot and hit!");
			target = rhit.collider.gameObject;
			if(target != null){
				target.GetComponent<DoSomething>().DoInteraction();
			}
		}
	}
}





