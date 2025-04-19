using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    public int atkValue = 30;
    private Rigidbody rgd;
    private Collider col;
    private bool isThrown = false; // 标记是否被投掷出去

    public void SetThrown(bool thrown)
    {
        isThrown = thrown;
    }

    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果标枪没有被投掷出去，直接返回
        if (!isThrown) return;

        if (collision.collider.tag == "Player") { return; }

        rgd.velocity = Vector3.zero;
        rgd.isKinematic = true;
        col.enabled = false;
        transform.parent = collision.gameObject.transform;

        Destroy(this.gameObject, 0.3f);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(atkValue);
        }
    }
}