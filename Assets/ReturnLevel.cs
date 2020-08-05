using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnLevel : MonoBehaviour
{

    private int previousSceneLoad;

    // Start is called before the first frame update
    void Start()
    {
        previousSceneLoad = SceneManager.GetActiveScene().buildIndex + 2;
       // Debug.Log(SceneManager.GetActiveScene().buildIndex);

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(previousSceneLoad);
    }
}
