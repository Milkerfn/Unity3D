using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminhar : MonoBehaviour {

	//public GameObject ground;
	private bool walking = false;
	private Vector3 spawnPoint;

	// Use this for initialization
	void Start()
	{
		spawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{

		//Código para caminhar
		if (walking)
		{
			transform.position = transform.position + Camera.main.transform.forward * .35f * Time.deltaTime;
		}

		//Se sai do campo de jogo se reinicia.
		if (transform.position.y < -10f)
		{
			transform.position = spawnPoint;
		}

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
		RaycastHit hit;

		//Debug.Log(Physics.Raycast(ray, out hit));

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.name.Contains("room_6_38"))//Se ve ao chao, caminha
			{
				walking = true;
			}
			else //Se não ve ao chao, não caminha
			{
				walking = false;
			}
		}
		else {
			walking = false;
		}

	}
}

