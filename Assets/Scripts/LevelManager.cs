using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int currentSceneIndex;

    private Animator animator;

    void Start()
    {
        Object.DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeInto(sceneIndex));
    }

    private IEnumerator FadeInto(int sceneIndex)
    {
        animator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
        animator.SetTrigger("fadeOut");
    }

}
