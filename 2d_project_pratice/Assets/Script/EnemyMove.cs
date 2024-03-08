using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    Animator anim;
    public int nextMove;
    SpriteRenderer spriteRender;
    void Start()
    {
        anim = GetComponent<Animator>(); 
        rigid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>(); 
        Invoke("Think", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector3(nextMove*Time.deltaTime, 0, 0));

        Vector2 frontVec = new Vector2(rigid.position.x +  nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        Debug.DrawRay(rigid.position, Vector3.right, new Color(1, 0, 0));

        //RaycastHit2D raywall = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null )
        {
            Turn();
        }
        
    }

    void Think()
    {
        nextMove = Random.Range(-1,2);// random °ªÀº ÃÖ´ë°ªÀº ¹Ì¸¸ÀÓ
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("RunSpeed", nextMove);
     
        if(nextMove!=0)
        {
            
            spriteRender.flipX = nextMove == 1;
        }

    }

    void Turn()
    {
        Debug.Log("À¸¾Ç ³¶¶°·¯Áö´Ù.");
        nextMove *= -1;
        spriteRender.flipX = nextMove == 1;
        CancelInvoke();// invoke ¸ØÃã
        Invoke("Think", 2);
    }
}
