using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField]
    float timeToChangeScene = 1.0f;

    public void LoadScene(string s)
    {
        StartCoroutine(LoadSceneCoroutine(s));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadSceneCoroutine(string s)
    {
        transition.SetTrigger("SceneTransitionStart");

        yield return new WaitForSeconds(timeToChangeScene);

        SceneManager.LoadScene(s);

        SceneManager.sceneLoaded += TriggerLoadSceneAnim;

    }

    public void LoadTransition(States state)
    {
        StartCoroutine(LoadTransitionCoroutine(state));
    }

    IEnumerator LoadTransitionCoroutine(States state)
    {
        transition.SetTrigger("SceneTransitionStart");

        yield return new WaitForSeconds(timeToChangeScene);

        SceneController.instance.ChangeState(state);

        transition.SetTrigger("SceneTransitionEnd");

    }

    void TriggerLoadSceneAnim(Scene s, LoadSceneMode l)
    {
        transition.SetTrigger("SceneTransitionEnd");
    }
}
