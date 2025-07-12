using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{   public static LoadingScene Instance;    
    public GameObject loadingScene;
   
    void Start()
    {
        Instance = this;
        loadingScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSceneByName(string sceneName)
    {
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null; 
        }
        loadingScene.SetActive(false);
    }

}
