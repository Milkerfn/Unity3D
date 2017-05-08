
using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ModoLeituraTreinamento_Exercicios : MonoBehaviour
{

	public Transform BarraEspera;
	public Transform TextProgreso;
	public Transform TextCargando;

	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed=0;
	   
	public Text nomeObjeto, 
				objeto,
				primerNomeExercicios,
				segundoNomeExercicios,
				//primerObjetoExercicios,
			//	segundoObjetoExercicios,
				pontoCorreto, 
				pontoErrado;
	public Image imagenA;  
	public Image imagenB;  

	public static string primertext, segundoText;
	public static int ptCorreto, ptErrado;
	private DatabaseReference mDatabase;

//	public float speed = 100f;
//	public float countdown = 5.0f;
	public float tiempo = 1;
	//public string nivel;
	String[] ArrayString;

	public static int TipoEtapa = 1;
		   
	public static int numeroPalavra = 0;
	public static int cantidadeObjetos = 0;

	public bool SeguinteCarga = false;
	public bool RepetirCarga = false;
	public bool PrimeiraSelecaoCarga = false;
	public bool SegundoSelecaoCarga = false;

	public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";

	void Start ()
	{
		objeto.text = "TREINAMENTO";  

		nomeObjeto.text = " ";
		primerNomeExercicios.text = " ";
		segundoNomeExercicios.text = " ";
//		primerObjetoExercicios.text = " ";
//		segundoObjetoExercicios.text = " ";
		pontoCorreto.text = " ";
		pontoErrado.text = " ";

		StartCoroutine (Temp ());

		imagenA = GameObject.Find ("ImageA").GetComponent<Image> ();
		imagenB = GameObject.Find ("ImageB").GetComponent<Image> ();

	}




	void Update () {


		if (numeroPalavra < 1) {
			GameObject.Find ("BarraCargaRepetir").transform.localScale = new Vector3 (0, 0, 0);

			if (TipoEtapa == 1 ) {
				GameObject.Find ("PrimerObjetoSelecao").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find ("SegundoObjetoSelecao").transform.localScale = new Vector3 (0, 0, 0);

				GameObject.Find ("ImageA").transform.localScale = new Vector3 (0, 0, 0);
				GameObject.Find ("ImageB").transform.localScale = new Vector3 (0, 0, 0);
			} 

		} else {
			GameObject.Find ("BarraCargaRepetir").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);

			if(TipoEtapa == 2 ) {
				GameObject.Find ("PrimerObjetoSelecao").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);
				GameObject.Find ("SegundoObjetoSelecao").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);

				GameObject.Find ("ImageA").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);
				GameObject.Find ("ImageB").transform.localScale = new Vector3 (0.6f, 0.5f, 0f);
			}

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

		if (PrimeiraSelecaoCarga)
		{
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				validarObjeto(primertext);
				//Debug.Log(primertext);
				currentAmount = .0f;
				PrimeiraSelecaoCarga = false;
			}
			BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;
		}

		if (SegundoSelecaoCarga)
		{
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				validarObjeto(segundoText);
				//Debug.Log(segundoText);
				currentAmount = .0f;
				SegundoSelecaoCarga = false;
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

		if (numeroPalavra < cant) { 

			if (TipoEtapa == 1) {
				string ObjetoSala = ArrayString [numeroPalavra].ToString ();
				string[] ObjetoSalaSplit = ObjetoSala.Split (',');

				nomeObjeto.text = ObjetoSalaSplit [0];
				objeto.text = ObjetoSalaSplit [1];
				numeroPalavra++;
			} else if (TipoEtapa == 2) {
				objeto.text = " ";
				int R = 0;

				while (numeroPalavra == R) {
					
					Debug.Log ("Random");
					R = UnityEngine.Random.Range (0, cant);
					Debug.Log (R);

				}

				if (numeroPalavra != R) {
					string primerNomeExe = ArrayString [numeroPalavra].ToString ();
					string segundoNomeExe = ArrayString [R].ToString ();

					string[] ObjetoPrimerNomeSplit = primerNomeExe.Split (',');
					string[] ObjetoSegundoNomeSplit = segundoNomeExe.Split (',');

					primerNomeExercicios.text = ObjetoPrimerNomeSplit [0];
					//primerObjetoExercicios.text = ObjetoPrimerNomeSplit [1];

					segundoNomeExercicios.text = ObjetoSegundoNomeSplit [0];
					//segundoObjetoExercicios.text = ObjetoSegundoNomeSplit [1];

					string ObjetoA=ObjetoPrimerNomeSplit [1].Replace(" ","");
					string ObjetoB=ObjetoSegundoNomeSplit [1].Replace(" ","");

					imagenA.sprite = Resources.Load<Sprite> ("ObjModoLeitura/" + ObjetoA .ToString());
					imagenB.sprite = Resources.Load<Sprite> ("ObjModoLeitura/" + ObjetoB.ToString());

					primertext = numeroPalavra.ToString();
					segundoText = R.ToString();
				
					numeroPalavra++;
				}

			} else if (TipoEtapa == 3) {
				SceneManager.LoadScene ("ModoLeituraSala");
			}

		} else {
			
			if (TipoEtapa == 1) {
				objeto.text = "EXERCÍCIOS";
				nomeObjeto.text = " ";    
				numeroPalavra = 0;
				TipoEtapa = 2;
			} else {
				TipoEtapa = 3;
				SceneManager.LoadScene ("ModoLeituraSala");
				Debug.Log ("Novo Cenario");
			}

		}
	}

	void Repetir ()
	{
		string ObjetoSala = ArrayString [numeroPalavra-1].ToString ();
		string[] ObjetoSalaSplit = ObjetoSala.Split (',');

			nomeObjeto.text = ObjetoSalaSplit [0];
			objeto.text = ObjetoSalaSplit [1];
	}

	void validarObjeto(string valor){
		int codigoValor = int.Parse(valor); 
		int codigoPalavra = numeroPalavra - 1;

		if (codigoPalavra == codigoValor) {
			Debug.Log ("Correcto!! - Enviar puntos");
			ptCorreto++;

			currentAmount = .0f;
			PrimeiraSelecaoCarga = false;
			currentAmount = .0f;
			SegundoSelecaoCarga = false;

		} else {
			Debug.Log ("Error!! - Enviar puntos");
			ptErrado++;

			currentAmount = .0f;
			PrimeiraSelecaoCarga = false;
			currentAmount = .0f;
			SegundoSelecaoCarga = false;
		}

		pontoCorreto.text = "Correto : " + ptCorreto.ToString ();
		pontoErrado.text = "Errado : " + ptErrado.ToString ();

		Seguinte ();

	}

	#region Metodos de carga Seguinte / Repetir / PrimeiraSelecao / SegundaSelecao

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

	public void Ingreso_PrimeiraObjetoSelecao()
	{
		PrimeiraSelecaoCarga = true;
	}
	  
	public void Saida_PrimeiraObjetoSelecao()
	{
		currentAmount = .0f;
		PrimeiraSelecaoCarga = false;
	}

	public void Ingreso_SegundoObjetoSelecao()
	{
		SegundoSelecaoCarga = true;
	}

	public void Saida_SegundoObjetoSelecao()
	{
		currentAmount = .0f;
		SegundoSelecaoCarga = false;
	}
	#endregion
}
