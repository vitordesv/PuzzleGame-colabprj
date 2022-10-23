using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;

    void Update()
    {
        //Set velocidade em X (horizontal)
        if (playerContrlSCRIPT.standingCollider.enabled || !scrVariaveis.segurarcaixa)
            playerContrlSCRIPT.rb.velocity = new Vector3(playerContrlSCRIPT.moveinput * playerContrlSCRIPT.speed, playerContrlSCRIPT.rb.velocity.y, 0);
        else
            playerContrlSCRIPT.rb.velocity = new Vector3(playerContrlSCRIPT.moveinput * playerContrlSCRIPT.speed * playerContrlSCRIPT.crouchSpeedModifier, playerContrlSCRIPT.rb.velocity.y, 0);
        /* if (playerContrlSCRIPT.rb.velocity.y < 0) playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityDown;
         else playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityUp;*/

        Flip();
        Jump();
    }

    private void FixedUpdate()
    {
        Agachar(playerContrlSCRIPT.crouchPressed);
    }

    //Pular
    internal void Jump()
    {
        if (playerContrlSCRIPT.jumpBuffer && playerContrlSCRIPT.isGrounded && !scrVariaveis.segurarcaixa)
        {
            if (playerContrlSCRIPT.standingCollider.enabled)
            {
                playerContrlSCRIPT.rb.velocity = new Vector3(playerContrlSCRIPT.rb.velocity.x, playerContrlSCRIPT.jumpForce, 0);
            }
            playerContrlSCRIPT.jumpBuffer = false;
        }
    }

    //agachar
    internal void Agachar(bool crouchFlag)
    {
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(playerContrlSCRIPT.overheadCheck.position, playerContrlSCRIPT.overheadRadius, playerContrlSCRIPT.groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (playerContrlSCRIPT.isGrounded)
            playerContrlSCRIPT.standingCollider.enabled = !crouchFlag;
    }

    //Virar Sprite do player
    internal void Flip()
    {
        if (playerContrlSCRIPT.moveinput < 0)
        {
            playerContrlSCRIPT.facingR = false;
            playerContrlSCRIPT.spriteRenderer.flipX = true;
        }
        else if (playerContrlSCRIPT.moveinput > 0) 
        {
            playerContrlSCRIPT.facingR = true;
            playerContrlSCRIPT.spriteRenderer.flipX = false;
        }
    }
}
