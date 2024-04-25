using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private float velocity = 5;
    private Animator myAnimator;
    private int maxJumps = 1;
    private int amountJumps = 0;
    private BoxCollider2D myBoxCol;
    [SerializeField] private LayerMask myLayer;
    private bool inFloor = false;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        amountJumps = maxJumps;
        myBoxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        myAnimator.SetBool("InFloor", InGrounded());

        if (InGrounded())
        {
            amountJumps = maxJumps;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * velocity;
        myAnimator.SetBool("Move", false);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1f, 1f);
            myAnimator.SetBool("Move", true);
        }
        myRB.velocity = new Vector2(horizontal, myRB.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && amountJumps > 0 && InGrounded())
        {
            myRB.velocity = Vector2.up * velocity;
            //myAnimator.SetBool("InFloor", false);
            amountJumps--;
        }

        myAnimator.SetFloat("Vspeed", 0f);
        if (myRB.velocity.y != 0f)
        {
            myAnimator.SetFloat("Vspeed", Mathf.Sign(myRB.velocity.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemys"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                myRB.velocity = Vector2.up * velocity;
                myAnimator.SetFloat("Vspeed", Mathf.Sign(myRB.velocity.y));
            }
            else
            {

            }
        }
    }

    private bool InGrounded()
    {
        bool inFloor = Physics2D.Raycast(myBoxCol.bounds.center, Vector2.down, 0.5f, myLayer);

        Debug.DrawRay(myBoxCol.bounds.center, Vector2.down * 0.5f, Color.red);

        return inFloor;
    }
}
