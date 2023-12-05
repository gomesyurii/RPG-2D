using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public Animator playerAnimator;
    public Player player;
    public float input_x = 0;
    public float input_y = 0;
    public bool isWalking = false;

    public Rigidbody2D rb;
     public Vector2 move = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isWalking = false;
        player = GetComponent<Player>();
    }

    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        isWalking = (input_x != 0 || input_y != 0);
        move = new Vector2(input_x, input_y).normalized;

        if (isWalking)
        {
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);


        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("attack");
        }        

        if (Input.GetKey(KeyCode.LeftShift) &&  player.entity.currentStamina > 0) 
        {
            player.entity.speed =  player.entity.runSpeed;
            player.entity.currentStamina -= 0.05f;
        }
        else 
        {
             player.entity.speed =  player.entity.walkSpeed;
        }
       
    }

    

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * player.entity.speed * Time.fixedDeltaTime);

        
    }

}
