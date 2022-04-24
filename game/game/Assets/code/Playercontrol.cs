using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    public bool bJump = false;
    public float JumpForce = 100;
    private Transform mGrondCheck;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGrondCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //�����ٶ�
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }
        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);
        }


        //��ɫ��ת
        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }



        //��ɫ��Ծ
        if (Physics2D.Linecast(transform.position, mGrondCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }

    private void FixedUpdate()
    {
        if (bJump)
        {
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
        }
    }
}



