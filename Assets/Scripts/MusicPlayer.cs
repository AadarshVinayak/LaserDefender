using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [System.Obsolete]
    // Start is called before the first frame update
    void Awake()
    {
        int musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
