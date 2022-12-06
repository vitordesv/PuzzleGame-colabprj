using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController playerContrlSCRIPT;

    void Update()
    {
        if(playerContrlSCRIPT.standingCollider.enabled && !scrVariaveis.segurarcaixa)
            playerContrlSCRIPT.rb.velocity= new Vector3(playerContrlSCRIPT.moveinput * playerContrlSCRIPT.speed, playerContrlSCRIPT.rb.velocity.y,0);
        else
        {
            playerContrlSCRIPT.rb.velocity= new Vector3(playerContrlSCRIPT.moveinput * playerContrlSCRIPT.speed * playerContrlSCRIPT.crouchSpeedModifier, playerContrlSCRIPT.rb.velocity.y,0);
            //Debug.Log("ANDANDO AGACHADO");
        }

        if (playerContrlSCRIPT.rb.velocity.y < 0) playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityDown;
        else playerContrlSCRIPT.rb.gravityScale = playerContrlSCRIPT.gravityUp;
        
        playerContrlSCRIPT.animator.SetFloat("speed", Mathf.Abs(playerContrlSCRIPT.moveinput));

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
        if (playerContrlSCRIPT.jumpBuffer)
        {
            if (playerContrlSCRIPT.isGrounded && !scrVariaveis.segurarcaixa)
            {
                if (playerContrlSCRIPT.standingCollider.enabled)
                {
                    playerContrlSCRIPT.rb.velocity = new Vector3(playerContrlSCRIPT.rb.velocity.x, playerContrlSCRIPT.jumpForce, 0);
                }
            } 
        }
        playerContrlSCRIPT.jumpBuffer = false;
    }

    //agachar
    internal void Agachar(bool crouchFlag)
    {
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(playerContrlSCRIPT.overheadCollider.position, playerContrlSCRIPT.overheadRadius, playerContrlSCRIPT.groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (playerContrlSCRIPT.isGrounded && !scrVariaveis.segurarcaixa)
            playerContrlSCRIPT.standingCollider.enabled = !crouchFlag;
    }

    //Virar Sprite do player
    internal void Flip()
    {
        //flip melhor
        /*  
        if (playerContrlSCRIPT.moveinput < 0) playerContrlSCRIPT.facingR = false;
        if (playerContrlSCRIPT.moveinput > 0) playerContrlSCRIPT.facingR = true;
        if (!scrVariaveis.segurarcaixa)
        {
            if (playerContrlSCRIPT.facingR) playerContrlSCRIPT.spriteRenderer.flipX = false;
            else playerContrlSCRIPT.spriteRenderer.flipX = true;
        }   */
        
        Vector3 scale = transform.localScale;

        if (playerContrlSCRIPT.moveinput < 0) playerContrlSCRIPT.facingR = false;
        if (playerContrlSCRIPT.moveinput > 0) playerContrlSCRIPT.facingR = true;

        if (!scrVariaveis.segurarcaixa)
        {
            if (playerContrlSCRIPT.facingR == true && transform.localScale.x < 0)
            {
                scale.x *= -1;
                transform.localScale = scale;
            }
            if (playerContrlSCRIPT.facingR == false && transform.localScale.x > 0)
            {
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }
}
