using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour
{
	private Animator anim;
	private Camera cam;
	private CharacterController controller;

	private Vector3 desiredMoveDirection;
	private Vector3 moveVector;

	public Vector2 moveAxis;
	private float verticalVel;

	[Header("Settings")]
	[SerializeField] float movementSpeed;
	[SerializeField] float rotationSpeed = 0.1f;
	[SerializeField] float fallSpeed = 0.2f;
	public float acceleration = 1;

	[Header("Booleans")]
	[SerializeField] bool blockRotationPlayer;
	private bool isGrounded;

    [Header("Dash Settings")]
    public float dashSpeed = 40f;          // 衝刺速度
    public float dashDuration = 0.2f;      // 衝刺持續時間
    [SerializeField] private float dashTime; // 衝刺計時器
    private bool isDashing;                // 是否在衝刺
    private Vector3 dashDirection;         // 衝刺方向

    void Start()
	{
		anim = this.GetComponent<Animator>();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController>();
	}

	void Update()
	{
		InputMagnitude();

		isGrounded = controller.isGrounded;

		if (isGrounded)
			verticalVel -= 0;
		else
			verticalVel -= 1;

		moveVector = new Vector3(0, verticalVel * fallSpeed * Time.deltaTime, 0);
		controller.Move(moveVector);

        // 检查是否按下滑鼠右键
        if (Input.GetMouseButtonDown(1) && !isDashing)
        {
            if (moveAxis.y > 0 || moveAxis.x != 0 || moveAxis.y < 0) // 修改此處，只要角色正在移動就朝向當前方向衝刺
            {
                StartDash(true); // 前進衝刺
            }
            else
            {
                StartDash(false); // 後退衝刺
            }
        }

        // 更新衝刺計時器
        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                EndDash();
            }
            else
            {
                Vector3 dashMovement = dashDirection * dashSpeed * Time.fixedDeltaTime;
                controller.Move(dashMovement);
            }
        }
        else
        {
            InputMagnitude();
        }
    }

	void PlayerMoveAndRotation()
	{
		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		desiredMoveDirection = forward * moveAxis.y + right * moveAxis.x;

		if (blockRotationPlayer == false)
		{
			//Camera
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed * acceleration);
			controller.Move(desiredMoveDirection * Time.deltaTime * (movementSpeed * acceleration));
		}
		else
		{
			//Strafe
			controller.Move((transform.forward * moveAxis.y + transform.right * moveAxis.y) * Time.deltaTime * (movementSpeed * acceleration));
		}
	}

	public void LookAt(Vector3 pos)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), rotationSpeed);
	}

	public void RotateToCamera(Transform t)
	{
		var forward = cam.transform.forward;

		desiredMoveDirection = forward;
		Quaternion lookAtRotation = Quaternion.LookRotation(desiredMoveDirection);
		Quaternion lookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, lookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

		t.rotation = Quaternion.Slerp(transform.rotation, lookAtRotationOnly_Y, rotationSpeed);
	}

	void InputMagnitude()
	{
		//Calculate the Input Magnitude
		float inputMagnitude = new Vector2(moveAxis.x, moveAxis.y).sqrMagnitude;

		//Physically move player
		if (inputMagnitude > 0.1f)
		{
			anim.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f, Time.deltaTime);
			PlayerMoveAndRotation();
		}
		else
		{
			anim.SetFloat("InputMagnitude", inputMagnitude * acceleration, .1f,Time.deltaTime);
		}
	}

	#region Input

	public void OnMove(InputValue value)
	{
		moveAxis.x = value.Get<Vector2>().x;
		moveAxis.y = value.Get<Vector2>().y;
	}

	#endregion

	private void OnDisable()
	{
		anim.SetFloat("InputMagnitude", 0);
	}

    void StartDash(bool IsForward)
    {
        isDashing = true;
        dashTime = dashDuration;
        //dashDirection = moveAxis.y > 0 ? transform.forward : -transform.forward;

        dashDirection = IsForward ? transform.forward : -transform.forward;

        anim.SetBool("IsDashingForward", IsForward);
        anim.SetBool("IsDashingBackward", !IsForward);

        // Disable normal movement animation
        anim.SetFloat("InputMagnitude", 0);
    }

    void EndDash()
    {
        isDashing = false;
        anim.SetBool("IsDashingForward", false);
        anim.SetBool("IsDashingBackward", false);
    }
}