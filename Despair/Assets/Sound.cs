using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public class Sound
    {
        private string _audioFile;
        private bool _playing;
        private System.Media.SoundPlayer _soundPlayer;   

        public string AudioFile
        {
            get { return _audioFile; }
            set { _audioFile = value; }
        }
        public bool Playing
        {
            get { return _playing; }
            set { _playing = value;  }
        }
        public System.Media.SoundPlayer SoundPlayer
        {
            get { return _soundPlayer; }
            set { _soundPlayer = value; }
        }

        /// <summary>
        /// Constructor -- Initialize the Sound Player
        /// </summary>
        /// <param name="audioFile"></param>
        public Sound(string audioFile)
        {
            _audioFile = audioFile;
            _soundPlayer = new System.Media.SoundPlayer(_audioFile);
        }

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="audioPath"></param>
        public void playSound(bool loop)
        {
            try
            {
                _soundPlayer.Play();
                if (loop == true)
                {
                    _soundPlayer.PlayLooping();
                    _playing = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing Sound File: {_audioFile} \n");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Stop a Sound and Dispose of it
        /// </summary>
        public void stopSound(bool dispose)
        {
            _soundPlayer.Stop();
            _playing = false;
            if (dispose == true) _soundPlayer.Dispose();
        }
    }
}
