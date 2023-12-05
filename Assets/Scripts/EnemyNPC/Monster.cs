using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Monster : MonoBehaviour
{
    [Header("Controler")]
    public Entity entity;
    public GameManager manager;


    [Header("Patrol")]
    public Transform[] waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5f;


    //Private variables
    private Transform targetWaypoint;
    private int currentWaypoint = 0;
    private float lastDistanceToTarget = 0;
    private float currentWaitTime = 0;


    [Header("Experience reward")]
    public int experienceReward = 10;
    public int lootGoldMin = 0;
    public int lootGoldMax = 10;


    [Header("Respawn")]
    public GameObject prefab;
    public bool respawn = true;
    public float respawnTime = 10f;
    private Rigidbody2D rb2D;
    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        entity.maxHealth = manager.CalculateHealth(entity);
        entity.maxMana = manager.CalculateMana(entity);
        entity.maxStamina = manager.CalculateStamina(entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        currentWaitTime = waitTime;
        if (currentWaitTime > 0)
        {
            targetWaypoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);
        }

    }


    void Update()
    {
        if (entity.dead) return;

        if (entity.currentHealth <= 0)
        {
            entity.dead = true;
            entity.currentHealth = 0;
            Die();
        }

        if (!entity.inCombat)
        {
            if (waypointList.Length > 0)
            {
                Patrol();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            if (entity.attackTimer > 0)
            {
                entity.attackTimer -= Time.deltaTime;
            }

            if (entity.attackTimer < 0)
            {
                entity.attackTimer = 0;
            }

            if (entity.target != null && entity.inCombat)
            {
                if (!entity.combatCoroutine)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                entity.combatCoroutine = false;
                StopCoroutine(Attack());
            }


        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !entity.dead)
        {
            entity.inCombat = true;
            entity.target = collider.gameObject;
            entity.target.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            entity.inCombat = false;
            if (entity.target)
            {
                entity.target.GetComponent<CapsuleCollider2D>().isTrigger = false;
                entity.target = null;
            }

        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        GameObject newMonster = Instantiate(prefab, transform.position, transform.rotation, null);
        newMonster.name = prefab.name;
        newMonster.GetComponent<Monster>().entity.dead = false;

        Destroy(this.gameObject);

    }

    void Patrol()
    {
        if (entity.dead)
            return;

        // calcular a distance do waypoint
        float distanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);

        if (distanceToTarget <= arrivalDistance || distanceToTarget >= lastDistanceToTarget)
        {
            animator.SetBool("isWalking", false);

            if (currentWaitTime <= 0)
            {
                currentWaypoint++;

                if (currentWaypoint >= waypointList.Length)
                    currentWaypoint = 0;

                targetWaypoint = waypointList[currentWaypoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);

                currentWaitTime = waitTime;
            }
            else
            {
                currentWaitTime -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        animator.SetFloat("input_x", direction.x);
        animator.SetFloat("input_y", direction.y);

        rb2D.MovePosition(rb2D.position + direction * (entity.speed * Time.fixedDeltaTime));
    }

    IEnumerator Attack()
    {
        entity.combatCoroutine = true;
        while (true)
        {
            yield return new WaitForSeconds(entity.attackTimer);

            if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead)
            {
                animator.SetBool("attack", false);
                float distance = Vector2.Distance(entity.target.transform.position, transform.position);
                if (distance <= entity.attackDistance)
                {

                    //ADICIONAR AQUI O BUFF DE DANO/RESISTENCIA DAS ARMAS E ARMADURAS
                    int monsterDMG = manager.CalculateDamage(entity, entity.damage);
                    int targetDEF = manager.CalculateDefense(entity.target.GetComponent<Player>().entity, entity.defense);
                    int dmgResult = monsterDMG - targetDEF;
                    if (dmgResult < 0) dmgResult = 0;
                    Debug.Log("Dano do monstro: " + monsterDMG);
                    Debug.Log("Defesa do player: " + targetDEF);
                    Debug.Log("Dano resultante: " + dmgResult);
                    entity.target.GetComponent<Player>().entity.currentHealth -= dmgResult;
                }
            }
        }
    }
    void Die()
    {
        entity.dead = true;
        entity.inCombat = false;
        entity.target = null;
        animator.SetBool("isWalking", false);

        // Add exp pro player
        Debug.Log("Inimigo morreu" + entity.name);

        StopAllCoroutines();
        StartCoroutine(Respawn());
    }



}
