using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI codeText;
    string codeTextValue = "";
    public string passCode;
    bool canAddDigit;

    AudioSource audioSource;
    public AudioClip[] feedback;

    public ItemSpawn itemSpawn; //refer from itemSpawn
    public LockDoor doorLock; //refer from LockDoor

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        codeText.text = codeTextValue;

        if (codeTextValue.Length == 4)
        {
            canAddDigit = false;
        }
        else
        {
            canAddDigit = true;
        }

        
    }

    public void AddDigit(string digit)
    {
        if (canAddDigit)
        {
            codeTextValue += digit;
            audioSource.clip = feedback[2];
            audioSource.Play();
        }
    }
     public void ClearNumber()
    {
        codeTextValue = "";
        itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[3]);
    }

    public void EnterCode()
    {
        if (codeTextValue == passCode)
        {
            itemSpawn.enabled = true;
            itemSpawn.Spawn();
            doorLock.isLocked = false;
            itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[1]);
            codeTextValue = "";
            
        }
        else
        {
            itemSpawn.gameObject.GetComponent<AudioSource>().PlayOneShot(feedback[0]);
            codeTextValue = "";
            
        }
    }
}
