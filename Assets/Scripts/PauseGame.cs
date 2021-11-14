using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private void Awake() => PauseGameNow(); 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGameNow();
    }

    private void PauseGameNow()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseMenu.SetActive(pauseMenu.activeSelf != true);
    }
}
