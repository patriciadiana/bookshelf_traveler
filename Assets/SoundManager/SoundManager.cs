using UnityEngine;

public enum SoundType
{
    NONE = -1,
    F_DAMAGE,
    F_FOOTSTEPS,
    F_HITTING_ANVIL,
    F_HITTING_ROCK,
    F_ITEM_EQUIP,
    F_SLIME_DEATH,
    F_SLIME_MOVE,
    F_SWORD_DASH,
    F_DRAGON_WINGS,
    F_FIREBALL,
    F_FOOTSTEPS_DRAGON,
    F_JUMP,
    F_TAKE_DAMAGE,
    C_DRAWER_OPEN,
    C_FOOTSTEPS,
    C_LAMP_SWITCH,
    C_PAPER_INTERACTION,
    SF_SPACESHIP_BULLET,
    SF_ENEMY_BULLET,
    SF_ALARM,
    SF_DAMAGE
}

public enum MusicType
{
    TITLE_THEME,
    FANTASY_AMBIENT,
    CAVE_AMBIENT,
    DRAGON_BATTLE,
    DRAGON_VICTORY,
    CRIME_AMBIENT,
    SF_AMBIENT,
    SF_BOSS_BATTLE,
    GAME_OVER
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioClip pausedClip;
    private float pausedTime;

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource voiceSource;

    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private AudioClip[] musicList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        sfxSource.Stop();
        musicSource.Stop();
        voiceSource.Stop();
    }


    private void Start()
    {
        SoundManager.PlayMusic(MusicType.TITLE_THEME);
    }

    public static void PlaySound(SoundType sound)
    {
        if (sound == SoundType.NONE)
            return;

        if (Instance.soundList.Length > (int)sound)
        {
            Instance.sfxSource.PlayOneShot(
                Instance.soundList[(int)sound]
            );
        }
        else
        {
            Debug.LogWarning("Sound clip not found for: " + sound);
        }
    }

    public static void PlayMusic(MusicType music)
    {
        if (Instance.musicList.Length > (int)music)
        {
            Instance.musicSource.clip = Instance.musicList[(int)music];
            Instance.musicSource.volume = 1f;
            Instance.musicSource.loop = true;
            Instance.musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found for: " + music);
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            pausedClip = musicSource.clip;      
            pausedTime = musicSource.time;      
            musicSource.Pause();
        }
    }

    public static void PlayVoice(AudioClip clip, float pitch)
    {
        if (clip != null)
        {
            Instance.voiceSource.pitch = pitch + Random.Range(-0.05f, 0.05f);
            Instance.voiceSource.PlayOneShot(clip);
        }
    }

    public void ResumeMusic()
    {
        if (pausedClip != null)
        {
            musicSource.clip = pausedClip;      
            musicSource.time = pausedTime;      
            musicSource.Play();
            pausedClip = null;                  
        }
    }

    public void PlayLoopSound(SoundType sound)
    {
        if (Instance.soundList.Length > (int)sound)
        {
            Instance.sfxSource.clip = Instance.soundList[(int)sound];
            Instance.sfxSource.loop = true;
            Instance.sfxSource.Play();
        }
    }

    public void StopLoopSound()
    {
        Instance.sfxSource.loop = false;
        Instance.sfxSource.Stop();
        Instance.sfxSource.clip = null;
    }
}