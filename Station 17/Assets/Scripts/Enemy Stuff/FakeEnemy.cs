using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour
{
    public AudioSource moveNoise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInteract.instance.IsDoingLongAction())
        {
            if(!playing_noise)
                StartCoroutine(PlayNoise());
        }
        else
        {
            moveNoise.Stop();
        }
    }

    bool playing_noise = false;
    private IEnumerator PlayNoise()
    {
        playing_noise = true;



        yield return new WaitForSeconds(0.5f + Random.Range(0, 0.2f));

        moveNoise.Play();

        playing_noise = false;
    }
}
