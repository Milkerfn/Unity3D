using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BarraProgresoMenuLeitura : MonoBehaviour {

	public Transform BarraEspera;
	public Transform TextProgreso;
	public Transform TextCargando;
	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed;

	public bool Voltar = false;


	void Start () {
		
	}
	

	void Update () {
		if (Voltar)
		{
			if (currentAmount < 100)
			{
				currentAmount += speed * Time.deltaTime;
			}
			else
			{
				SceneManager.LoadScene ("MenuModo");
				// Application.LoadLevel("ModoEscritaAula");
			}
			BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

		}
	}

	public void Ingreso_Voltar()
	{
		Voltar = true;
	}

	public void Saida_Voltar()
	{
		currentAmount = .0f;
		Voltar = false;
	}
}
