using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void ReloadLevel()
    {
        int currentSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneInd);
    }
}
