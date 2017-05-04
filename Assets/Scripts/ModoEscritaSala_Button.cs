
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ModoEscritaSala_Button : MonoBehaviour
{

	public Transform BarraEspera;
	public Transform TextProgreso;
	public Transform TextCargando;
	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed=0;

	public Text nomeObjeto, objeto;
	private DatabaseReference mDatabase;

//	public float speed = 100f;
//	public float countdown = 5.0f;
	public float tiempo = 1;
	//public string nivel;
	String[] ArrayString;
	   
	public static int numeroPalavra = 0;
	private int cantidadeObjetos = 0;

	public bool SeguinteCarga = false;
	public bool RepetirCarga = false;

	void Start ()
	{
		objeto.text = "TREINAMENTO";  

		nomeObjeto.text = " ";
		StartCoroutine (Temp ());

		//
	}

	void Update () {

		//isShowing = !isShowing;    
		if (numeroPalavra < 1) {
			GameObject.Find ("BarraCargaRepetir").transform.localScale = new Vector3 (0, 0, 0);

		} else {
			GameObject.Find ("BarraCargaRepetir").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);

			if (RepetirCarga)
			{
				if (currentAmount < 100)
				{
					currentAmount += speed * Time.deltaTime;
				}
				else
				{
					Repetir ();
					currentAmount = .0f;
					RepetirCarga = false;
				}
				BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;
			}
		}

		if (SeguinteCarga)
		{
			
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				Seguinte ();
				currentAmount = .0f;
				SeguinteCarga = false;
			}
			BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;
		}



	}

	IEnumerator Temp ()
	{
		yield return new WaitForSeconds (tiempo);
		//SceneManager.LoadScene(nivel);
		ObterObjetosCenario ();
	}

	void ObterObjetosCenario ()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://palavrart-992d9.firebaseio.com/");
	
		FirebaseDatabase.DefaultInstance 
			.GetReference ("cenarios/1/objeto")
			.GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {

			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				IEnumerator<DataSnapshot> en;
				en = snapshot.Children.GetEnumerator ();
				int i = 0;
				cantidadeObjetos = int.Parse(snapshot.ChildrenCount.ToString());
				ArrayString = new String[cantidadeObjetos];

				while (en.MoveNext ()) {
					DataSnapshot data = en.Current; 

					string imagen = (string)data.Child ("imagem").GetValue (true); 
					string nome = (string)data.Child ("nome").GetValue (true); 

					ArrayString [i] = (nome.ToString () + " , " + imagen.ToString ());
					i++;
				}
					  
			}     
		});   
	}

	void Seguinte ()
	{
		int cant = cantidadeObjetos;
		RepetirCarga = false;
		if (numeroPalavra < cant) {     
			string ObjetoSala = ArrayString [numeroPalavra].ToString ();
			string[] ObjetoSalaSplit = ObjetoSala.Split (',');

			nomeObjeto.text = ObjetoSalaSplit [0];
			objeto.text = ObjetoSalaSplit [1];
			numeroPalavra++;

		} else {
			objeto.text = "EXERCÍCIOS";
			nomeObjeto.text = " ";    
			numeroPalavra = 0;
		}

	}

	void Repetir ()
	{
		
		string ObjetoSala = ArrayString [numeroPalavra-1].ToString ();
			string[] ObjetoSalaSplit = ObjetoSala.Split (',');

			nomeObjeto.text = ObjetoSalaSplit [0];
			objeto.text = ObjetoSalaSplit [1];

			

	}

	#region Metodos de carga Seguinte / Repetir

	public void Ingreso_Seguinte()
	{
		SeguinteCarga = true;
	}

	public void Saida_Seguinte()
	{
		currentAmount = .0f;
		SeguinteCarga = false;
	}

	public void Ingreso_Repetir()
	{
		RepetirCarga = true;
	}

	public void Saida_Repetir()
	{
		currentAmount = .0f;
		RepetirCarga = false;
	}

	#endregion
}
