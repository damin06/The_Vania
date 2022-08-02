using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exit : MonoBehaviour
{
    [SerializeField] float levelLoadelay = 1;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Player"))
    {
            StartCoroutine(levelnext());
    }
  }

    IEnumerator levelnext()
    {
        yield return new WaitForSeconds(levelLoadelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<SceenPersist>().ResetPersists();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
