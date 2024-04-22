using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private float velocity = 5;
    private Animator myAnimator;
    private int maxJumps = 1;
    private int amountJumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        amountJumps = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * velocity;
        myAnimator.SetBool("Move", false);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1f, 1f);
            myAnimator.SetBool("Move", true);
        }
        myRB.velocity = new Vector2(horizontal, myRB.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (amountJumps > 0)
            {
                myRB.velocity = Vector2.up * velocity;
                myAnimator.SetBool("InFloor", false);
                amountJumps--;
            }
        }

        myAnimator.SetFloat("Vspeed", 0f);
        if (myRB.velocity.y != 0f)
        {
            myAnimator.SetFloat("Vspeed", Mathf.Sign(myRB.velocity.y));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            myAnimator.SetBool("InFloor", true);
            amountJumps = maxJumps;
        }
    }
}
