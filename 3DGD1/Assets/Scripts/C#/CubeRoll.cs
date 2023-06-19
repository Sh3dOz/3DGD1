using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour
{
	public GameObject secondCube;
	public bool canMove = true;
	Rigidbody rb;
	public Transform cubeMesh;
	public bool rollForever = false;
	private float rollSpeed = 400;
	private bool isMoving = false;
	private RaycastHit hit;
	public Vector3 pivot;
	private float cubeSize = 1; // Block cube size
	LevelManager manager;
	public enum CubeDirection { none, left, up, right, down };
	public CubeDirection direction = CubeDirection.none;

	Quaternion lastRotation;
	bool isClimbing = false;
	CameraFollow cam;
	public GameObject moveEffect;
	void Start()
	{
		manager = FindObjectOfType<LevelManager>();
		lastRotation = Quaternion.identity;
		rb = GetComponent<Rigidbody>();
		cam = FindObjectOfType<CameraFollow>();
	}

	public float GetScale() { return cubeSize; }
	public void SetScale(float size)
	{
		cubeSize = size;
		//transform.localScale = new Vector3(size, size, size);
		ResetPosition();
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.Tab))
        {
			secondCube.GetComponent<CubeRoll>().enabled = true;
			cam.target = secondCube;
			this.enabled = false;
		}
		if (Input.GetKeyDown(KeyCode.Z)) SetScale(2);
		else if (Input.GetKeyDown(KeyCode.X)) SetScale(1);

		// If our localScale does not match the cubeSize set, then we
		// animate our cube towards the cubeSize gradually.
		if (Mathf.Abs(cubeSize - transform.localScale.x) > 0.1f)
		{
			transform.localScale = Vector3.Lerp(
				transform.localScale,
				new Vector3(cubeSize, cubeSize, cubeSize),
				Time.deltaTime * 8
			);
		}
		else transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

		if(canMove){
			if (direction == CubeDirection.none)
			{
				// If we are not moving in this frame,
				// then listen for movement input.
				if (Input.GetKeyDown(KeyCode.D))
				{
					direction = CubeDirection.right;
				}
				else if (Input.GetKeyDown(KeyCode.A))
				{
					direction = CubeDirection.left;
				}
				else if (Input.GetKeyDown(KeyCode.W))
				{
					direction = CubeDirection.up;
				}
				else if (Input.GetKeyDown(KeyCode.S))
				{
					direction = CubeDirection.down;
				}

			}
			else
			{

				// Runs the first frame after input is detected.
				if (!isMoving)
				{
					if (CheckCollision(direction))
					{
						// If a wall is blocking, then we stop moving.
						isMoving = false;

						// If a push block is in the way, then we push it.
						if (hit.collider.gameObject.GetComponent<PushBlock>())
						{
							hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.gameObject.transform.position).normalized, 1);
						}
						else
						{
							// Attempts to climb the cube in front.
							// If you want to be able to climb push blocks, you will have
							// to move this out of this block.
							if (!CheckCollision(direction, true))
							{
								CalculatePivot(true);
								AddStepCount();
								isMoving = true;
								isClimbing = true;
								return;
							}
						}
						direction = CubeDirection.none;

						return;
					}
					else
					{
						CalculatePivot();
						AddStepCount();
						isMoving = true;
					}
				}
			}
			switch (direction)
			{
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > (isClimbing ? 170 : 90))
						ResetPosition();
					break;
				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > (isClimbing ? 170 : 90))
						ResetPosition();
					break;
				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > (isClimbing ? 170 : 90))
						ResetPosition();
					break;
				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (Quaternion.Angle(lastRotation, cubeMesh.transform.rotation) > (isClimbing ? 170 : 90))
					{
						ResetPosition();
					}
					break;
			}
		}

		if (transform.position.y <= -10)
		{
			SceneManager.LoadScene("Lose");
		}
	}

	// Stops the cube after it finishes moving.
	void ResetPosition()
	{
		// Resets the rotation of the cube mesh, and moves the Player Cube
		// object to the position of the cube mesh. Finally, it centres the
		// cube mesh back on the player cube.
		//cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		lastRotation = cubeMesh.transform.rotation = Quaternion.Euler(
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.x / 90) * 90,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.y / 90) * 90,
			Mathf.Round(cubeMesh.transform.rotation.eulerAngles.z / 90) * 90
		);

		// The rounding of coords will be different for even vs. odd.
		if (cubeSize % 2 == 0)
		{
			transform.position = new Vector3(
				Mathf.Round(cubeMesh.transform.position.x),
				transform.position.y + (isClimbing ? cubeSize : 0),
				Mathf.Round(cubeMesh.transform.position.z)
			);
		}
		else
		{
			transform.position = new Vector3(
				Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f,
				transform.position.y + (isClimbing ? cubeSize : 0),
				Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f
			);
		}
		cubeMesh.transform.localPosition = Vector3.zero;

		isMoving = false; // Is moving is false so the cube doesn't continue moving.
		isClimbing = false;

		// Pushes any push block that is in the direction that we moved in.
		if (CheckCollision(direction) && hit.collider != null)
		{
			if (hit.collider.gameObject.GetComponent<PushBlock>())
			{
				hit.collider.gameObject.GetComponent<PushBlock>().Move(hit.normal, 1);
			}
		}

		if (!rollForever)
			direction = CubeDirection.none;
	}

	void CalculatePivot(bool setPivotOnTop = false)
	{

		float y = setPivotOnTop ? 1 : -1;

		switch (direction)
		{
			case CubeDirection.right:
				pivot = new Vector3(1, y, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, y, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, y, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, y, -1);
				break;
		}
		// Calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);

		if (GetComponent<AudioSource>())
			GetComponent<AudioSource>().Play(); // Play the flop sound 
			Instantiate(moveEffect, transform.position, transform.rotation);
	}

	bool CheckCollision(CubeDirection direction, bool stepUp = false)
	{

		Vector3 upOffset = stepUp ? Vector3.up * cubeSize : Vector3.zero,
				dirToMove = Vector3.zero;

		switch (direction)
		{
			case CubeDirection.right:
				dirToMove = transform.right;
				break;
			case CubeDirection.left:
				dirToMove = -transform.right;
				break;
			case CubeDirection.up:
				dirToMove = transform.forward;
				break;
			case CubeDirection.down:
				dirToMove = -transform.forward;
				break;
		}

		//Physics.Linecast(transform.position + upOffset, transform.position + dirToMove * cubeSize, out hit);
		Physics.BoxCast(transform.position + upOffset, transform.localScale * 0.4f, dirToMove, out hit, Quaternion.identity, cubeSize);
		Debug.DrawLine(transform.position + upOffset, transform.position + dirToMove * cubeSize, Color.black);

		if (hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player")))
		{
			return false;
		}
		else
		{
			print(hit.collider.name);
			return true;
		}
	}

	void AddStepCount()
	{
		manager.steps+= 1;
	}
}
