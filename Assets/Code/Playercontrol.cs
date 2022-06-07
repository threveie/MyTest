using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public AudioClip[] JumpClips;
    public AudioSource audioSource ;
    public AudioMixer audioMixer;

    private Transform mGrondCheck;
    Animator anim;
    float mVolume = 0;
    
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGrondCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

        anim.SetFloat("speed", Mathf.Abs(h));

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
        if(Input.GetKeyDown (KeyCode.UpArrow))
        {
            mVolume++;
            audioMixer.SetFloat("MasterVolume",mVolume );
        }
        if (bJump)
        {
            int i = Random.Range(0, JumpClips.Length);
            //AudioSource.PlayClipAtPoint(JumpClips[i], transform.position);
            audioSource.clip = JumpClips[i];
            audioSource.Play();
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
            anim.SetTrigger("jumping");
        }
    }
}



