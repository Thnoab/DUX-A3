using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 需要添加这个命名空间来使用NavMeshAgent

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState,
    }

    private EnemyState state = EnemyState.NormalState;
    private EnemyState childState = EnemyState.RestingState;
    private NavMeshAgent enemyAgent;

    public float restTime = 1;
    private float restTimer = 0;

    public int HP = 100;


    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (state == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime; // 修正：应该是Time.deltaTime而不是restTime.deltaTime

                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            }
            else if (childState == EnemyState.MovingState)
            {
                if (enemyAgent.remainingDistance <= 0.1f) // 修正：≤改为<=，并添加一个小的阈值
                {
                    restTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(30);
        }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        return transform.position + randomDir.normalized * Random.Range(2f, 5f);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            GetComponent<Collider>().enabled = false;
            int count = 2;
            for(int i =0;i < count; i++)
            {
                ItemSO item = ItemDBManager.Instance.GetRandomItem();
                print(transform.position);
                GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);
                Animator anim = go.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.enabled = false;
                }
              
            }
            Destroy(this.gameObject);

        }

    }

}