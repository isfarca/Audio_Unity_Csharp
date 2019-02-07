using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    // Reference types
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private AudioMixer audioMixer;

    /// <summary>
    /// Get the components.
    /// </summary>
    private void Awake()
    {
        // Get the audio source component.
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update()
    {
        // By press the button, playing audio in the scene.
        if (Input.GetButtonDown("Jump"))
            PlayAudio();

        // By pressing plus button on key pad, than pitching the current audio.
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            audioMixer.SetFloat("MasterPitch", 1.5f);
	}

    /// <summary>
    /// Playing audio.
    /// </summary>
    public void PlayAudio()
    {
        // Random pitching the audio.
        audioSource.pitch = Random.Range(0.8f, 1.2f);

        // By more audios, play random audio, else play one audio from audio source.
        if (audioClips.Length > 0)
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        else
            audioSource.PlayOneShot(audioSource.clip);
    }
}
