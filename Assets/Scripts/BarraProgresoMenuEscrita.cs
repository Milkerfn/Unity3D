using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraProgresoMenuEscrita : MonoBehaviour {

    public Transform BarraEspera;
    public Transform TextProgreso;
    public Transform TextCargando;
    [SerializeField]
    private float currentAmount;
    [SerializeField]
    private float speed;

    public bool Sala = false;
    public bool Cozinha = false;
    public bool Cuarto = false;
    public bool Aula = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Sala)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene ("ModoEscritaSala");
                //Application.LoadLevel("ModoEscritaSala");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }

        if (Cozinha)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene ("ModoEscritaSala");
              //  Application.LoadLevel("ModoEscritaCozinha");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }

        if (Cuarto)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene ("ModoEscritaSala");
                //Application.LoadLevel("ModoEscritaCuarto");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }

        if (Aula)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene ("ModoEscritaSala");
               // Application.LoadLevel("ModoEscritaAula");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }
	}

    public void Ingreso_Sala()
    {
        Sala = true;
    }

    public void Saida_Sala()
    {
        currentAmount = .0f;
        Sala = false;
    }

    public void Ingreso_Cozinha()
    {
        Cozinha = true;
    }

    public void Saida_Cozinha()
    {
        currentAmount = .0f;
        Cozinha = false;
    }

    public void Ingreso_Cuarto()
    {
        Cuarto = true;
    }

    public void Saida_Cuarto()
    {
        currentAmount = .0f;
        Cuarto = false;
    }

    public void Ingreso_Aula()
    {
        Aula = true;
    }

    public void Saida_Aula()
    {
        currentAmount = .0f;
        Aula = false;
    }
}
