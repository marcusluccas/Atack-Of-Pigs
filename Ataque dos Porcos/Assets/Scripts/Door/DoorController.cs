using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator myAnimator;
    [SerializeField] private string Scene;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        myAnimator.SetTrigger("Open");
    }

    private void NextScene()
    {
        if (Scene != null)
        {
            FindObjectOfType<GameManager>().ChooseScene(Scene);
        }
    }
}
