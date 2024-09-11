using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private Dictionary<SoundState, List<Sound>> DicoActualSound = new Dictionary<SoundState, List<Sound>>();


    private Queue<Sound> musicQueue = new Queue<Sound>();
    private Sound currentPlayingSound;

    private bool isStoppedManually = false;


    public static AudioManager instance;

    void Start()
    {
        // Garantit que ce GameObject ne sera pas détruit lorsque la scène est changée
        DontDestroyOnLoad(gameObject);
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = s.AudioMixer;

            if (!DicoActualSound.ContainsKey(s.ActualSound))
            {
                DicoActualSound.Add(s.ActualSound, new List<Sound>());
            }
            DicoActualSound[s.ActualSound].Add(s);

            if (s.AtStart)
                Play(s.Name);
        }
    }

    public void GenerateRandomMusicQueue(SoundState soundState)
    {
        if (!DicoActualSound.ContainsKey(soundState))
        {
            Debug.LogWarning("No sounds found for this SoundState: " + soundState);
            return;
        }

        List<Sound> availableSounds = new List<Sound>(DicoActualSound[soundState]); // Musiques correspondant au SoundState
        musicQueue.Clear(); // Vider la file actuelle

        while (availableSounds.Count > 0)
        {
            int randomIndex = Random.Range(0, availableSounds.Count);
            musicQueue.Enqueue(availableSounds[randomIndex]);
            availableSounds.RemoveAt(randomIndex);
        }
    }
    // Jouer la prochaine musique dans la file
    public void PlayNextInQueue()
    {
        if (isStoppedManually)
        {
            return; // Si la musique a été arrêtée manuellement, on ne joue plus rien
        }

        if (musicQueue.Count == 0) // Si toutes les musiques ont été jouées
        {
            Debug.Log("All songs have been played. Restarting the queue.");
            GenerateRandomMusicQueue(currentPlayingSound.ActualSound); // Recréer la file aléatoire pour le SoundState actuel
        }

        currentPlayingSound = musicQueue.Dequeue(); // Récupérer la prochaine musique
        currentPlayingSound.Source.Play();

        // Attendre la fin de la musique et passer à la suivante
        StartCoroutine(WaitForMusicToEnd(currentPlayingSound.Source.clip.length));
    }

    IEnumerator WaitForMusicToEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        PlayNextInQueue(); // Jouer la musique suivante
    }

    public void PlayMusicByState(SoundState soundState)
    {
        StopCurrentMusic(); // Arrêter toute musique en cours

        isStoppedManually = false; // Réinitialiser le flag de l'arrêt manuel
        GenerateRandomMusicQueue(soundState); // Générer une file pour ce SoundState
        PlayNextInQueue(); // Jouer la première musique dans la file
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.Source.Play();
    }

    public void Stop(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Stop();

        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void StopCurrentMusic()
    {
        if (currentPlayingSound != null && currentPlayingSound.Source.isPlaying)
        {
            currentPlayingSound.Source.Stop();
            StopAllCoroutines(); // Arrêter également la coroutine qui attend la fin de la musique
        }
        isStoppedManually = true;
    }

    public void PlayRandom(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Play();
        }
        else
        {
            throw new ArgumentNullException();
        }
    }

    public void Paused(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.Pause();

        }
        else
        {
            throw new ArgumentNullException();
        }
    }
    public void Unpaused(SoundState soundState)
    {
        if (DicoActualSound.ContainsKey(soundState))
        {
            int i = Random.Range(0, DicoActualSound[soundState].Count);

            Sound s = DicoActualSound[soundState][i];
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found");
                return;
            }
            s.Source.UnPause();

        }
        else
        {
            throw new ArgumentNullException();
        }
    }
}