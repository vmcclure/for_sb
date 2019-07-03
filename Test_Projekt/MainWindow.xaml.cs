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
using System.IO;
using System.Threading;

namespace Test_projekt
{
    public static class Logic
    {
        
        public static string filename;
        public static short fileinmem = 0;
        public static short check = 0;
        public static int pair = 0;
        public static List<string> str = new List<string>();
        public static TextBox textBox;
        public static int[] comboBox = new int[4] { 0, 0, 0, 0 };
        public static string[] searchstring = new string[4];
        public static int i = 0;
        public static int z = 0;
        public static string mylist;
        public static int n = 1;
        public static int check_read;
        public static int inic = 0;


    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            Logic.i = 0;
            Logic.z = 0;
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Logic.str.Clear();
                Logic.filename = dlg.FileName;
                StreamReader myFile = new StreamReader(Logic.filename, GetFileEncoding());
                Logic.textBox.Text = null;
                Logic.textBox.Clear();
                Logic.inic = 1;
                Logic.fileinmem = 1;
                
                while (myFile.Peek() >= 0)
                {
                    Logic.str.Add(myFile.ReadLine());
                    Logic.i++;                   
                }
                Logic.check_read = 1;
                Logic.textBox.Text = " ";

            }
        }
        public static Encoding GetFileEncoding()
        {
            Encoding encoding = Encoding.GetEncoding(1251);
            using (FileStream stream = File.OpenRead(Logic.filename))
            {
                byte[] buff = new byte[5];
                stream.Read(buff, 0, buff.Length);
                if (buff[0] == 0xEF && buff[1] == 0xBB && buff[2] == 0xBF)
                {
                    encoding = Encoding.UTF8;
                }
                else if (buff[0] == 0xFE && buff[1] == 0xFF)
                {
                    encoding = Encoding.BigEndianUnicode;
                }
                else if (buff[0] == 0xFF && buff[1] == 0xFE)
                {
                    encoding = Encoding.Unicode;
                }
                else if (buff[0] == 0 && buff[1] == 0 && buff[2] == 0xFE && buff[3] == 0xFF)
                {
                    encoding = Encoding.UTF32;
                }
                else if (buff[0] == 0x2B && buff[1] == 0x2F && buff[2] == 0x76)
                {
                    encoding = Encoding.UTF7;
                }
            }
            return encoding;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Logic.textBox == null)
                Logic.textBox = (TextBox)sender;
            if (Logic.check_read == 1)
            {
                string stroka1 = null;
                if (Logic.z == 0)
                    Logic.textBox.Clear();
                
                
                while (Logic.z < Logic.i)
                {
                    stroka1 += Logic.str[Logic.z] + '\n';
                    Logic.z++;
                    if (Logic.z % 10000 == 0)
                    {
                        Logic.textBox.Text += stroka1;
                        stroka1 = null;
                    }
                }
                Logic.check_read = 0;                
                Logic.textBox.Text += stroka1;
            }
            Logic.i = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int d = 0;
            string stroka1 = null;
           
          if (Logic.fileinmem == 1)
            {
                Logic.pair = 0;

                for (int n = 0; n < Logic.comboBox.Length; n++)
                    if (Logic.comboBox[n] != 0)
                        Logic.textBox.Clear();
                for (int n = 0; n < Logic.comboBox.Length; n++)
                {
                    if (Logic.comboBox[n] == 1)
                        Logic.pair++;
                }
                while (d < Logic.str.Count)
                {
                    Logic.check = 0;
                    i = 0;
                    for (int n = 0; n < Logic.searchstring.Length; n++)
                    {
                        if (Logic.str[d].Contains(Logic.searchstring[n]) && Logic.check == 0 && Logic.comboBox[n] == 2)
                        {
                            Logic.check = 1;
                            stroka1 += Logic.str[d] + '\n';
                        }
                        if (Logic.str[d].Contains(Logic.searchstring[n]) && Logic.check == 0 && Logic.comboBox[n] == 1)
                        {
                            i++;
                            if (i == Logic.pair)
                            {
                                stroka1 += Logic.str[d] + '\n';
                                Logic.check = 1;
                            }
                        }
                    }
                    d++;
                }
                Logic.textBox.Text += stroka1;
            }
        }

        

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox str = (TextBox)sender;
            Logic.searchstring[0] = str.Text;
            if(Logic.comboBox[0] == 0)
                str.Clear();
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            TextBox str = (TextBox)sender;
            Logic.searchstring[1] = str.Text;
            if(Logic.comboBox[1] == 0)
                str.Clear();
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            TextBox str = (TextBox)sender;
            Logic.searchstring[2] = str.Text;
            if (Logic.comboBox[2] == 0)
                str.Clear();
        }
        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {
            TextBox str = (TextBox)sender;
            Logic.searchstring[3] = str.Text;
            if (Logic.comboBox[3] == 0)
                str.Clear();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb =  (ComboBox) sender;
            Logic.comboBox[0] = cb.SelectedIndex + 1;
          
        }
        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Logic.comboBox[1] = cb.SelectedIndex + 1;
        }

        private void ComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Logic.comboBox[2] = cb.SelectedIndex + 1;
        }

        private void ComboBox_SelectionChanged3(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Logic.comboBox[3] = cb.SelectedIndex + 1;
        }
    }
}
