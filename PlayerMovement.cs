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
        Segurar();
    }

    private void FixedUpdate()
    {
        Agachar(playerContrlSCRIPT.crouchPressed);
    }

    //Pular
    internal void Jump()
    {
        if (playerContrlSCRIPT.jumpBuffer)
        {
            if (playerContrlSCRIPT.isGrounded && playerContrlSCRIPT.standingCollider.enabled && !scrVariaveis.segurarcaixa)
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
        {
            playerContrlSCRIPT.standingCollider.enabled = !crouchFlag;

            if (crouchFlag)
                playerContrlSCRIPT.animator.SetBool("Crouch", true);
            else
                playerContrlSCRIPT.animator.SetBool("Crouch", false);
        }
    }

    //Virar Sprite do player
    internal void Flip()
    {
        if (playerContrlSCRIPT.moveinput < 0) playerContrlSCRIPT.facingR = false;
        else if (playerContrlSCRIPT.moveinput > 0) playerContrlSCRIPT.facingR = true;

        playerContrlSCRIPT.spriteRenderer.flipX = !playerContrlSCRIPT.facingR;
    }

    void Segurar()
    {
        if (playerContrlSCRIPT.segurarPressed && scrVariaveis.pegavel || playerContrlSCRIPT.segurarPressed && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa = true;
            playerContrlSCRIPT.japegou++;
            Debug.Log("ja pegou" + playerContrlSCRIPT.japegou);
        }
        if (playerContrlSCRIPT.segurarPressed && playerContrlSCRIPT.japegou == 2 && scrVariaveis.segurarcaixa)
        {
            scrVariaveis.segurarcaixa = false;
            playerContrlSCRIPT.japegou = 0;
            Debug.Log("Soltou");
        }
    }
}
