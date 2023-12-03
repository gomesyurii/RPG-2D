using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Animator playerAnimator;
    public float input_x = 0;
    public float input_y = 0;
    public float speed = 1f;
    public bool isWalking = false;

    void Start()
    {
        isWalking = false;
    }

    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        isWalking = (input_x != 0 || input_y != 0);

        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }   

        playerAnimator.SetBool("isWalking", isWalking);


        transform.position += new Vector3(input_x, input_y, 0) * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("attack");
        }       
    }
}
