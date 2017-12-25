using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SNSBot.Instagram;

namespace CSharp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Instagram i = new Instagram();
			MessageBox.Show(i.Login("maxjeon2", "heejoon2").ToString());
			InstagramPage asup = i.GetPage("akmu_suhyun");
			MessageBox.Show(asup.Reload().Result.ToString());
		}
	}
}
