using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour {

	public static AudioPlayerController instance = null;

	private AudioSource audioPlayer;

	private void Awake()
    {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
		audioPlayer = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
