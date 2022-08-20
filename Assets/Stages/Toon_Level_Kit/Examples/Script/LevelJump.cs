using UnityEngine;
using System.Collections;

public class LevelJump : MonoBehaviour {
		
	void OnTriggerEnter(){
		Application.LoadLevel("Nightmare");
	}
}
