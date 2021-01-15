using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string sceneToLoad;
    public float delayTime = 3;
    public bool clickToSkip = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime - .75f);
        //float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene(sceneToLoad);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (clickToSkip)
        {
            if (Input.anyKey || Input.anyKeyDown)
            {
                SceneManager.LoadScene(sceneToLoad);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}