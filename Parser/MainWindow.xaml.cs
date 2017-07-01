using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReadFromFileChooseButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "TXT Files|*.txt*";
            ofd.ShowDialog();

            FromTextBox.Text = ofd.FileName;
        }

        private void WriteToFileChooseButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog ofd = new System.Windows.Forms.FolderBrowserDialog();
           
            ofd.ShowDialog();

            ToTextBox.Text = ofd.SelectedPath;
        }



        private void DoTheParsing(object sender, RoutedEventArgs e)
        {
            if (ToTextBox.Text.Count() == 0) { 
                MessageBox.Show("Please, choose the file to parse the text.", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
                ToTextBox.BorderBrush = Brushes.Red;
                return;
            }

            if (FromTextBox.Text.Count() == 0)
            {
                MessageBox.Show("Please, choose the file where to write the text.", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
                FromTextBox.BorderBrush = Brushes.Red;
                return;
            }

            if (!FromTextBox.Text.EndsWith(".txt"))
            {
                MessageBox.Show("Input file should have a '.txt' extension!", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
                FromTextBox.BorderBrush = Brushes.Red;
                return;
            }

            if (!ToTextBox.Text.EndsWith(".txt"))
            {
                if (ToTextBox.Text.Contains("."))
                {
                    MessageBox.Show("Output file should have a '.txt' extension!", "Error occured!", MessageBoxButton.OK, MessageBoxImage.Error);
                    ToTextBox.BorderBrush = Brushes.Red;
                    return;
                }
                else
                {
                        ToTextBox.Text += "\\" + Name.Text + ".txt";
                }
            }

            Seeker seek = new Seeker(FromTextBox.Text, ToTextBox.Text);

            MessageBox.Show("Operation successful", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Randomizer(object sender, RoutedEventArgs e)
        {
            Name.Text = System.IO.Path.GetFileNameWithoutExtension(FromTextBox.Text) + "Changed";
        }
    }
}
