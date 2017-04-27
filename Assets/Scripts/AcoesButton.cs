using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

   

public class AcoesButton : MonoBehaviour {

	public Text tNomeObjeto, tImagemObjeto;  
	public int iNumeroObjeto = 1002;  

	public void Seguinte(){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://palavrart-992d9.firebaseio.com/");
		object objetos = new object();

		FirebaseDatabase.DefaultInstance
			.GetReference("cenarios/1/objeto/"+ iNumeroObjeto +"/nome")
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {

				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					objetos = snapshot.Value;      

					tImagemObjeto.text = "";
					tNomeObjeto.text = "";

					tImagemObjeto.text = objetos.ToString();
					tNomeObjeto.text = objetos.ToString();
					Debug.Log (objetos); 
					iNumeroObjeto++;
				}
			});   
		
	}

	void Treinamento(){
		Debug.Log ("Treinamento");
	}
}   
