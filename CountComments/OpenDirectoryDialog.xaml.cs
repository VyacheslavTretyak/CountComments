using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace CountComments
{
	/// <summary>
	/// Interaction logic for OpenDirectoryDialog.xaml
	/// </summary>
	public partial class OpenDirectoryDialog : Window
	{
		public OpenDirectoryDialog()
		{
			InitializeComponent();
			
			Tree.Items.Clear();
			TreeViewItem item = new TreeViewItem()
			{
				Header = "c",
			};			
			LoadDirectories(item, @"c:\");
			Tree.Items.Add(item);
			
		}
		

		private void LoadDirectories(TreeViewItem item, string path)
		{			
			DirectoryInfo info = new DirectoryInfo(path);
			var dirs = info.GetDirectories();
			foreach(DirectoryInfo i in dirs)
			{
				if((int)(i.Attributes & FileAttributes.Hidden) != 0 || (int)(i.Attributes & FileAttributes.System) != 0 || (int)(i.Attributes & FileAttributes.Encrypted) != 0)
				{
					continue;
				}
				Console.Out.WriteLine(i.Name);
				TreeViewItem node = new TreeViewItem() { Header = i.Name };					
				item.Items.Add(node);
			}
		}
	}
}
