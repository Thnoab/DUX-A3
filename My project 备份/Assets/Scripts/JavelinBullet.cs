using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    private Rigidbody rgd;
    private Collider col;
    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player") { return; }
        rgd.velocity = Vector3.zero;
        rgd.isKinematic = true;
        col.enabled = false;

        Destroy(this.gameObject, 1f);
    }

}
