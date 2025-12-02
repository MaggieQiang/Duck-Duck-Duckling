using UnityEngine;
using UnityEngine.Audio;
public class Audio : MonoBehaviour
{

    public static Audio Instance { get; private set; }
    [SerializeField] AudioSource defaultSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioMixer audioMixer;
    public AudioClip background;
    public AudioClip shooting;
    public AudioClip yielding;
    public AudioClip death;
    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    public AudioClip kill;
    public AudioClip slurp;
    public AudioClip lose;
    public AudioClip win;
    [SerializeField] private string parameterName = "BGMVolume";

    public void SetVolume(float value)
    {
        float dB = value > 0.0001f ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat(parameterName, dB);
    }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (defaultSource != null && background != null)
        {
            defaultSource.clip = background;
            defaultSource.loop = true;
            defaultSource.Play();
        }
    }
    
    public void ShootSound()
    {
        if (sfxSource != null && shooting != null)
        {
            sfxSource.PlayOneShot(shooting);
        }
    }

    public void YieldSound()
    {
        if (sfxSource != null && yielding != null)
        {
            sfxSource.PlayOneShot(yielding);
        }
    }
    
    public void DeathSound()
    {
        if (sfxSource != null && death != null)
        {
            sfxSource.PlayOneShot(death);
        }
    }

    public void LevelUpSound(int level)
    {
        AudioClip clip = null;
        
        switch (level)
        {
            case 1: clip = level1; break;
            case 2: clip = level2; break;
            case 3: clip = level3; break;
        }
        
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void KillSound()
    {
        if (sfxSource != null && kill != null)
        {
            sfxSource.PlayOneShot(kill);
        }
    }

    public void SlurpSound()
    {
        if (sfxSource != null && slurp != null)
        {
            sfxSource.PlayOneShot(slurp);
        }
    }

    public void WinSound()
    {
        if (sfxSource != null && win != null)
        {
            sfxSource.PlayOneShot(win);
        }
    }
    public void GameOverSound()
    {
        if (sfxSource != null && lose != null)
        {
            sfxSource.PlayOneShot(lose);
        }
    }

    public void ChangeVolume(float volume)
    {
        defaultSource.volume = volume;
    }
}
