using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Xml.Linq;
using System.IO;
using TagLib;
using System.Globalization;
using System.Collections;
using System.Data;
using System.Windows.Threading;
using System.ComponentModel;
namespace MediaPlayer_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XDocument doc;
        string ApplicationFolder = "";
        string file_name = "playlists.xml";
        StylePicker style_picker; 
        WindowForVideo winForVid = new WindowForVideo();
        bool isPaused = false;
        bool isMusic = false;
        bool isPlaying = false;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public class PlaylistItem
        {
            public string TITLE { get; set; }
            public string ARTIST { get; set; }
            public string LENGHT { get; set; }
            public string ALBUM { get; set; }
            public string PATH { get; set; }
        }
        public MainWindow()
        {
            this.InitializeComponent();
            INFO.Text = "Приятного прослушивания!^_^";
            style_picker = new StylePicker(this);
            
            VOLUME.Value = 0.5;
            winForVid.IsPaused = false;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            DataGridTextColumn title = new DataGridTextColumn();
            DataGridTextColumn artist = new DataGridTextColumn();
            DataGridTextColumn lenght = new DataGridTextColumn();
            DataGridTextColumn album = new DataGridTextColumn();
            DataGridTextColumn path = new DataGridTextColumn();
            title.Header = "Title";
            artist.Header = "Artist";
            lenght.Header = "Lenght";
            album.Header = "Album";
            path.Header = "Path";

            path.Binding = new Binding("PATH");
            title.Binding = new Binding("TITLE");
            artist.Binding = new Binding("ARTIST");
            lenght.Binding = new Binding("LENGHT");
            album.Binding = new Binding("ALBUM");
            curr_play.Columns.Add(title);
            curr_play.Columns.Add(artist);
            curr_play.Columns.Add(lenght);
            curr_play.Columns.Add(album);
            curr_play.Columns.Add(path);
            curr_play.ColumnWidth = DataGridLength.Auto;
            if (System.IO.File.Exists(file_name))
            {
                doc = XDocument.Load(file_name);

                foreach (var item in doc.Element("Root").Elements("Playlist"))
                {
                    list.Items.Add(item.Element("Name").Value);
                }
            }
            else
            {
                doc = new XDocument();
                doc.Add(new XElement("Root"));
                doc.Element("Root").Add(new XElement("Style"));
                doc.Element("Root").Element("Style").Value = "mainstyle.xaml";
                doc.Save(file_name);
            }
            string proj_path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string[] se = proj_path.Split('\\');
            ApplicationFolder = proj_path.Remove(proj_path.Length - se[se.Length - 1].Length);
            this.Resources = new ResourceDictionary() { Source = new Uri((System.IO.Path.Combine(ApplicationFolder, doc.Element("Root").Element("Style").Value.ToString()))) };
            winForVid.Closing += new CancelEventHandler(winForVid_Closing);
            winForVid.Closed += new EventHandler(winForVid_Closed);
            winForVid.IsVisibleChanged += new DependencyPropertyChangedEventHandler(winForVid_IsVisibleChanged);
            style_picker.Closing +=new CancelEventHandler(style_picker_Closing);
           
        }
        private void STYLE_Click(object sender, RoutedEventArgs e)
        {
            style_picker = new StylePicker(this);
            style_picker.Show();
        }
        private void winForVid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            INFO.Text = "";
        }
        public void StyleChange(string style_file_name)//ЗДЕСЬ ИЗМЕНЯЮ СТИЛЬ
        {
           // MessageBox.Show(System.IO.Path.Combine(ApplicationFolder, style_file_name));
            this.Resources = new ResourceDictionary() { Source = new Uri((System.IO.Path.Combine(ApplicationFolder/*объединяю путь к проекту с именем словаря сттилей*/, style_file_name))) };
            doc.Element("Root").Element("Style").Value = style_file_name;
            doc.Save(file_name);
        }
        private void MAIN_BORDER_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void style_picker_Closing(object sender, EventArgs e)
        {
            //style_picker.Close();

        }
        private void winForVid_Closed(object sender, EventArgs e)
        {
            CURR_POS.Content = "00:00:00";
            INFO.Text = "";
            dispatcherTimer.Stop();
        }
        private void winForVid_Closing(object sender, EventArgs e)
        {
           // PLAY_POSITION.Value = 123;
            CURR_POS.Content = "00:00:00";
            INFO.Text = "";
            winForVid.Stop();
            dispatcherTimer.Stop();
        
        }
        private void MIN_BTN_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MAX_BTN_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void EXIT_BTN_Click(object sender, RoutedEventArgs e)
        {
            CloseAllWindows();
        }
        private void XMLAddItem(PlaylistItem pi)
        {
            if (list.SelectedItem != null)
            {
                foreach (var item in doc.Element("Root").Elements("Playlist"))
                {
                    if (item.Element("Name").Value.Equals(list.SelectedItem as string))
                    {
                        //item.Element("Items").Add(new XElement())
                        XElement new_item = new XElement("Item");
                        new_item.Add(new XElement("Title"));
                        new_item.Add(new XElement("Artist"));
                        new_item.Add(new XElement("Lenght"));
                        new_item.Add(new XElement("Album"));
                        new_item.Add(new XElement("Path"));
                        new_item.Element("Title").Value = pi.TITLE;
                        new_item.Element("Artist").Value = pi.ARTIST;
                        new_item.Element("Lenght").Value = pi.LENGHT;
                        new_item.Element("Album").Value = pi.ALBUM;
                        new_item.Element("Path").Value = pi.PATH;
                        item.Element("Items").Add(new_item);
                        doc.Save(file_name);
                    }
                }
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new_playlist_name.Visibility = Visibility.Visible;
            OK.Visibility = Visibility.Visible;
        }
        private void AddItemIntoPlaylist()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            TagLib.File file;
            PlaylistItem tmp;
            if (list.SelectedItem != null)
            {
                if (ofd.ShowDialog() == true)
                {
                    string extention = System.IO.Path.GetExtension(ofd.FileName);
                    if (extention.Equals(".mp3"))
                    {
                        file = new TagLib.Mpeg.AudioFile(ofd.FileName);
                        if (file.Tag.Album != null && file.Tag.Title != null && file.Tag.FirstPerformer != null && file.Properties.Duration.TotalSeconds != 0)
                        {
                            tmp = new PlaylistItem
                            {
                                ALBUM = file.Tag.Album,
                                TITLE = file.Tag.Title,
                                ARTIST = file.Tag.FirstPerformer,
                                LENGHT = file.Properties.Duration.Hours.ToString() + ":" + file.Properties.Duration.Minutes.ToString() + ":" + file.Properties.Duration.Seconds,
                                PATH = ofd.FileName
                            };
                            curr_play.Items.Add(tmp);
                            XMLAddItem(tmp);
                        }

                    }
                    else if (extention.Equals(".avi") || extention.Equals(".mp4") || extention.Equals(".wmv"))
                    {
                        file = TagLib.File.Create(ofd.FileName);
                        if (file.Properties.Duration.TotalSeconds != 0)
                        {
                            tmp = new PlaylistItem
                            {
                                ALBUM = "<videofile>",
                                TITLE = System.IO.Path.GetFileName(ofd.FileName),
                                ARTIST = "<videofile>",
                                LENGHT = file.Properties.Duration.Hours.ToString() + ":" + file.Properties.Duration.Minutes.ToString() + ":" + file.Properties.Duration.Seconds,
                                PATH = ofd.FileName
                            };
                            curr_play.Items.Add(tmp);
                            XMLAddItem(tmp);
                        }
                    }
                    else if (extention.Equals(".png") || extention.Equals(".gif") || extention.Equals(".jpg") || extention.Equals(".jpeg"))
                    {
                        if (System.IO.Path.GetFileName(ofd.FileName) != null)
                        {
                            tmp = new PlaylistItem
                            {
                                ALBUM = "<image>",
                                TITLE = System.IO.Path.GetFileName(ofd.FileName),
                                ARTIST = "<image>",
                                LENGHT = "<image>",
                                PATH = ofd.FileName
                            };
                            curr_play.Items.Add(tmp);
                            XMLAddItem(tmp);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить файл!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Нет выбраных плейлистов!");
            }
        }
        private void DeleteItemFromPlaylist(string song_name)
        {
            foreach (var item in doc.Element("Root").Elements("Playlist"))
            {
                if (item.Element("Name").Value.Equals(list.SelectedItem as String))
                {
                    foreach (var item1 in item.Element("Items").Elements("Item"))
                    {
                        if (item1.Element("Title").Value == song_name)
                        {
                            item1.Remove();
                            curr_play.Items.RemoveAt(curr_play.SelectedIndex);
                        }
                    }
                }
            }
            doc.Save(file_name);
        }
        private void DeletePlaylist(string playlist_name)
        {
            if (list.SelectedItem != null)
            {
                foreach (var item in doc.Element("Root").Elements("Playlist"))
                {
                    if (item.Element("Name").Value == playlist_name)
                    {
                        item.Remove();
                        list.Items.RemoveAt(list.SelectedIndex);
                        curr_play.Items.Clear();
                    }
                }
            }
            doc.Save(file_name);
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (new_playlist_name.Text != "")
            {
                string name = new_playlist_name.Text;
                list.Items.Add(name);
                XElement playlist = new XElement("Playlist");
                playlist.Add(new XElement("Name"));
                playlist.Add(new XElement("Items"));
                playlist.Element("Name").Value = name;
                doc.Element("Root").Add(playlist);
                doc.Save(file_name);
                new_playlist_name.Text = "";
                OK.Visibility = Visibility.Hidden;
                new_playlist_name.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Нет выбраных плейлистов!");
            }
        }
        private void Unpause()
        {

                if (winForVid.IsVisible)
                {

                    winForVid.setPlayPosition(new TimeSpan(0, 0, (int)((PLAY_POSITION.Value * winForVid.getDuration().TimeSpan.TotalSeconds) / PLAY_POSITION.Maximum)));
                    winForVid.Play();
                    dispatcherTimer.Start();
                    winForVid.IsPaused = false;
                }
                else
                {
                    media.Position = new TimeSpan(0, 0, (int)((PLAY_POSITION.Value * media.NaturalDuration.TimeSpan.TotalSeconds) / PLAY_POSITION.Maximum));
                    media.Play();
                    dispatcherTimer.Start();
                    isPaused = false;
                }
        }
        private void StartPlay()
        {
            
            //CURR_POS.Content = "00:00:00";
            if (curr_play.SelectedItem != null)
            {
                object item = curr_play.SelectedItem;
                string path = (curr_play.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                if (System.IO.File.Exists(path))
                {
                    isPlaying = true;
                    INFO.Text = System.IO.Path.GetFileName((curr_play.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                    if (System.IO.Path.GetExtension(path) == ".mp3")
                    {

                        if (winForVid.IsVisible == true)
                        {
                            winForVid.Close();
                        }
                        CURR_POS.Content = "00:00:00";
                        media.Source = new Uri(path);
                        media.Volume = VOLUME.Value;
                        media.Play();
                        isMusic = true;
                        dispatcherTimer.Start();
                    }
                    else
                    {
                        if (winForVid.IsVisible == false)
                        {

                            winForVid = new WindowForVideo();
                            if (isMusic == true)
                            {
                                media.Stop();
                            }
                            winForVid.setVolume(VOLUME.Value);
                            winForVid.setSource(path);
                            winForVid.Show();
                            winForVid.Play();
                            isMusic = false;
                            dispatcherTimer.Start();
                        }
                        else
                        {
                            dispatcherTimer.Stop();
                            winForVid.setVolume(VOLUME.Value);
                            winForVid.setSource(path);
                            winForVid.Play();
                            dispatcherTimer.Start();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Файл по данному пути не существует!");
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItemIntoPlaylist();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                curr_play.ColumnWidth = DataGridLength.Auto;

                curr_play.Items.Clear();
                curr_play.ColumnWidth = DataGridLength.Auto;
                foreach (var item in doc.Element("Root").Elements("Playlist"))
                {
                    if (item.Element("Name").Value.Equals(list.SelectedItem as string))
                    {
                        foreach (var song in item.Element("Items").Elements("Item"))
                        {
                            curr_play.Items.Add(new PlaylistItem
                            {
                                TITLE = song.Element("Title").Value,
                                ARTIST = song.Element("Artist").Value,
                                LENGHT = song.Element("Lenght").Value,
                                ALBUM = song.Element("Album").Value,
                                PATH = song.Element("Path").Value,
                            });
                        }
                    }
                }
            }
        }
        private void PLAY_Click(object sender, RoutedEventArgs e)
        {
            if (winForVid.IsVisible)
            {
                if (winForVid.IsPaused == true)
                {
                    Unpause();
                    return;
                }
            }
            else
            {
                if (isPaused == true)
                {
                    Unpause();
                    return;
                }
            }
            StartPlay();
        }
        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }
        private void PAUSE_Click(object sender, RoutedEventArgs e)
        {
            if (winForVid.IsVisible)
            {
                winForVid.Pause();
                winForVid.IsPaused = true;
                dispatcherTimer.Stop();
            }
            else
            {
                media.Pause();
                dispatcherTimer.Stop();
                isPaused = true;
            }
        }
        private void STOP_Click(object sender, RoutedEventArgs e)
        {
            if (winForVid.IsVisible)
            {
                winForVid.Stop();
                isPlaying = false;
                winForVid.Close();
                CURR_POS.Content = "00:00:00";
                PLAY_POSITION.Value = PLAY_POSITION.Minimum;
                INFO.Text = "";
            }
            else
            {
                isPlaying = false;
                media.Stop();
                CURR_POS.Content = "00:00:00";
                PLAY_POSITION.Value = PLAY_POSITION.Minimum;
                INFO.Text = "";
            }
            dispatcherTimer.Stop();
           // CURR_POS.Content = "00:00:00";
          
        }
        private void BACK_Click(object sender, RoutedEventArgs e)
        {
            if (curr_play.SelectedItem != null)
            {
                if (curr_play.SelectedIndex - 1 >= 0)
                {
                    curr_play.SelectedIndex -= 1;
                    curr_play.CurrentItem = curr_play.Items[curr_play.SelectedIndex];
                    if (isPlaying == true)
                    {
                        media.Stop();
                        if (winForVid.IsVisible)
                        {
                            winForVid.Stop();
                        }
                    }
                    StartPlay();
                }
            }
        }

        private void FORWARD_Click(object sender, RoutedEventArgs e)
        {
            if (curr_play.SelectedItem != null)
            {

                if (curr_play.SelectedIndex + 1 <= curr_play.Items.Count)
                {
                    curr_play.SelectedIndex += 1;
                    curr_play.CurrentItem = curr_play.Items[curr_play.SelectedIndex];
                    if (isPlaying == true)
                    {
                        media.Stop();
                        if (winForVid.IsVisible)
                        {
                            winForVid.Stop();
                        }
                    }
                    StartPlay();
                }
            }
        }
        private void VOLUME_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (winForVid.IsVisible)
            {
                winForVid.setVolume(VOLUME.Value);
            }
            else
            {
                media.Volume = (double)VOLUME.Value;
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (winForVid.IsVisible)
            {

                if (winForVid.getDuration().HasTimeSpan)
                {
                    PLAY_POSITION.Value = (winForVid.getPosition().TotalSeconds * PLAY_POSITION.Maximum) / winForVid.getDuration().TimeSpan.TotalSeconds;
                    CURR_POS.Content =
                    winForVid.getDuration().TimeSpan.Hours.ToString() + ":" +
                    winForVid.getDuration().TimeSpan.Minutes.ToString() +
                    ":" + winForVid.getDuration().TimeSpan.Seconds.ToString() +
                    "/" + winForVid.getPosition().Hours.ToString() + ":" +
                    winForVid.getPosition().Minutes.ToString() +
                    ":" + winForVid.getPosition().Seconds.ToString();
                }
                else
                {
                    CURR_POS.Content = "00:00:00";
                }
            }
            else
            {
                if (media.NaturalDuration.HasTimeSpan)
                {
                    if (isPlaying == true)
                    {
                        PLAY_POSITION.Value = (media.Position.TotalSeconds * PLAY_POSITION.Maximum) / media.NaturalDuration.TimeSpan.TotalSeconds;
                        CURR_POS.Content =
                        media.NaturalDuration.TimeSpan.Hours.ToString() + ":" +
                        media.NaturalDuration.TimeSpan.Minutes.ToString() +
                        ":" + media.NaturalDuration.TimeSpan.Seconds.ToString() +
                        "/" + media.Position.Hours.ToString() + ":" +
                        media.Position.Minutes.ToString() +
                        ":" + media.Position.Seconds.ToString();
                    }
                    else
                    {
                        CURR_POS.Content = "00:00:00";
                    }
                }
                else
                {
                    CURR_POS.Content = "00:00:00";
                }
            }
        
        }

        private void PLAY_POSITION_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //  media.Position = new TimeSpan(0,0,(int)((PLAY_POSITION.Value*media.NaturalDuration.TimeSpan.TotalSeconds)/PLAY_POSITION.Maximum));
        }
        private void PLAY_POSITION_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (isPlaying == true)
           // {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (winForVid.IsVisible)
                    {

                        winForVid.setPlayPosition(new TimeSpan(0, 0, (int)((PLAY_POSITION.Value * winForVid.getDuration().TimeSpan.TotalSeconds) / PLAY_POSITION.Maximum)));
                    }
                    else
                    {
                        media.Position = new TimeSpan(0, 0, (int)((PLAY_POSITION.Value * media.NaturalDuration.TimeSpan.TotalSeconds) / PLAY_POSITION.Maximum));
                    }
                }
          //  }
            
        }

        private void Button_DELETE_SONG_Click(object sender, RoutedEventArgs e)
        {
            if (curr_play.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены?", "Удаление песни", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    object item = curr_play.SelectedItem;
                    string song_name = (curr_play.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    DeleteItemFromPlaylist(song_name);
                }
            }
        }
        private void DELETE_PLAYLIST_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены?", "Удаление плейлиста", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    DeletePlaylist(list.SelectedItem.ToString());
                }
            }
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        private void curr_play_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void curr_play_Drop(object sender, DragEventArgs e)
        {
        }

        private void curr_play_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}