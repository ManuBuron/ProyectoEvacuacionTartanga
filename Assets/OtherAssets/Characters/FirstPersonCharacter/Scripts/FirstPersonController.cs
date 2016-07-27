using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[RequireComponent(typeof (CharacterController))]
	[RequireComponent(typeof (AudioSource))]
	public class FirstPersonController : MonoBehaviour
	{
		[SerializeField] private bool m_IsWalking;
		[SerializeField] private float m_WalkSpeed;
		[SerializeField] private float m_RunSpeed;
		[SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
		[SerializeField] private float m_StickToGroundForce;
		[SerializeField] private float m_GravityMultiplier;
		[SerializeField] public MouseLook m_MouseLook;
		[SerializeField] private bool m_UseFovKick;
		[SerializeField] private FOVKick m_FovKick = new FOVKick();
		[SerializeField] private bool m_UseHeadBob;
		[SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
		[SerializeField] private float m_StepInterval;
		[SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
		[SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

		private Camera m_Camera;
		private float m_YRotation;
		private Vector2 m_Input;
		private Vector3 m_MoveDir = Vector3.zero;
		private CharacterController m_CharacterController;
		private CollisionFlags m_CollisionFlags;
		private bool m_PreviouslyGrounded;
		private Vector3 m_OriginalCameraPosition;
		private float m_StepCycle;
		private float m_NextStep;
		private AudioSource m_AudioSource;

		// agachado o de pie
		public bool estadoAgachado;
		float alturaAgachado;
		float alturaLevantado;
		float walkSpeedLevantado;
		float walkSpeedAgachado;
		public float diferenciaAltura;
		public bool agacharse;
		bool agachandoLevantando;

		//segundos corriendo
		float runTimeF;
		public int runTimeInt;
		public Text textoCorriendo;
		public GameObject canvasC;

		public bool fijarCamara;
		public Transform viewPerson;//se le pasa el del objeto con el que se habla

		// Use this for initialization
		private void Start()
		{
			canvasC = GameObject.FindGameObjectWithTag ("canvas");
			estadoAgachado = false;
			alturaLevantado = transform.GetComponent<CharacterController> ().height;
			alturaAgachado = alturaLevantado - diferenciaAltura;
			walkSpeedLevantado = transform.GetComponent<FirstPersonController> ().m_WalkSpeed;
			walkSpeedAgachado = walkSpeedLevantado - 3;

			m_CharacterController = GetComponent<CharacterController>();
			m_Camera = Camera.main;
			m_OriginalCameraPosition = m_Camera.transform.localPosition;
			m_FovKick.Setup(m_Camera);
 				m_HeadBob.Setup(m_Camera, m_StepInterval);
			m_StepCycle = 0f;
			m_NextStep = m_StepCycle/2f;
			m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
		}


		// Update is called once per frame
		private void Update()
		{
			RotateView();

			if (!m_CharacterController.isGrounded &&  m_PreviouslyGrounded)
			{
				m_MoveDir.y = 0f;
			}

			m_PreviouslyGrounded = m_CharacterController.isGrounded;

			//Agacharse
			if (Input.GetMouseButtonDown(2)){
				Debug.Log ("Boton");
				//AgacharseLevantarse ();
				agachandoLevantando=true;
				if (agacharse){
					Debug.Log ("Agachar");
					agacharse=false;
					canvasC.GetComponent<CanvasController> ().ChangePlayerState(true);
				}else{
					Debug.Log ("Levantar");
					agacharse=true;
					canvasC.GetComponent<CanvasController> ().ChangePlayerState(false);

				}
			}
			if (agachandoLevantando){
				if (agacharse){
					m_CharacterController.height = Mathf.Lerp(m_CharacterController.height,alturaAgachado,Time.deltaTime*3);
					//transform.position=new Vector3(transform.position.x,Mathf.Lerp(transform.position.y,(transform.position.y-alturaLevantado)+alturaAgachado/2,Time.deltaTime*3),transform.position.z);
					if (m_CharacterController.height<alturaAgachado+0.1f){
						m_CharacterController.height=alturaAgachado;
						agachandoLevantando=false;
					}
				}else{
					m_CharacterController.height = Mathf.Lerp(m_CharacterController.height,alturaLevantado,Time.deltaTime*2);
					transform.position=new Vector3(transform.position.x,Mathf.Lerp(transform.position.y,(transform.position.y-alturaAgachado)+alturaLevantado/2,Time.deltaTime*3),transform.position.z);
					if (m_CharacterController.height>alturaLevantado-0.1f){
						m_CharacterController.height=alturaLevantado;
						agachandoLevantando=false;
					}
				}
			}
		}


		private void PlayLandingSound()
		{
			m_AudioSource.clip = m_LandSound;
			m_AudioSource.Play();
			m_NextStep = m_StepCycle + .5f;
		}


		private void FixedUpdate()
		{
			float speed;
			GetInput(out speed);
			// always move along the camera forward as it is the direction that it being aimed at
			Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

			// get a normal for the surface that is being touched to move along it
			RaycastHit hitInfo;
			Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
				m_CharacterController.height/2f, ~0, QueryTriggerInteraction.Ignore);
			desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

			m_MoveDir.x = desiredMove.x*speed;
			m_MoveDir.z = desiredMove.z*speed;

			//Saber si esta corriendo
			if (speed == m_RunSpeed && (m_Input.x != 0 || m_Input.y != 0)) {
				runTimeF = runTimeF + Time.deltaTime;
				runTimeInt = (int)runTimeF;
				//Debug.Log ("Esta Corriendo" + runTimeInt);
				//textoCorriendo.text = "Run Seg. " + runTimeInt;
			} 




			if (m_CharacterController.isGrounded)
			{
				m_MoveDir.y = -m_StickToGroundForce;

			}
			else
			{
				m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
			}
			m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

			ProgressStepCycle(speed);
			UpdateCameraPosition(speed);

			m_MouseLook.UpdateCursorLock();
		}


		private void ProgressStepCycle(float speed)
		{
			if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
			{
				m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
					Time.fixedDeltaTime;
			}

			if (!(m_StepCycle > m_NextStep))
			{
				return;
			}

			m_NextStep = m_StepCycle + m_StepInterval;

			PlayFootStepAudio();
		}


		private void PlayFootStepAudio()
		{
			if (!m_CharacterController.isGrounded)
			{
				return;
			}
			// pick & play a random footstep sound from the array,
			// excluding sound at index 0
			int n = Random.Range(1, m_FootstepSounds.Length);
			m_AudioSource.clip = m_FootstepSounds[n];
			m_AudioSource.PlayOneShot(m_AudioSource.clip);
			// move picked sound to index 0 so it's not picked next time
			m_FootstepSounds[n] = m_FootstepSounds[0];
			m_FootstepSounds[0] = m_AudioSource.clip;
		}


		private void UpdateCameraPosition(float speed)
		{
			Vector3 newCameraPosition;
			if (!m_UseHeadBob)
			{
				return;
			}
			if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
			{
				m_Camera.transform.localPosition =
					m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
						(speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
				newCameraPosition = m_Camera.transform.localPosition;
			}
			else
			{
				newCameraPosition = m_Camera.transform.localPosition;
			}
			m_Camera.transform.localPosition = newCameraPosition;
		}


		private void GetInput(out float speed)
		{
			// Read input
			float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
			float vertical = CrossPlatformInputManager.GetAxis("Vertical");

			bool waswalking = m_IsWalking;

			#if !MOBILE_INPUT
			// On standalone builds, walk/run speed is modified by a key press.
			// keep track of whether or not the character is walking or running
			//m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
			m_IsWalking = !Input.GetMouseButton(1);
			#endif
			// set the desired speed to be walking or running
			speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;

			m_Input = new Vector2(horizontal, vertical);

			// normalize input if it exceeds 1 in combined length:
			if (m_Input.sqrMagnitude > 1)
			{
				m_Input.Normalize();
			}

			// handle speed change to give an fov kick
			// only if the player is going to a run, is running and the fovkick is to be used
			if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
			{
				StopAllCoroutines();
				StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
			}
		}


		private void RotateView()
		{
			if (fijarCamara){
				//Debug.Log ("person= " + viewPerson.position);
				m_MouseLook.LookPerson(transform, m_Camera.transform, viewPerson);
			}else{
				m_MouseLook.LookRotation (transform, m_Camera.transform);
			}
		}


		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody body = hit.collider.attachedRigidbody;
			//dont move the rigidbody if the character is on top of it
			if (m_CollisionFlags == CollisionFlags.Below)
			{
				return;
			}

			if (body == null || body.isKinematic)
			{
				return;
			}
			body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
		}

		void AgacharseLevantarse(){
			canvasC.GetComponent<CanvasController> ().ChangePlayerState(estadoAgachado);
			/*if (estadoAgachado) {
				estadoAgachado = false;
				transform.GetComponent<CharacterController> ().height = alturaLevantado;
				//transform.GetChild(0).localPosition=new Vector3(0f, (transform.GetChild(0).localPosition.y-diferenciaAltura),0f);
				transform.GetComponent<FirstPersonController> ().m_WalkSpeed = walkSpeedLevantado;
			} else {
				estadoAgachado = true;
				transform.GetComponent<CharacterController> ().height = alturaAgachado;
				transform.GetComponent<FirstPersonController> ().m_WalkSpeed = walkSpeedAgachado;
				//transform.GetChild(0).localPosition=new Vector3(0f,(transform.GetChild(0).localPosition.y-diferenciaAltura),0f);
				Debug.Log ((alturaAgachado/2) - 0.15f);
			}*/
		}

	}
}