using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerSpeed = 5;
    public int jumpPower = 10;
    //bool isJumping;
    Rigidbody2D rigid;
    SpriteRenderer spriteRender;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();// 현재 스크립트를 가지고 있는 오브젝트 rigidbody 가져오기
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();    
        //isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !anim.GetBool("isJumping"))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            // 오른쪽 키를 눌렀고, 왼쪽 키를 누르지 않았을 때
            transform.Translate(new Vector3(playerSpeed * Time.deltaTime, 0, 0));
            spriteRender.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            // 왼쪽 키를 눌렀고, 오른쪽 키를 누르지 않았을 때
            transform.Translate(new Vector3(-playerSpeed * Time.deltaTime, 0, 0));
            spriteRender.flipX = true;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log(rayHit.collider.name);
                    anim.SetBool("isJumping", false);
                }

            }
        }

    }

    void Jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            //isJumping = false;
        }
    }
}
