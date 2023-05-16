using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour {
	public Transform cubeMesh;
	public bool rollForever = false; // Does the cube keep moving in a direction untill hitting a wall?
	private float rollSpeed = 400; // How fast the cube rollls
	private bool isMoving = false;
	private RaycastHit hit;
	public Vector3 pivot;
	private float cubeSize = 1; // Block cube size
	public static int steps;
	public List<GameObject> cubes;
	int index = 0;
	Camera cam;
	
	public enum CubeDirection {none, left, up, right, down};
	public CubeDirection direction = CubeDirection.none;

	Quaternion lastRotation;
	void Start() {
		// Sets number of steps to 500.
		steps = 500;
		lastRotation = Quaternion.identity;
		cubeMesh = cubes[0].transform;
		cam = FindObjectOfType<Camera>();
	}

	void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			index++;
			index %= cubes.Count;
			cubeMesh = cubes[index].transform;
			cam.GetComponent<CameraFollow>().target = cubeMesh.parent.gameObject;
			lastRotation = Quaternion.identity;
			isMoving = false;
			direction = CubeDirection.none;
			CalculatePivot();
        }
		// Listening for player input if player is currently not moving.
		if(direction == CubeDirection.none) {
			// Set the direction of movement to be equals to the corresponding
			//directional buttons.
			if (Input.GetKeyDown(KeyCode.D)) {
				direction = CubeDirection.right;
				//DeductStepCount();
			}
			else if (Input.GetKeyDown(KeyCode.A)) {
				direction = CubeDirection.left;
				//DeductStepCount();
			}
			else if (Input.GetKeyDown(KeyCode.W)) {
				direction = CubeDirection.up;
				//DeductStepCount();
			}
			else if (Input.GetKeyDown(KeyCode.S)) {
				direction = CubeDirection.down;
				//DeductStepCount();
			}
		}
		else {
			// If this is our first frame moving 
			if(!isMoving) {

				// Check if anything is blocking the way we want to move in
				if(CheckCollision(direction)) {
					isMoving = false;
					direction = CubeDirection.none;

					// If we are hitting a  PushBlock, move the block.
					if (hit.collider.gameObject.GetComponent<PushBlock>())
					{
						hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.transform.position).normalized, 1);
					}
				} else {
					CalculatePivot();
					DeductStepCount();
					isMoving = true;
				}
			}


			//Rotating the cube

			switch(direction) {
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation,cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90)
						ResetPosition();
					break;
				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > 90) {
						ResetPosition();
					}
					break;
			}
		}

		if(transform.position.y <= -10) {
			SceneManager.LoadScene("Lose");
		}
	}

	void ResetPosition() {
		// Sets the rotation and local position to 0
		// rounding the global position to a .5 value so it stays in the grid
		lastRotation = cubeMesh.transform.rotation = Quaternion.Euler(
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.x / 90f) * 90f,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.y / 90f) * 90f,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.z / 90f) * 90f
			);
		cubeMesh.parent.gameObject.transform.position = new Vector3(Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f, cubeMesh.transform.position.y, Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f);
		cubeMesh.transform.localPosition = Vector3.zero;

		// Sets isMoving to false
		isMoving = false;

		// Tereminate the movement direction, unless <rollForever> is on.	
		if (!rollForever)
			direction = CubeDirection.none;
	}

	void CalculatePivot() {
		switch(direction) {
			case CubeDirection.right:
				pivot = new Vector3(1, -1, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, -1, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, -1, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, -1, -1);
				break;
			case CubeDirection.none:
				pivot = new Vector3(0, 0, 0);
				break;
		}

		// Calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		if(GetComponent<AudioSource>())
			GetComponent<AudioSource>().Play(); // Play the flop sound 
	}

	bool CheckCollision(CubeDirection direction) {
		switch(direction) {
			case CubeDirection.right:
				Physics.Linecast(transform.position, transform.position + transform.right* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* 1, Color.black);
				break;
			case CubeDirection.left:
				Physics.Linecast(transform.position, transform.position + transform.right* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* -1, Color.black);
				break;
			case CubeDirection.up:
				Physics.Linecast(transform.position, transform.position + transform.forward* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* 1, Color.black);
				break;
			case CubeDirection.down:
				Physics.Linecast(transform.position, transform.position + transform.forward* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* -1, Color.black);
				break;
		}

		if(hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) {
			return false;
		} else {
			return true;
		}
	}

	void DeductStepCount() {
		steps -= 1;

		if(steps <= 0) {
			steps = 0;
		}
	}
}