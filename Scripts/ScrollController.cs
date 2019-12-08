using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class ScrollController : MonoBehaviour {

    public float scrollSpeed = 2;
    public float tileSizeZ = 15;

    int frame = 0;
    public Note notePrefab;
    int frequencyOfNotesCreation = 300;
    List<Note> notes;

    public AudioClip impact;
    AudioSource audioSource;

    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        notes = new List<Note>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        float timeOffset = Time.time * scrollSpeed;

        float newPosition = Mathf.Repeat(timeOffset, tileSizeZ);
        transform.position = startPosition - Vector3.forward * newPosition;


        if (frame % frequencyOfNotesCreation == 0)
        {
            Note note = Instantiate(notePrefab);
            notes.Add(note);
        }

        //Debug.Log(transform.parent.GetChild(0));
        try { 
        foreach(Note note in notes)
        {
            if(note.transform.position.z < -6)
            {
                notes.Remove(note);
                note.Die();
            }
            else if(Vector3.Distance(note.transform.position, transform.parent.GetChild(1).transform.position) < .1f)
            {

                    //note.GetComponent<ParticleSystem>().Play();
                    note.PlayAnimation();

                //notes.Remove(note);
                //note.Die();
                    
                //play sound    
                audioSource.PlayOneShot(impact, 1.0F);
                
                //Debug.Log("pLAYING NOTE");
            }
            else
            {
                note.transform.localPosition -= Time.deltaTime * scrollSpeed * Vector3.forward;
            }
        }
        }
        catch (Exception err)
        {
            Debug.Log(err.ToString());
        }
        frame++;
    }
}



