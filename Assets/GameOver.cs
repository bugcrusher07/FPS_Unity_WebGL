using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Yes()
    {
        SceneManager.LoadScene(1);
    }
    public void No()
    {

        SceneManager.LoadScene(0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
