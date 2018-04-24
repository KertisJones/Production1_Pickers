using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{

    public string sceneToLoad;
    public float delayTime = 3;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime - .75f);
        //float fadeTime = GameObject.Find("_GM").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene(sceneToLoad);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}