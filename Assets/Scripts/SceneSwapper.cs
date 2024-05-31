using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwapper : MonoBehaviour
{

    public void GameScene()
    {
        SceneManager.LoadScene("TransportGame");
    }

    public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
