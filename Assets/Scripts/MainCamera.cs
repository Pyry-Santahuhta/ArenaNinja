using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player;
    public float dstFromTarget = 2;
    float mouseX, mouseY;
    public Vector2 Yminmax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();

    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, Yminmax.x, Yminmax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mouseY, mouseX), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = Target.position - transform.forward * dstFromTarget;
        /*
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
        */
    }


    /*
         public GameObject player;
     private Vector3 offset;
     public Transform lookAt;
     public Transform camTransform;
     private UnityEngine.Camera cam;
     public const float Y_ANGLE_MIN = 10.0F;
     public const float Y_ANGLE_MAX = 10.0f;
     private float distance = 5.0f;
     public float currentX = 0.0f;
     private float currentY = 0.0f;
     public float sensitivitX = 4.0f;
     public float sensitivityY = 1.0f;
        // Start is called before the first frame update
        void Start(){
            offset = transform.position - player.transform.position;
            camTransform = transform;
            cam = UnityEngine.Camera.main;
        }

        // Update is called once per frame
        void Update(){
            if (Input.GetMouseButton(1)){
                currentX += Input.GetAxis("Mouse X");
                currentY += Input.GetAxis("Mouse Y");
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

                transform.position = player.transform.position + offset;
                Vector3 dir = new Vector3(0, 0, -distance);
                Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
                camTransform.position = lookAt.position + rotation * dir;
                camTransform.LookAt(lookAt.position);
            } 
        }*/
}
