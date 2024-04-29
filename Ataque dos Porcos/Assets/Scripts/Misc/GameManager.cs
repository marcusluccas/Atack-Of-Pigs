using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int life = 3;
    [SerializeField] private Image[] lifeImages; 

    private void Awake()
    {
        int amount = FindObjectsOfType<GameManager>().Length;

        if (amount > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        DrawLife();
    }

    public void ChooseScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public int CurrentLife()
    {
        return life;
    }

    public void SetLife(int lifePlayer)
    {
        life = lifePlayer;
    }

    public void Reset()
    {
        life = 3;
        SceneManager.LoadScene("World0");
    }

    private void DrawLife()
    {
        for(int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = false;
            if (i < life)
            {
                lifeImages[i].enabled = true;
            }
        }
    }
}
