using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinWeapon : Weapon
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    private GameObject bulletGo;
    private void Start()
    {
        SpawnBullet();

        

    }

    public override void Attack()
    {
        if(bulletGo != null)
        {
            bulletGo.transform.parent = null;
            bulletGo.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            bulletGo = null;
            Invoke("SpawnBullet", 0.2f);
        }
            
        else
        {
            return;

        }
        
    }

    private void SpawnBullet()
    {
        bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGo.transform.parent = transform;

    }

}
