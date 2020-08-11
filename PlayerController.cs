using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 800f;
    [SerializeField]
    private float lookSensitivity = 3f;
    private PlayerMotor motor;
    public GameObject SparkEfecct;
   public Rigidbody rb;
    bool AvaliableJump = true;
   public Rigidbody bullet;
    public Rigidbody bulletMov;
    public GameObject gun;
    public int health = 10;
   
   // bool CancelAll = false;
     void Start()
    {
        motor = GetComponent<PlayerMotor>();
        speed = 20;
    }

     void Update()
    {   
       // board.GetComponent<Text>().text = "Health: " + health.ToString();
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motor.Move(_velocity);

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = 65;
            SparkEfecct.SetActive(true);
            Invoke("SlowDown", 5f);
        }
        if (Input.GetKeyDown("space"))
        {
            if(AvaliableJump == true)
            {
                rb.AddForce(0, 2200, 0);
                AvaliableJump = false;
            }
             
            
        }
         
        
        /*
        // Old Shooting System
        if (Input.GetKeyDown(KeyCode.Mouse0)){


            Rigidbody clone;
            //Vector3 offset = new Vector3(0.413f, -0.2f, 1.331f);
            clone = Instantiate(bullet, gun.transform.position , transform.rotation);
            clone.velocity = gun.transform.TransformDirection(Vector3.forward * 100);
            // clone.AddForce(0, 0, 200f);

        }   
        */

        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _Rotation = new Vector3(0f, _yRot, 0) * lookSensitivity;

        motor.Rotate(_Rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(_cameraRotation);
    }
    void SlowDown()
    {
        speed = 20;
        SparkEfecct.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            AvaliableJump = true;
        }
    }
}
