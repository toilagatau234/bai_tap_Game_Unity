using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadCurentScene : MonoBehaviour
{

    public void ReloadScene()
    {
        if (GameController.instance.isGameOver) // kiểm tra hiện tại cso phải là game over không?
        {
            Time.timeScale = 1;
            GameController.instance.isGameOver = false; // nếu là game over thì đưa về chế độ play
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //cho phép nạp lại scene
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
