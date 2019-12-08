using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    public Vector3 startPosition;
    public ParticleSystem2 particleSystemPrefab;
    ParticleSystem2 particleSystem2;

    // Use this for initialization
    void Start () {
        Random rand = new Random();
        int x = Random.Range(-1, 1);

        startPosition = new Vector3(1.5f + x, 2, 2.5f);

        ///startPosition = new Vector3();
        //startPosition = transform.position;
        transform.position = startPosition;
        //GetComponent<ParticleSystem>().Pause();
    }
	
	// Update is called once per frame
	void Update () {
        //float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        //transform.position = startPosition - Vector3.forward * newPosition;
    }

    public void PlayAnimation()
    {
        particleSystem2 = Instantiate(particleSystemPrefab);
        particleSystem2.transform.SetParent(transform);
    }

    public void Die()
    {
        if(particleSystem2 != null)
        {
            particleSystem2.GetComponent<ParticleSystem>().Pause();
            particleSystem2.Die();
        }
        Destroy(this.gameObject);
    }
}
