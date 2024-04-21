using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private float velocity = 4;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * velocity;
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1f, 1f);
        }
        myRB.velocity = new Vector2(horizontal, myRB.velocity.y);
    }
}
