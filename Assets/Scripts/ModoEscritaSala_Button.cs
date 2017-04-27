
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;



public class ModoEscritaSala_Button : MonoBehaviour {
	
	public Text nomeObjeto, objeto;   
	private DatabaseReference mDatabase;

	public float speed = 100f;
	public float countdown = 5.0f;
	public float tiempo = 1;
	public string nivel;


	void Start () {
		objeto.text = "INTRODUTION";  
		nomeObjeto.text = " ";
		StartCoroutine(Temp());
	}
		
	IEnumerator Temp(){
		yield return new WaitForSeconds(tiempo);
		//SceneManager.LoadScene(nivel);
		inicio();
	}

	void inicio(){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://palavrart-992d9.firebaseio.com/");

		object cenarios = new object();
		var numeroObjeto2 = "1001";

		FirebaseDatabase.DefaultInstance 
			.GetReference("cenarios/1/objeto/"+ numeroObjeto2 +"/nome")
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {

				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					cenarios = snapshot.Value;      
					//.objeto.value(1001).nome
					objeto.text = cenarios.ToString();
					nomeObjeto.text = cenarios.ToString();
					Debug.Log (cenarios); 
				}
			});   
	}

}
