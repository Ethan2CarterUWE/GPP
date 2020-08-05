using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceLevel : MonoBehaviour
{
    private int nextSceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;   
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextSceneLoad);
    }
}
