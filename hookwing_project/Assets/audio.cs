using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public static AudioClip hit, jump, grappleSound, kick;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        hit = Resources.Load<AudioClip>("birdhurt");
        jump = Resources.Load<AudioClip>("JumpSound");
        kick = Resources.Load<AudioClip>("KickSound");
        grappleSound = Resources.Load<AudioClip>("spidertech");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "birdhurt":
                audioSource.PlayOneShot(hit);
                break;
            case "JumpSound":
                audioSource.PlayOneShot(jump);
                break;
            case "KickSound":
                audioSource.PlayOneShot(kick);
                break;
            case "spidertech":
                audioSource.PlayOneShot(grappleSound);
                break;

        }

    }
}
