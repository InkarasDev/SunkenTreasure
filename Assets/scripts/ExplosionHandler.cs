using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {

	private IEnumerator KillOnAnimationEnd() {
         yield return new WaitForSeconds (0.6f);
         Destroy (gameObject);
     }
 
     void Update () {
         StartCoroutine (KillOnAnimationEnd ());
     }
}
