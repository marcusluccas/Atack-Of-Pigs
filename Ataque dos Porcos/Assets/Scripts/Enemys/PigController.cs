using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private float myVelocity = 2f;
    private float waitMove = 1f;
    private int direction;
    private Animator myAnimator;
    private BoxCollider2D myBoxCol;
    [SerializeField] private LayerMask myLayerCol;
    private bool dead = false;
    private SpriteRenderer mySprite;
    [SerializeField] private BoxCollider2D myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        direction = Random.Range(-1, 2);
        myAnimator = GetComponent<Animator>();
        myBoxCol = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move()
    {
        myAnimator.SetBool("Move", false);

        if (!dead)
        {

            if (Collider())
            {
                myRB.velocity = new Vector2(myRB.velocity.x * -1, myRB.velocity.y);
            }

            waitMove -= Time.deltaTime;
            if (waitMove <= 0f)
            {
                myRB.velocity = new Vector2(myVelocity * direction, 0f);
                waitMove = Random.Range(5, 8);
                direction = Random.Range(-1, 2);
            }

            if (myRB.velocity.x != 0f)
            {
                transform.localScale = new Vector3(Mathf.Sign(myRB.velocity.x) * -1, 1f, 1f);
                myAnimator.SetBool("Move", true);
            }
        }
    }

    private bool Collider()
    {
        bool colBlock = Physics2D.Raycast(myBoxCol.bounds.center, new Vector2(Mathf.Sign(transform.localScale.x) * -1, 0f), 0.4f, myLayerCol);

        Debug.DrawRay(myBoxCol.bounds.center, new Vector2(Mathf.Sign(transform.localScale.x) * -1, 0f) * 0.4f, Color.red);

        return colBlock;
    }

    private void MyDead()
    {
        dead = true;
        myRB.velocity = new Vector2(0f, myRB.velocity.y);
        myCollider.enabled = false;
        Destroy(gameObject, 1f);
    }
}
