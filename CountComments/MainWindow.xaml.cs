using Microsoft.Win32;
using System;
using System.IO;
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

namespace CountComments
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<string> filesList;
		public MainWindow()
		{
			InitializeComponent();
			BtnOpen.Click += BtnOpen_Click;
		}

		private void BtnOpen_Click(object sender, RoutedEventArgs e)
		{
			filesList = new List<string>();
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "solution (*.sln)|*.sln";
			if (openFile.ShowDialog() != true)
			{
				return;
			}
			string path = System.IO.Path.GetDirectoryName(openFile.FileName);
			LoadFiles(path);
			Calculate();



		}
		private void LoadFiles(string path)
		{
			DirectoryInfo info = new DirectoryInfo(path);
			foreach (var i in info.GetDirectories())
			{
				if ((i.Attributes & FileAttributes.Hidden) != 0 
					|| i.Name.ToLower() == "packages"
					|| i.Name.ToLower() == "bin" 
					|| i.Name.ToLower() == "obj" 
					|| i.Name.ToLower() == "properties")
				{
					continue;
				}
				var files = i.GetFiles();
				foreach(var f in files)
				{
					if(f.Extension == ".cs")
					{
						filesList.Add(f.FullName);
						Console.Out.WriteLine(f.FullName);
					}
				}				
				Console.Out.WriteLine(i.Name);
				LoadFiles(i.FullName);
			}
		}
		private void Calculate()
		{
			int comments = 0;
			int lines = 0;
			foreach (var fileName in filesList)
			{
				int fileComments = 0;
				int fileLines = 0;
				using (StreamReader sr = new StreamReader(fileName))
				{
					
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						line = line.TrimStart();
						if (line.Length > 2)
						{
							if (line.Substring(0, 2) == "//")
							{
								fileComments++;
							}
							fileLines++;
						}

					}
					//MessageBox.Show($"{fileName} {fileComments.ToString()}");
				}
				comments += fileComments;
				lines += fileLines;
			}
			TextBlockLines.Text = $"Lines : {lines}";
			TextBlockComments.Text = $"Comments : {comments}";
			TextBlockPersent.Text = $"{Math.Round((double)(comments/(float)lines*100f), 1)}%";
		}
	}
}
