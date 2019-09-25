using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public AudioClip[] Tracks;
    AudioSource BGMusic;
    AudioSource[] Temps = new AudioSource[10];
    public float FadingTime;
    public GameDataManager DataManager;
    public float BGMusicVolume, TempMusicVolume;
    public float BGPlayDelayTime = 1;


    public static MusicController ControllerInstance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (ControllerInstance == null)
        {
            ControllerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        BGMusic = sources[0];
        BGMusic.clip = Tracks[14];
        for (int i = 0; i < Temps.Length; i++)
        {
            Temps[i] = sources[i + 1];
        }

    }
	void Start () {
        DataManager = GameDataManager.ManangerInstance;

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayMusic(float Delay)
    {
        if (DataManager.MusicOnOff)
        {
            if(!BGMusic.isPlaying)
            {
                BGMusic.volume = 0;
                BGMusic.PlayDelayed(Delay);
                BGMusic.loop = true;
                iTween.AudioTo(gameObject, BGMusicVolume,1, FadingTime);
            }
        }
    }

    IEnumerator FadeAudio2Stop()
    {
        iTween.AudioTo(gameObject, 0, 1, FadingTime);
        yield return new WaitForSeconds(FadingTime);
        BGMusic.Stop();
        foreach (var item in Temps)
        if (item.isPlaying)
            item.Stop();

    }

    public void StopMusic()
    {
        if (!DataManager.MusicOnOff)
        {
            if (BGMusic.isPlaying)
            {
                BGMusic.volume = BGMusicVolume;
                StartCoroutine(FadeAudio2Stop());
            }
        }
    }

    public void PlayTempMusic(int TrackNo)
    {
        if (DataManager.MusicOnOff)
        {
            foreach (var item in Temps)
            {
                if(!item.isPlaying)
                {
                    item.volume = TempMusicVolume;
                    item.clip = Tracks[TrackNo];
                    item.Play();
                    //Debug.Log("Played " + TrackNo);
                    break;
                }

            }

        }
    }


    public void PlayBombTempMusic(int TrackNo) //This Function Made because of a Fucking Bug
    {
        if (DataManager.MusicOnOff)
        {
            foreach (var item in Temps)
            {
                if (item.clip == Tracks[TrackNo] && item.isPlaying)
                    return;
            }
            foreach (var item in Temps)
            {
                if (!item.isPlaying)
                {
                    item.volume = TempMusicVolume;
                    item.clip = Tracks[TrackNo];
                    item.Play();
                    break;
                }

            }

        }
    }

    public void StopAllTempSounds()
    {
        foreach (var item in Temps)
        {
            item.Stop();
        }
    }


    public void PlayTempMusicDelayed(int TrackNo,float Delay)
    {
        if (DataManager.MusicOnOff)
        {
            foreach (var item in Temps)
            {
                if (!item.isPlaying)
                {
                    item.volume = TempMusicVolume;
                    item.clip = Tracks[TrackNo];
                    item.PlayDelayed(Delay);
                    break;
                }

            }

        }
    }

}
