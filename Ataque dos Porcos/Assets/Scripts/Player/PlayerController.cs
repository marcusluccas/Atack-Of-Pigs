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
    private BoxCollider2D myBoxCol;
    [SerializeField] private LayerMask myLayer;
    private int life;
    private float waitHit = 0;
    private bool dead = false;
    private DoorController door = null;
    private bool openDoor = false;
    private GameManager theGM;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        amountJumps = maxJumps;
        myBoxCol = GetComponent<BoxCollider2D>();
        myAnimator.SetInteger("Life", life);
        theGM = FindObjectOfType<GameManager>();
        life = theGM.CurrentLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && !openDoor)
        {
            Move();
            Jump();
            Invencibilidade();
            OpenDoor();
        }
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

    private void Invencibilidade()
    {
        waitHit -= Time.deltaTime;
    }

    private void OpenDoor()
    {
        if (door != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                door.Open();
                Invoke("AnimationOpenDoor", 0.5f);
                openDoor = true;
                myRB.velocity = Vector2.zero;
                myAnimator.SetBool("Move", false);
            }
        }
    }

    private void AnimationOpenDoor()
    {
        myAnimator.SetTrigger("OpenDoor");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Doors"))
        {
            door = collision.GetComponent<DoorController>();
        }

        if (collision.gameObject.CompareTag("Enemys"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                myRB.velocity = Vector2.up * velocity;
                collision.GetComponentInParent<Animator>().SetTrigger("Hit");
                myAnimator.SetFloat("Vspeed", Mathf.Sign(myRB.velocity.y));
            }
            else
            {
                if (waitHit <= 0f && !dead && !openDoor)
                {
                    life--;
                    theGM.SetLife(life);
                    myAnimator.SetTrigger("Hit");
                    myAnimator.SetInteger("Life", life);
                    if (life < 0)
                    {
                        dead = true;
                        myRB.velocity = Vector2.zero;
                    }
                    waitHit = 1f;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Doors"))
        {
            door = null;
        }
    }

    private bool InGrounded()
    {
        bool inFloor = Physics2D.Raycast(myBoxCol.bounds.center, Vector2.down, 0.5f, myLayer);

        Debug.DrawRay(myBoxCol.bounds.center, Vector2.down * 0.5f, Color.red);

        return inFloor;
    }

    private void GameOver()
    {
        theGM.Reset();
    }
}
