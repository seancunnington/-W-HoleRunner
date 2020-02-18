using System.Collections;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

     public float mouseSensitivityX = 250f;
     public float mouseSensitivityY = 250f;
     public float runSpeed = 10f;
     public float smoothAmount = 0.15f;
     public float jumpForce = 220f;
     public LayerMask groundedMask;

     Transform cameraTransform;
     float verticalLookRotation;

     Vector3 moveAmount;
     Vector3 smoothMoveVelocity;

     bool grounded;
     Rigidbody body;

     public GameObject currentPlanet;

     // Start is called before the first frame update
     void Start()
     {
          cameraTransform = Camera.main.transform;
          body = GetComponent<Rigidbody>();
     }

     // Update is called once per frame
     private void Update()
     {
          transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
          verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
          verticalLookRotation = Mathf.Clamp(verticalLookRotation, -20f, 20f);
          cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

          Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
          Vector3 targetMoveAmount = moveDir * runSpeed;
          moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, smoothAmount);

          if (Input.GetButtonDown("Jump"))
          {
               if (grounded)
                    body.AddForce(transform.up * jumpForce);
          }

          grounded = false;
          Ray ray = new Ray(transform.position, -transform.up);
          RaycastHit hit;

          if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
               grounded = true;

     }


     private void FixedUpdate()
     {
          body.MovePosition(body.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
     }
}
