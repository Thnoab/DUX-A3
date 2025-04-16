using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Vector3 offset;
    private Transform playerTransform;

    // Start �ڵ�һ��֡����֮ǰ����
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // Update ÿ֡����һ��
    void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}