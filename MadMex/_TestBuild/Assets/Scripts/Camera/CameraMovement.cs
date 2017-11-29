using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public static CameraMovement instance;
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (this);
		}
	}
	public float mouseSensitivity = 5;
	//public float cameraDistOffset = 10;
	public float ScrollSpeed;
	
	private Camera mainCamera;
	private GameObject cameraHolder;
	private GameObject target;
	private float horizontalRotation;
	private CameraManager camManger;
	// Use this for initialization
	void Start()
	{
		camManger = CameraManager.instance;
		mainCamera = GetComponentInChildren<Camera>();
		cameraHolder = gameObject;
		ChangeCameraTarget ();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 targetInfo = target.transform.transform.position;
	    cameraHolder.transform.position = new Vector3(targetInfo.x , targetInfo.y , targetInfo.z );
		
		if (Input.GetKey(KeyCode.Mouse2))
		{
			Vector3 newRotation = new Vector3(0, horizontalRotation, 0);
			horizontalRotation += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			newRotation = new Vector3(0, horizontalRotation, 0);
			cameraHolder.transform.localRotation = Quaternion.Euler(newRotation);		
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			Vector3 newRotation = new Vector3(0, horizontalRotation, 0);
			horizontalRotation -= Input.GetAxis("Horizontal") * mouseSensitivity * Time.deltaTime	;
			newRotation = new Vector3(0, horizontalRotation, 0);
			cameraHolder.transform.localRotation = Quaternion.Euler(newRotation);		
		}	
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0  && Vector3.Distance(cameraHolder.transform.position, mainCamera.transform.position) > 5)
		{
			
			mainCamera.transform.position += mainCamera.transform.forward * Time.deltaTime * ScrollSpeed;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Distance(cameraHolder.transform.position, mainCamera.transform.position) < 15)
		{
			mainCamera.transform.position -= mainCamera.transform.forward * Time.deltaTime * ScrollSpeed;
		}
	}

	public void ChangeCameraTarget()
	{
		target = PlayerManager.currentPlayer;

	}
}