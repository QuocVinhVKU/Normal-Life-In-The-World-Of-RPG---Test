using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;

    [Header("Transition Variables")]
    public GameObject fadeInPanels;
    public GameObject fadeOutPanels;
    public float fadeWait;

    private void Awake()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPos;
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);
            
        }
    }
    public IEnumerator FadeCo()
    {
        if (fadeOutPanels != null)
        {
            Instantiate(fadeOutPanels, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        if (fadeInPanels != null)
        {
            Instantiate(fadeInPanels, Vector3.zero, Quaternion.identity);
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
