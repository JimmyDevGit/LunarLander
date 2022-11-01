using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        if(GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 0)
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 1)
            SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 2)
            SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 3)
            SceneManager.LoadScene("Level5");
    }
    public void LoadLevel6()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 4)
            SceneManager.LoadScene("Level6");
    }
    public void LoadLevel7()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 5)
            SceneManager.LoadScene("Level7");
    }
    public void LoadLevel8()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 6)
            SceneManager.LoadScene("Level8");
    }
    public void LoadLevel9()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 7)
            SceneManager.LoadScene("Level9");
    }
    public void LoadLevel10()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 8)
            SceneManager.LoadScene("Level10");
    }
    public void LoadLevel11()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 9)
            SceneManager.LoadScene("Level11");
    }
    public void LoadLevel12()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 10)
            SceneManager.LoadScene("Level12");
    }
    public void LoadLevel13()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 11)
            SceneManager.LoadScene("Level13");
    }
    public void LoadLevel14()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 12)
            SceneManager.LoadScene("Level14");
    }
    public void LoadLevel15()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 13)
            SceneManager.LoadScene("Level15");
    }
    public void LoadLevel16()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 14)
            SceneManager.LoadScene("Level16");
    }
    public void LoadLevel17()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 15)
            SceneManager.LoadScene("Level17");
    }
    public void LoadLevel18()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 16)
            SceneManager.LoadScene("Level18");
    }
    public void LoadLevel19()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 17)
            SceneManager.LoadScene("Level19");
    }
    public void LoadLevel20()
    {
        if (GameObject.Find("MenuManager").GetComponent<MenuManager>().levelsCompleted > 18)
            SceneManager.LoadScene("Level20");
    }
    public void LoadTraining1()
    {
            SceneManager.LoadScene("LevelTraining");
    }
    public void LoadTraining2()
    {
        SceneManager.LoadScene("LevelTraining2");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
