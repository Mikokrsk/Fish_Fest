using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public static LoadLevelManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(id);
        SceneManager.LoadScene(id);
    }
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        SceneManager.LoadScene(name);
    }
}