using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public GameObject bullet;
    public const float BULLET_SPEED = 10f;
    public Vector3 direction = Vector3.right;
    public const float TIME_BETWEEN_SHOTS = 2f;
    
	void Start () {
        StartCoroutine(Activate());
    }

    void shoot()
    {
        GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
        clone.GetComponent<TurretBullet>().spawnOrigin = gameObject;
        clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(direction * BULLET_SPEED);
    }

    IEnumerator Activate()
    {
        while (true)
        {
            shoot();
            yield return new WaitForSeconds(TIME_BETWEEN_SHOTS);
        }
    }
}
