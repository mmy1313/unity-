using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer AudioMixer;

    //开始游戏
    public void PlayGame()
    {
        SceneManager.LoadScene("level01");
    }

    //结束游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/mainmenu/UI").SetActive(true);
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void setVolume(float value)
    {
        AudioMixer.SetFloat("MainVolume",value);
    }

}
