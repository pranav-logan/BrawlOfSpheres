using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camerarotation = Vector3.zero;
    public GameObject PlasmaParticle;
    public float DashSpeed = 2;
    bool dashing = false;
    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlasmaParticle.gameObject.SetActive(true);
            rb.MovePosition(rb.position  +velocity * DashSpeed * Time.fixedDeltaTime);
            dashing = true;
            Invoke("TurnOff", 0.1f);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            if(dashing == true)
            {
               // something to push the player back to areas where the playe should be
                
                rb.MovePosition(rb.position - velocity  * Time.fixedDeltaTime);
            }
            
        }
    }
    void TurnOff ()
    {
        PlasmaParticle.gameObject.SetActive(false);
        dashing = false;
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _Rotation)
    {
        rotation = _Rotation;

    }
    public void RotateCamera(Vector3 _cameraRotation)
    {
        camerarotation = _cameraRotation;

    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

    }

    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
         
    }
    

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-camerarotation);

        }
    }

}
