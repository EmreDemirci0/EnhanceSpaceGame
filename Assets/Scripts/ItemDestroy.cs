using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour
{
    /**/
    GameController gc;
    scoreController score;
    [SerializeField] ParticleSystem BoomParticleRed, BoomParticleGreen;
    AudioSource source;
    [SerializeField]AudioClip throwObjectClip;//deðiþcek
    [SerializeField]List<AudioClip> unsuccessHit;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        score = gc.GetComponent<scoreController>();
        source = GameObject.FindGameObjectWithTag("SoundController").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (gc.whichRedObject.name == other.name && gc.isLeft)
        {   //mixkit,
            print("a");
            source.clip = throwObjectClip;
            source.Play();
            Destroy(other.gameObject, .15f);
            score.WinScore(1);
            BoomParticleRed.Play();

        }
        else if (gc.whichGreenObject.name == other.name && !gc.isLeft)
        {
            print("b");
            source.clip = throwObjectClip;
            source.Play();
            Destroy(other.gameObject, .15f);
            score.WinScore(1);
            BoomParticleGreen.Play();
        }
        else
        {   
            score.LoseScore(1);
            int random = Random.Range(0, unsuccessHit.Count);
            source.clip = unsuccessHit[random];
            source.Play();
            other.GetComponent<Collider>().isTrigger = false;
            //print("boom");

            other.GetComponent<Rigidbody>().drag= 1f;

        }
    }
}
