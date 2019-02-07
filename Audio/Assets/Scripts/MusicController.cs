using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Reference types
    private AudioSource audioSource;

    /// <summary>
    /// Get the components.
    /// </summary>
    private void Awake()
    {
        // Get the audio source.
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
    {
        // By press the button, stop or play the audio.
        if (Input.GetButtonDown("Jump"))
        {
            // When the audio playing, than stop the audio, else play the audio.
            if (audioSource.isPlaying && audioSource.volume > 0)
                Stop();
            else
                Play();
        }
	}

    /// <summary>
    /// Stop the audio.
    /// </summary>
    private void Stop()
    {
        // Decrease the audio volume to zero.
        StartCoroutine(FadeTo(0f, 2f));
    }

    /// <summary>
    /// Play the audio.
    /// </summary>
    private void Play()
    { 
        // Increase the audio volume to one.
        StartCoroutine(FadeTo(1f, 1f));
    }

    /// <summary>
    /// Increase or Decrease the audio volume. 
    /// </summary>
    /// <param name="targetVolume">The current audio volume by calling.</param>
    /// <param name="duration">Duration for increase/decrease.</param>
    /// <returns>By ending the operation.</returns>
    private IEnumerator FadeTo(float targetVolume, float duration)
    {
        // Declare variables
        float currentVolume = audioSource.volume;

        // Increasing or Decreasing the audio volume to end this operation.
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(currentVolume, targetVolume, t / duration);

            // The operation to end.
            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = targetVolume;
    }
}
