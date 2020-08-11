using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
    private const string PLAYER_TAG = "Player";
    [SerializeField]
    private Camera cam;
    public PlayerWeapon weapon;
    [SerializeField]
    private LayerMask mask;
    public GameObject bullet;
    Vector3 endPosition;
    Vector3 startPostion;
    
	// Use this for initialization
	void Start () {
		if(cam == null)
        {
            Debug.LogError("PlayerShoot: No Camera Referenced");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
  
        
            
        
    }
   

    void ShootEffect()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Invoke("StopShoot", 0.5f);
    }
    void StopShoot()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }

    [Client]
    void Shoot()
    {
        //Vector3 localPostion = cam.transform.position;
        //int numberOfShots = 5;
        ShootEffect();
        
            RaycastHit hit;
            startPostion = cam.transform.position;
            endPosition = startPostion + (weapon.range * cam.transform.forward);
            if (Physics.SphereCast(cam.transform.position, 0.2f , cam.transform.forward, out hit, weapon.range, mask))
            {

                //Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * weapon.range, Color.yellow);
                



                /* ln.SetPosition(0, targetPosition);
                 ln.SetPosition(1, endPosition); */


                if (hit.collider.tag == PLAYER_TAG)
                {
                    CmdPlayerShot(hit.collider.name, weapon.damage);
                }
                if (hit.collider.tag == "Object")
                {
                    Debug.Log("Something has been hit");
                    Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                    hit.rigidbody.AddForce(-hit.normal * 2000);

                }
            }


            /*float randomX = Random.Range(-0.09f, 0.09f);
            float randomY = Random.Range(-0.09f, 0.09f);
            localPostion.x += randomX;
            localPostion.y += randomY;
            Debug.DrawRay(cam.transform.position, endPosition, Color.red);*/
        
    }
    [Command]
    void CmdPlayerShot (string _playerID , int _damage)
    {
        Debug.Log(_playerID + " has been shot");
        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);

    }
    
}
