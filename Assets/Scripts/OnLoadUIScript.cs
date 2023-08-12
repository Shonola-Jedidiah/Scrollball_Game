using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLoadUIScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTime());
    }

    
    IEnumerator LoadTime()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level_1");
    }
}
