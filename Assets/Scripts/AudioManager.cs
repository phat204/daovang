using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, sfxSource;
    public AudioClip backgroundSound, congTienSound, dongHoSound, thuaSound, keoSound, mucTieuSound, noMinSound, thangSound;

    public static AudioManager Instance{get; private set;}
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void PlayLoop(AudioSource source, AudioClip clip, float volume)
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }

    public void PlayBackgroundSound() {
        PlayLoop(musicSource, backgroundSound, 0.5f);
    }

    public void StopBackgroundSound() {
        musicSource.Stop();
    }

    private void PlaySFX(AudioClip clip) {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);    
        }
    }

    public void PlayCongTienSound() {
        PlaySFX(congTienSound);
    }

    public void PlayDongHoSound() {
        PlaySFX(dongHoSound);
    }

    public void PlayThuaSound() {
        PlaySFX(thuaSound);
    }

    public void PlayKeoSound() {
        PlaySFX(keoSound);
    }

    public void PlayMucTieuSound() {
        PlaySFX(mucTieuSound);
    }
    public void PlayNoMinSound() {
        PlaySFX(noMinSound);
    }

    public void PlayThangSound() {
        PlaySFX(thangSound);
    }
}
