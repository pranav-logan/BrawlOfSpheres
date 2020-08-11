using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int playerHealth;

    public GameObject bullet;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "Ground")
        {
            Invoke("DestroyObject", 2);
        }

        // Hurts Player
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
            playerHealth = collision.gameObject.GetComponent<PlayerController>().health;
            Debug.Log(collision.gameObject.GetComponent<PlayerController>().health);
        }

        //Kills Player
        if (playerHealth == 0)
        {
            //collision.gameObject.SetActive(false);

            Object.Destroy(collision.gameObject, 0.01f);
            Debug.Log("Player Destroyed");

        }

    }
    void DestroyObject()
    {
        Object.Destroy(bullet);
    }
}
