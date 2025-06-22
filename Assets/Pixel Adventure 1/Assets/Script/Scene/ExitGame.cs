using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExitGame : MonoBehaviour
{

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //exit trên unity
#else 
        Application.quit(); // exit trên thiết bị thực
#endif
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
