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

namespace MediaPlayer_V2
{
	/// <summary>
	/// Interaction logic for StylePicker.xaml
	/// </summary>
	public partial class StylePicker : Window
	{
        MainWindow window;
		public StylePicker(MainWindow wind)
		{
			this.InitializeComponent();
            window = wind;
			// Insert code required on wobject creation below this point.
		}

        private void apply_style_Click(object sender, RoutedEventArgs e)
        {
            if(style_list.SelectedItem!=null)
            {
                window.StyleChange((style_list.SelectedItem as ListBoxItem).Content.ToString());
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
	}
}