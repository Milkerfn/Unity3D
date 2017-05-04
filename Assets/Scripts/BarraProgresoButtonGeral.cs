using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraProgresoButtonGeral : MonoBehaviour {

	public Transform BarraEspera;
	public Transform TextProgreso;
	public Transform TextCargando;
	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed;

	public bool Seguinte = false;
	public bool Repetir = false;


	void Start () {
		
	}
	
	void Update () {

		if (Seguinte)
		{
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				//Seguinte ();
			}
			BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;
		}

		if (Repetir)
		{
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				//Repetir ();
			}
			BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

		}

	}

	public void Ingreso_Seguinte()
	{
		Seguinte = true;
	}

	public void Saida_Seguinte()
	{
		currentAmount = .0f;
		Seguinte = false;
	}

	public void Ingreso_Repetir()
	{
		Repetir = true;
	}

	public void Saida_Repetir()
	{
		currentAmount = .0f;
		Repetir = false;
	}
}
