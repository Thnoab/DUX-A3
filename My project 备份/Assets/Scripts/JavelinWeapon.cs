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
        if (bulletGo != null)
        {
            bulletGo.transform.parent = null;
            bulletGo.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            bulletGo.GetComponent<JavelinBullet>().SetThrown(true); // 通知子弹它被投掷了
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
        bulletGo.GetComponent<JavelinBullet>().SetThrown(false); // 新生成的标枪在手中

        if (tag == "Interactable")
        {
            Destroy(bulletGo.GetComponent<JavelinBullet>());
        }
    }
}