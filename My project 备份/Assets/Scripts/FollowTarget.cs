using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Vector3 offset;
    private Transform playerTransform;

    // Start 在第一次帧更新之前调用
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // Update 每帧调用一次
    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}