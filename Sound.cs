using System;
using Tao.Sdl;

public class Sound
{
   
    IntPtr pointer;

    
    public Sound(string nombreFichero)
    {
        pointer = SdlMixer.Mix_LoadMUS(nombreFichero);
    }

    // Reproducir una vez
    public void PlayOnce()
    {
        SdlMixer.Mix_PlayMusic(pointer, 1);
    }

   
    public void Play()
    {
        SdlMixer.Mix_PlayMusic(pointer, -1);
    }

    
    public void Stop()
    {
        SdlMixer.Mix_HaltMusic();
    }
}
