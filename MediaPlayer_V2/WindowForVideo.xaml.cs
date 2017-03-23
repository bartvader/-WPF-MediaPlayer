using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaPlayer_V2
{
    /// <summary>
    /// Interaction logic for WindowForVideo.xaml
    /// </summary>
    public partial class WindowForVideo : Window
    {
        public WindowForVideo()
        {
            InitializeComponent();
        }
        public bool IsPaused { get; set; }
        public void Play()
        {
            media.Play();
        }
        public void Pause()
        {
            media.Pause();
        }
        public void Stop()
        {
            media.Stop();
        }
        public void setVolume(double new_volume)
        {
            media.Volume = new_volume;
        }
        public void setPlayPosition(TimeSpan pos)
        {
            media.Position = pos;
        }
        public void setSource(string path)
        {
            media.Source = new Uri(path);
        }
        public TimeSpan getPosition()
        {
            return media.Position;
        }
        public Duration getDuration()
        {
            return media.NaturalDuration;
        }
    }
}
