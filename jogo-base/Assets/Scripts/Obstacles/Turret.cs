using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject bullet;
    public float bulletSpeed;
    public Vector3 direction;
    
	void Start () {
        bulletSpeed = 10f;
        direction = Vector3.right;
        StartCoroutine(Activate());
    }
	
	void Update () {
		
	}

    void Shoot()
    {
        GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
        clone.GetComponent<TurretBullet>().spawnOrigin = gameObject;
        clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(direction * bulletSpeed);
    }

    IEnumerator Activate()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(2f);
        }
    }
}
