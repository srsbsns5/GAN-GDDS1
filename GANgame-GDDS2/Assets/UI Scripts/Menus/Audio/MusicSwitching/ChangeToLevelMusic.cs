using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToLevelMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("Level");
        AudioManager.instance.Stop("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
