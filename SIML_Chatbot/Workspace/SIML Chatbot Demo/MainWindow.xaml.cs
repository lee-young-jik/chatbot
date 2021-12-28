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
// Changjae Lee @ 2021-11-02 
using System.IO;
using Syn.Bot.Siml; 

namespace SIML_Chatbot_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Changjae Lee @ 2021-11-02 
        public SimlBot Chatbot; 
        public MainWindow()
        {
            InitializeComponent();
            // Changjae Lee @ 2021-11-02 
            Chatbot = new SimlBot();
            // SIML Chatbot Demo\bin\Debug\net5.0-windows\Knowledge.simlpk
            Chatbot.PackageManager.LoadFromString(File.ReadAllText("Knowledge.simlpk")); 
        }

        // Changjae Lee @ 2021-11-02 
        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Our code will go here 
            var result = Chatbot.Chat(InputBox.Text);
            OutputBox.Text = string.Format("{2}\nUser: {0}\nBot: {1}",
                InputBox.Text, result.BotMessage, OutputBox.Text);
            InputBox.Text = string.Empty; 
        }
    }
}
