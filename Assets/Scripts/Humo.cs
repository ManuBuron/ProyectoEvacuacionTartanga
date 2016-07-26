using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Humo : MonoBehaviour {
	public GameObject planta1_1;
	public GameObject planta1_2;
	public GameObject planta2_1;
	public GameObject planta2_2;
	public GameObject planta3_1;
	public GameObject planta3_2;
	public GameObject escalera1;
	public GameObject escalera2;
	public GameObject[] colider1;
	public GameObject[] colider2;
	public GameObject[] colider3;
	public GameObject[] coliderEsc1;
	public GameObject[] coliderEsc2;
	int contaCollider1;
	int contaCollider2;
	int contaCollider3;

	ParticleSystem[] particulasescalera1;
	ParticleSystem[] particulasescalera2;
	int conta;

	int contaEscalera1;
	int contaEscalera2;


	public GameObject player;
	public FirstPersonController fControl;
	public Player playerPlayer;

	// Use this for initialization
	void Start () {
		/*player=GameObject.FindWithTag("Player");
		fControl=player.GetComponent<FirstPersonController>();
		playerPlayer=player.GetComponent<Player>();
		*/

		//Humo Planta 0
		Invoke ("PonerHumoPlanta1",300);
		contaCollider1=0;
		Invoke ("ColliderPlanta1",33f);

		//Humo Planta 1
		Invoke ("PonerHumoPlanta2",545);
		contaCollider2=0;
		Invoke ("ColliderPlanta2",157f);

		//Humo Planta 3
		Invoke ("PonerHumoPlanta3",862);
		contaCollider3=0;
		Invoke ("ColliderPlanta3",350f);

		//Humo Escaleras 1
		conta=0;
		particulasescalera1=new ParticleSystem[escalera1.transform.childCount];
		foreach(Transform children in escalera1.transform){
			//Debug.Log (children);
			particulasescalera1[conta]=children.GetComponent<ParticleSystem>();
			conta+=1;
		}
		contaEscalera1=0;
		Invoke ("ensancharHumoEscalera1",40);

		//Humo Escaleras 2
		conta=0;
		particulasescalera2=new ParticleSystem[escalera2.transform.childCount];
		foreach(Transform children in escalera2.transform){
			//Debug.Log (children);
			particulasescalera2[conta]=children.GetComponent<ParticleSystem>();
			conta+=1;
		}

		contaEscalera2=0;
		Invoke ("ensancharHumoEscalera2",300);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)) {
			Time.timeScale = 1;
			//Debug.Log (Time.time);
		}

		if (Input.GetKeyDown(KeyCode.F2)) {
			Time.timeScale = 10;
			//Debug.Log (Time.time);
		}

	}

	void PonerHumoPlanta1(){
		planta1_2.SetActive(true);
		Invoke ("QuitarHumoPlanta1",10);
	}

	void QuitarHumoPlanta1(){
		planta1_1.SetActive (false);
	}

	void PonerHumoPlanta2(){
		planta2_2.SetActive(true);
		Invoke ("QuitarHumoPlanta2",10);
	}
	
	void QuitarHumoPlanta2(){
		planta2_1.SetActive (false);
	}

	void PonerHumoPlanta3(){
		planta3_2.SetActive(true);
		Invoke ("QuitarHumoPlanta3",10);
	}
	
	void QuitarHumoPlanta3(){
		planta3_1.SetActive (false);
	}

	void ColliderPlanta1(){
		colider1[contaCollider1].SetActive (true);
		contaCollider1+=1;
		if (contaCollider1<colider1.Length){
			Invoke ("ColliderPlanta1",28f);
		}

	}

	void ColliderPlanta2(){
		colider2[contaCollider2].SetActive (true);
		contaCollider2+=1;
		if (contaCollider2<colider2.Length){
			Invoke ("ColliderPlanta2",57f);
		}
		
	}

	void ColliderPlanta3(){
		colider3[contaCollider3].SetActive (true);
		if (contaCollider3<colider3.Length){
			Invoke ("ColliderPlanta3",54f);
			contaCollider3+=1;
		}
		
	}

	void ensancharHumoEscalera1(){
		particulasescalera1[contaEscalera1].startSize=3;
		particulasescalera1[contaEscalera1].gameObject.transform.position -= new Vector3(0,0.6f,0);
		if (particulasescalera1[contaEscalera1].gameObject.GetComponent<BoxCollider>()!=null){
			particulasescalera1[contaEscalera1].gameObject.GetComponent<BoxCollider>().enabled=true;
		}
		contaEscalera1+=1;
		if (contaEscalera1<particulasescalera1.Length){
			Invoke ("ensancharHumoEscalera1",40);
		}
	}

	void ensancharHumoEscalera2(){
		particulasescalera2[contaEscalera2].startSize=3;
		particulasescalera2[contaEscalera2].gameObject.transform.position -= new Vector3(0,0.50f,0);
		if (particulasescalera2[contaEscalera2].gameObject.GetComponent<BoxCollider>()!=null){
			particulasescalera2[contaEscalera2].gameObject.GetComponent<BoxCollider>().enabled=true;
		}
		contaEscalera2+=1;
		if (contaEscalera2<particulasescalera2.Length){
			Invoke ("ensancharHumoEscalera2",40);
		}
	}
}
