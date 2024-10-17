using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioClip[] tracksWithInterval1;    // First array for tracks with interval
    public AudioClip[] tracksWithInterval2;    // Second array for tracks with interval
    public AudioClip[] tracksWithoutInterval;  // Array for tracks without interval
    public AudioSource audioSource;           // Reference to AudioSource
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private int currentTrackWithIntervalIndex1 = 0;
    private int currentTrackWithIntervalIndex2 = 0;
    private int currentTrackWithoutIntervalIndex = 0;

    public float delayBetweenTracks1 = 15f;    // Time interval between tracks for first set in seconds
    public float delayBetweenTracks2 = 10f;    // Time interval between tracks for second set in seconds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayTracksWithInterval1());
        StartCoroutine(PlayTracksWithInterval2());
        StartCoroutine(PlayTracksWithoutInterval());
    }

    // Coroutine to play tracks from the first array with a delay between them
    IEnumerator PlayTracksWithInterval1()
    {
        while (true)
        {
            if (!audioSource.isPlaying)
            {
                if (tracksWithInterval1.Length > 0)
                {
                    // Play the current track
                    audioSource.clip = tracksWithInterval1[currentTrackWithIntervalIndex1];
                    audioSource.Play();

                    // Wait until the track finishes
                    yield return new WaitForSeconds(audioSource.clip.length);

                    // Wait for the interval before playing the next track
                    yield return new WaitForSeconds(delayBetweenTracks1);

                    // Move to the next track, looping back to the start if necessary
                    currentTrackWithIntervalIndex1 = (currentTrackWithIntervalIndex1 + 1) % tracksWithInterval1.Length;
                }
            }
            yield return null; // Wait for the next frame
        }
    }

    // Coroutine to play tracks from the second array with a delay between them
    IEnumerator PlayTracksWithInterval2()
    {
        while (true)
        {
            if (!audioSource1.isPlaying)
            {
                if (tracksWithInterval2.Length > 0)
                {
                    // Wait for the interval before playing the next track
                    yield return new WaitForSeconds(delayBetweenTracks2);
                    // Play the current track
                    audioSource1.clip = tracksWithInterval2[currentTrackWithIntervalIndex2];
                    audioSource1.Play();

                    // Wait until the track finishes
                    yield return new WaitForSeconds(audioSource.clip.length);

                    

                    // Move to the next track, looping back to the start if necessary
                    currentTrackWithIntervalIndex2 = (currentTrackWithIntervalIndex2 + 1) % tracksWithInterval2.Length;
                }
            }
            yield return null; // Wait for the next frame
        }
    }

    // Coroutine to play tracks without delay between them
    IEnumerator PlayTracksWithoutInterval()
    {
        while (true)
        {
            if (!audioSource2.isPlaying)
            {
                if (tracksWithoutInterval.Length > 0)
                {
                    // Play the current track
                    audioSource2.clip = tracksWithoutInterval[currentTrackWithoutIntervalIndex];
                    audioSource2.Play();

                    // Wait until the track finishes
                    yield return new WaitForSeconds(audioSource.clip.length);

                    // Move to the next track, looping back to the start if necessary
                    currentTrackWithoutIntervalIndex = (currentTrackWithoutIntervalIndex + 1) % tracksWithoutInterval.Length;
                }
            }
            yield return null; // Wait for the next frame
        }
    }
}