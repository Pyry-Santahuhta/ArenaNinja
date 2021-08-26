using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    float jumpSpeed = 5;
    float walkSpeed = 3;
    float runSpeed = 6;
    float currentSpeed;
    bool superJumpsRemaining;
    float playerDamage = 1;
    //float hitPoints = 100;
    float distToGround = 0;
    public float currentX = 0.0f;
    bool moving;
    private bool jumpKeyPressed;
    private bool isGrounded;
    private bool hasDoubleJumped;
    private UnityEngine.Camera cam;
    float turnSmoothTime = 0.2f;
    Transform cameraT;
    float turnSmoothVelocity;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        cam = UnityEngine.Camera.main;
        distToGround = GetComponent<Collider>().bounds.extents.y;
        cameraT = Camera.main.transform;
    }
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        currentX += Input.GetAxis("Mouse X");
        Vector3 movementX = cam.transform.right * inputDir.x;
        Vector3 movementZ = cam.transform.forward * inputDir.y;
        Vector3 movement = movementX + movementZ;


        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        if (Input.GetButtonDown("Jump") == true)
        {
            if (!isGrounded)
            {
                if (!hasDoubleJumped)
                {
                    hasDoubleJumped = true;
                    jumpKeyPressed = true;
                }
                else
                {
                    jumpKeyPressed = false;
                }
            }
            else
            {
                jumpKeyPressed = true;
            }
        }


    }

    private void FixedUpdate()
    {
        if (jumpKeyPressed)
        {
            jumpSpeed = 5;
            if (superJumpsRemaining)
            {
                jumpSpeed *= 2;
                superJumpsRemaining = false;
            }
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 60f)
                isGrounded = true;
            hasDoubleJumped = false;
        }




    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            playerDamage = playerDamage * 4;
        }
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
            superJumpsRemaining = true;
        }

    }
}
