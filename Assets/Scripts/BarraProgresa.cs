using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarraProgresa : MonoBehaviour {
    public Transform BarraEspera;
    public Transform TextProgreso;
    public Transform TextCargando;
    [SerializeField]
    private float currentAmount;
    [SerializeField]
    private float speed;

    public bool ModoLeitura = false;
    public bool ModoEscrita = false;

    #region
     //if (currentAmount < 100)
     //   {
     //       currentAmount += speed * Time.deltaTime;
     //       //TextProgreso.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
     //       //TextCargando.gameObject.SetActive(true);

     //   }
     //   else
     //   {
     //       //TextCargando.gameObject.SetActive(false);
     //       //TextProgreso.GetComponent<Text>().text = "Listo!";
     //       //Application.LoadLevel("MenuCenariosLeitura");
     //       currentAmount = .0f;

     //   }
     //   BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

    #endregion

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

      

        if (ModoLeitura)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene("MenuLeitura");
              //  Application.LoadLevel("MenuLeitura");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }

        if (ModoEscrita)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
            }
            else
            {
				SceneManager.LoadScene("MenuEscrita");
              // Application.LoadLevel("MenuEscrita");
            }
            BarraEspera.GetComponent<Image>().fillAmount = currentAmount / 100;

        }
	}

    public void Ingreso_ModoLeitura()
    {
        ModoLeitura = true;
    }

    public void Saida_ModoLeitura()
    {
        currentAmount = .0f;
        ModoLeitura = false;
    }

    public void Ingreso_ModoEscrita()
    {
        ModoEscrita = true;
    }

    public void Saida_ModoEscrita()
    {
        currentAmount = .0f;
        ModoEscrita = false;
    }

}
