using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	// List of the sound
	public Sound[] sounds;
	// Sound is on or not
	public static bool soundIsOn = true;

	void Awake()
	{
		// So the song will continue (singleton pattern)
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		// Add audio source each sound object
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = false;
		}
	}

	public void TurnOffSound()
    {
		// Set bool
		soundIsOn = false;
		// Turn off every active sound
		foreach (Sound s in sounds)
		{
			if (s.source.isPlaying == true)
			{
				s.source.Pause();
			}
		}
	}

	public void TurnOnSound()
    {
		// Set bool
		soundIsOn = true;
		// Start BGM again
		Play("BGM");
	}

	// This method is to play sound
	public void Play(string sound)
	{
		// Only if sound is turned on
		if (soundIsOn)
		{
			// Find the sound based on given string
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				// Print warning if sound not found
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}
			// Adjust pitch and volume
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			
			// Check the sound and Play
			if(sound == "BGM" && !s.source.isPlaying)
            {
				s.source.Play();
            }
            else if (sound != "BGM")
            {
				s.source.Play();
			}
			
		}
	}

	// Method to stop sound
	public void Stop(string sound)
	{
		if (soundIsOn)
		{
			Sound s = Array.Find(sounds, item => item.name == sound);
			if (s == null)
			{
				Debug.LogWarning("Sound: " + name + " not found!");
				return;
			}

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.Stop();
		}
	}
}
