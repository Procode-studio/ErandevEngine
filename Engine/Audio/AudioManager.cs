using NAudio.Wave;

namespace ErandevEngine.Audio;

public class AudioManager
{
    private WaveOutEvent? waveOut;
    private Mp3FileReader? mp3Reader;
    public void PlaySound(string soundName)
    {
        waveOut = new WaveOutEvent();
        mp3Reader = new Mp3FileReader(soundName);
        waveOut.Init(mp3Reader);
        waveOut.Play();
    }
    public void StopSound(string soundName)
    {
        waveOut = new WaveOutEvent();
        mp3Reader = new Mp3FileReader(soundName);
        waveOut.Stop();
        mp3Reader.Position = 0;
        if (waveOut != null)
        {
            waveOut.Dispose();
            waveOut = null;
        }
        if (mp3Reader != null)
        {
            mp3Reader.Dispose();
            mp3Reader = null;
        }
    }
}
