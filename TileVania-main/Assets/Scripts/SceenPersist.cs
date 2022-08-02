using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceenPersist : MonoBehaviour
{
 private void Awake() 
    { 
    
        int numScenePersists = FindObjectsOfType<SceenPersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetPersists()
    {
        Destroy(gameObject);
    }
}
