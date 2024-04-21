using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private float velocity = 4;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
    }
}
