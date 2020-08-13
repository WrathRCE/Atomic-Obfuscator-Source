using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Atomic.packer;
using AtomicProtector.Atomic.cflow;
using dnlib.DotNet;
using dnlib.DotNet.Writer;

namespace Atomic
{
	// Token: 0x02000013 RID: 19
	public partial class Form1 : Form
	{
		// Token: 0x06000068 RID: 104
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000069 RID: 105
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x0600006A RID: 106 RVA: 0x00003EEC File Offset: 0x000020EC
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003F40 File Offset: 0x00002140
		private void button1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003F44 File Offset: 0x00002144
		private void Form1_Load(object sender, EventArgs e)
		{
			OnProgramStart.Initialize("AtomicObfuscator", "70910", "0hRCPrgtZXDdc94Zuw7J9eSNlYlBAyKOMyd", "1.0.0");
			API.Log(Environment.UserName, "load");
			bool flag = File.Exists("C:\\ProgramData\\atomicobfuscatorkeysave.txt");
			if (flag)
			{
				string aio = File.ReadAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt");
				bool flag2 = API.AIO(aio);
				if (flag2)
				{
					this.textBox2.Visible = false;
					this.button6.Visible = false;
					this.button7.Visible = false;
					this.button1.Visible = true;
					this.button2.Visible = true;
					this.button3.Visible = true;
					this.button4.Visible = true;
					this.button5.Visible = true;
					this.label3.Text = "Your Subscription ends on" + User.Expiry;
					this.label4.Text = "You started supporting us on" + User.RegisterDate;
					this.label1.Visible = true;
					this.label3.Visible = true;
					this.label4.Visible = true;
					this.label5.Visible = true;
				}
				else
				{
					File.Delete("C:\\ProgramData\\atomicobfuscatorkeysave.txt");
				}
			}
			string text = new WebClient
			{
				Proxy = null
			}.DownloadString("https://pastebin.com/raw/yc422EAZ");
			bool flag3 = File.Exists("C:\\ProgramData\\atomicconfigg.txt");
			if (flag3)
			{
				string text2 = File.ReadAllText("C:\\ProgramData\\atomicconfigg.txt");
				bool flag4 = text2.Contains("numberprotect");
				if (flag4)
				{
					this.checkBox2.Checked = true;
				}
				bool flag5 = text2.Contains("stringenc");
				if (flag5)
				{
					this.checkBox6.Checked = true;
				}
				bool flag6 = text2.Contains("cflow");
				if (flag6)
				{
					this.checkBox5.Checked = true;
				}
				bool flag7 = text2.Contains("renamer");
				if (flag7)
				{
					this.checkBox4.Checked = true;
				}
				bool flag8 = text2.Contains("mutation");
				if (flag8)
				{
					this.checkBox3.Checked = true;
				}
				bool flag9 = text2.Contains("packer");
				if (flag9)
				{
					this.checkBox1.Checked = true;
				}
				bool flag10 = text2.Contains("antidump");
				if (flag10)
				{
					this.checkBox7.Checked = true;
				}
				FileInfo fileInfo = new FileInfo("C:\\ProgramData\\atomicconfigg.txt");
				DateTime lastWriteTime = fileInfo.LastWriteTime;
				this.label2.Text = "Status: Last updated\n" + lastWriteTime.ToString();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000041E0 File Offset: 0x000023E0
		public static bool Simplify(MethodDef methodDef)
		{
			bool flag = methodDef.Parameters == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				methodDef.Body.SimplifyMacros(methodDef.Parameters);
				result = true;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004218 File Offset: 0x00002418
		public static bool Optimize(MethodDef methodDef)
		{
			bool flag = methodDef.Body == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				methodDef.Body.OptimizeMacros();
				methodDef.Body.OptimizeBranches();
				result = true;
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004254 File Offset: 0x00002454
		private void metroButton1_Click(object sender, EventArgs e)
		{
			ModuleDef moduleDef = ModuleDefMD.Load(this.textBox1.Text);
			bool cflow = this.Cflow;
			if (cflow)
			{
				Ctrl_Flow.Brs(moduleDef);
			}
			bool strings = this.Strings;
			if (strings)
			{
				numbers.InjectClass1(moduleDef);
				numbers.String(moduleDef);
			}
			bool mutation = this.Mutation;
			if (mutation)
			{
				mutatio.Booleanisator(moduleDef);
			}
			bool renamer = this.Renamer;
			if (renamer)
			{
				Atomic.Atomic.Renamer.Renamer3.Rename(moduleDef);
			}
			bool numbers = this.Numbers;
			if (numbers)
			{
				numbers.InjectClass(moduleDef);
			}
			foreach (TypeDef typeDef in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.Instructions.Count != 0;
					if (flag)
					{
						bool numbers2 = this.Numbers;
						if (numbers2)
						{
							array.Array(methodDef);
						}
					}
				}
			}
			foreach (TypeDef typeDef2 in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef2 in typeDef2.Methods.ToArray<MethodDef>())
				{
					bool flag2 = methodDef2.HasBody && methodDef2.Body.Instructions.Count > 0 && !methodDef2.IsConstructor;
					if (flag2)
					{
						bool cflow2 = this.Cflow;
						if (cflow2)
						{
							bool flag3 = !cfhelper.HasUnsafeInstructions(methodDef2);
							if (flag3)
							{
								bool flag4 = Form1.Simplify(methodDef2);
								if (flag4)
								{
									Blocks blocks = cfhelper.GetBlocks(methodDef2);
									bool flag5 = blocks.blocks.Count != 1;
									if (flag5)
									{
										control_flow.toDoBody(methodDef2, blocks);
										break;
									}
								}
								Form1.Optimize(methodDef2);
							}
						}
					}
				}
			}
			bool cflow3 = this.Cflow;
			if (cflow3)
			{
				cflow.Execute(moduleDef);
			}
			bool antiDumper = this.AntiDumper;
			if (antiDumper)
			{
				antidump.Execute(moduleDef);
			}
			bool numbers3 = this.Numbers;
			if (numbers3)
			{
				numbers.InjectClass(moduleDef);
				numbers.encrypt(moduleDef);
				numbers.encrypt(moduleDef);
			}
			bool strings2 = this.Strings;
			if (strings2)
			{
				numbers.String(moduleDef);
			}
			bool mutation2 = this.Mutation;
			if (mutation2)
			{
				foreach (TypeDef typeDef3 in moduleDef.Types.ToArray<TypeDef>())
				{
					foreach (MethodDef methodDef3 in typeDef3.Methods.ToArray<MethodDef>())
					{
						bool flag6 = methodDef3.HasBody && methodDef3.Body.Instructions.Count != 0;
						if (flag6)
						{
							mutatio.Mutate1(methodDef3);
						}
					}
				}
			}
			bool numbers4 = this.Numbers;
			if (numbers4)
			{
				numbers.encrypt(moduleDef);
			}
			Directory.CreateDirectory(".\\AtomicProtected\\");
			moduleDef.Write(".\\AtomicProtected\\" + Path.GetFileName(this.textBox1.Text), new ModuleWriterOptions(moduleDef)
			{
				Logger = DummyLogger.NoThrowInstance
			});
			bool packer = this.Packer;
			if (packer)
			{
				context.LoadModule(".\\AtomicProtected\\" + Path.GetFileName(this.textBox1.Text));
				context.PackerPhase();
				context.SaveModule();
			}
			Process.Start(".\\AtomicProtected\\");
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000045F8 File Offset: 0x000027F8
		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.Numbers = !this.Numbers;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000460A File Offset: 0x0000280A
		private void checkBox6_CheckedChanged(object sender, EventArgs e)
		{
			this.Strings = !this.Strings;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000461C File Offset: 0x0000281C
		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			this.Cflow = !this.Cflow;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000462E File Offset: 0x0000282E
		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			this.Renamer = !this.Renamer;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004640 File Offset: 0x00002840
		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			this.Mutation = !this.Mutation;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004652 File Offset: 0x00002852
		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.Packer = !this.Packer;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004664 File Offset: 0x00002864
		private void checkBox7_CheckedChanged(object sender, EventArgs e)
		{
			this.AntiDumper = !this.AntiDumper;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004678 File Offset: 0x00002878
		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
				bool flag = array != null;
				if (flag)
				{
					string text = array.GetValue(0).ToString();
					int num = text.LastIndexOf(".");
					bool flag2 = num != -1;
					if (flag2)
					{
						string a = text.Substring(num).ToLower();
						bool flag3 = a == ".exe" || a == ".dll";
						if (flag3)
						{
							base.Activate();
							this.textBox1.Text = text;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004730 File Offset: 0x00002930
		private void textBox1_DragEnter(object sender, DragEventArgs e)
		{
			bool dataPresent = e.Data.GetDataPresent(DataFormats.FileDrop);
			if (dataPresent)
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004764 File Offset: 0x00002964
		private void button2_Click(object sender, EventArgs e)
		{
			this.button8.Visible = false;
			this.label1.Visible = false;
			this.label3.Visible = false;
			this.label4.Visible = false;
			this.label5.Visible = false;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = false;
			this.label2.Visible = false;
			this.textBox1.Visible = true;
			this.button8.Visible = true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004844 File Offset: 0x00002A44
		private void button1_Click_1(object sender, EventArgs e)
		{
			this.button8.Visible = false;
			this.textBox1.Visible = false;
			this.checkBox2.Visible = false;
			this.checkBox6.Visible = false;
			this.checkBox5.Visible = false;
			this.checkBox4.Visible = false;
			this.checkBox3.Visible = false;
			this.checkBox1.Visible = false;
			this.checkBox7.Visible = false;
			this.button6.Visible = false;
			this.label2.Visible = false;
			this.label1.Visible = true;
			this.label3.Visible = true;
			this.label4.Visible = true;
			this.label5.Visible = true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004918 File Offset: 0x00002B18
		private void button3_Click(object sender, EventArgs e)
		{
			this.label1.Visible = false;
			this.label3.Visible = false;
			this.label4.Visible = false;
			this.label5.Visible = false;
			this.textBox1.Visible = false;
			this.button8.Visible = false;
			this.checkBox2.Visible = true;
			this.checkBox6.Visible = true;
			this.checkBox5.Visible = true;
			this.checkBox4.Visible = true;
			this.checkBox3.Visible = true;
			this.checkBox1.Visible = true;
			this.checkBox7.Visible = true;
			this.button6.Visible = false;
			this.label2.Visible = false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000049EC File Offset: 0x00002BEC
		private void button4_Click(object sender, EventArgs e)
		{
			this.label2.Text = "Status: Updating..";
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			bool @checked = this.checkBox2.Checked;
			if (@checked)
			{
				text = "numberprotect";
			}
			bool checked2 = this.checkBox6.Checked;
			if (checked2)
			{
				text2 = "stringenc";
			}
			bool checked3 = this.checkBox5.Checked;
			if (checked3)
			{
				text3 = "cflow";
			}
			bool checked4 = this.checkBox4.Checked;
			if (checked4)
			{
				text4 = "renamer";
			}
			bool checked5 = this.checkBox3.Checked;
			if (checked5)
			{
				text5 = "mutation";
			}
			bool checked6 = this.checkBox1.Checked;
			if (checked6)
			{
				text6 = "packer";
			}
			bool checked7 = this.checkBox7.Checked;
			if (checked7)
			{
				text7 = "antidump";
			}
			string[] contents = new string[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7
			};
			File.WriteAllLines("C:\\ProgramData\\atomicconfigg.txt", contents);
			FileInfo fileInfo = new FileInfo("C:\\ProgramData\\atomicconfigg.txt");
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			this.label2.Text = "Status: Last updated\n" + lastWriteTime.ToString();
			MessageBox.Show("Config Updated!", "AtomicProtector", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004B63 File Offset: 0x00002D63
		private void button5_Click(object sender, EventArgs e)
		{
			MessageBox.Show("We recommend you check all settings first.. \n\n If the program doesn't open, uncheck settings until it opens/works.\nSome settings wont work for certain programs, so be ready to uncheck, its not unusual.\n\nIf you are still having problems, click help.");
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004B71 File Offset: 0x00002D71
		private void button7_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004B7B File Offset: 0x00002D7B
		private void label6_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/Bwk5c8R");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004B8C File Offset: 0x00002D8C
		private void button6_Click(object sender, EventArgs e)
		{
			this.label2.Text = "Status: Updating..";
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			bool @checked = this.checkBox2.Checked;
			if (@checked)
			{
				text = "numberprotect";
			}
			bool checked2 = this.checkBox6.Checked;
			if (checked2)
			{
				text2 = "stringenc";
			}
			bool checked3 = this.checkBox5.Checked;
			if (checked3)
			{
				text3 = "cflow";
			}
			bool checked4 = this.checkBox4.Checked;
			if (checked4)
			{
				text4 = "renamer";
			}
			bool checked5 = this.checkBox3.Checked;
			if (checked5)
			{
				text5 = "mutation";
			}
			bool checked6 = this.checkBox1.Checked;
			if (checked6)
			{
				text6 = "packer";
			}
			bool checked7 = this.checkBox7.Checked;
			if (checked7)
			{
				text7 = "antidump";
			}
			string[] contents = new string[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7
			};
			File.WriteAllLines("C:\\ProgramData\\atomicconfigg.txt", contents);
			FileInfo fileInfo = new FileInfo("C:\\ProgramData\\atomicconfigg.txt");
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			this.label2.Text = "Status: Last updated\n" + lastWriteTime.ToString();
			MessageBox.Show("Config Updated!", "AtomicProtector", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D04 File Offset: 0x00002F04
		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004D40 File Offset: 0x00002F40
		private void label5_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004D7C File Offset: 0x00002F7C
		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004DB8 File Offset: 0x00002FB8
		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004DF4 File Offset: 0x00002FF4
		private void label1_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004E30 File Offset: 0x00003030
		private void label3_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004E6C File Offset: 0x0000306C
		private void label7_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004EA8 File Offset: 0x000030A8
		private void label4_MouseDown(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004EE2 File Offset: 0x000030E2
		private void button8_Click(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004EF0 File Offset: 0x000030F0
		private void metroButton2_Click(object sender, EventArgs e)
		{
			string text = this.textBox2.Text.Replace(" ", "");
			bool flag = API.AIO(text);
			if (flag)
			{
				this.textBox2.Visible = false;
				this.button1.Visible = true;
				this.button2.Visible = true;
				this.button3.Visible = true;
				this.button4.Visible = true;
				this.button5.Visible = true;
				File.WriteAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt", text);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004F83 File Offset: 0x00003183
		private void label7_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F86 File Offset: 0x00003186
		private void label5_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004F8C File Offset: 0x0000318C
		private void button6_Click_1(object sender, EventArgs e)
		{
			string text = this.textBox2.Text.Replace(" ", "");
			bool flag = API.AIO(text);
			if (flag)
			{
				this.button6.Visible = false;
				this.button7.Visible = false;
				this.textBox2.Visible = false;
				this.button1.Visible = true;
				this.button2.Visible = true;
				this.button3.Visible = true;
				this.button4.Visible = true;
				this.button5.Visible = true;
				File.WriteAllText("C:\\ProgramData\\atomicobfuscatorkeysave.txt", text);
				this.label3.Text = "Your Subscription ends on" + User.Expiry;
				this.label4.Text = "You started supporting us on" + User.RegisterDate;
				this.label1.Visible = true;
				this.label3.Visible = true;
				this.label4.Visible = true;
				this.label5.Visible = true;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000050A8 File Offset: 0x000032A8
		private void button8_Click_1(object sender, EventArgs e)
		{
			ModuleDef moduleDef = ModuleDefMD.Load(this.textBox1.Text);
			bool cflow = this.Cflow;
			if (cflow)
			{
				Ctrl_Flow.Brs(moduleDef);
			}
			bool strings = this.Strings;
			if (strings)
			{
				numbers.InjectClass1(moduleDef);
				numbers.String(moduleDef);
			}
			bool mutation = this.Mutation;
			if (mutation)
			{
				mutatio.Booleanisator(moduleDef);
			}
			bool renamer = this.Renamer;
			if (renamer)
			{
				Atomic.Atomic.Renamer.Renamer3.Rename(moduleDef);
			}
			bool numbers = this.Numbers;
			if (numbers)
			{
				numbers.InjectClass(moduleDef);
			}
			foreach (TypeDef typeDef in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef in typeDef.Methods.ToArray<MethodDef>())
				{
					bool flag = methodDef.HasBody && methodDef.Body.Instructions.Count != 0;
					if (flag)
					{
						bool numbers2 = this.Numbers;
						if (numbers2)
						{
							array.Array(methodDef);
						}
					}
				}
			}
			foreach (TypeDef typeDef2 in moduleDef.Types.ToArray<TypeDef>())
			{
				foreach (MethodDef methodDef2 in typeDef2.Methods.ToArray<MethodDef>())
				{
					bool flag2 = methodDef2.HasBody && methodDef2.Body.Instructions.Count > 0 && !methodDef2.IsConstructor;
					if (flag2)
					{
						bool cflow2 = this.Cflow;
						if (cflow2)
						{
							bool flag3 = !cfhelper.HasUnsafeInstructions(methodDef2);
							if (flag3)
							{
								bool flag4 = Form1.Simplify(methodDef2);
								if (flag4)
								{
									Blocks blocks = cfhelper.GetBlocks(methodDef2);
									bool flag5 = blocks.blocks.Count != 1;
									if (flag5)
									{
										control_flow.toDoBody(methodDef2, blocks);
										break;
									}
								}
								Form1.Optimize(methodDef2);
							}
						}
					}
				}
			}
			bool cflow3 = this.Cflow;
			if (cflow3)
			{
				cflow.Execute(moduleDef);
			}
			bool antiDumper = this.AntiDumper;
			if (antiDumper)
			{
				antidump.Execute(moduleDef);
			}
			bool numbers3 = this.Numbers;
			if (numbers3)
			{
				numbers.InjectClass(moduleDef);
				numbers.encrypt(moduleDef);
				numbers.encrypt(moduleDef);
			}
			bool strings2 = this.Strings;
			if (strings2)
			{
				numbers.String(moduleDef);
			}
			bool mutation2 = this.Mutation;
			if (mutation2)
			{
				foreach (TypeDef typeDef3 in moduleDef.Types.ToArray<TypeDef>())
				{
					foreach (MethodDef methodDef3 in typeDef3.Methods.ToArray<MethodDef>())
					{
						bool flag6 = methodDef3.HasBody && methodDef3.Body.Instructions.Count != 0;
						if (flag6)
						{
							mutatio.Mutate1(methodDef3);
						}
					}
				}
			}
			bool numbers4 = this.Numbers;
			if (numbers4)
			{
				numbers.encrypt(moduleDef);
			}
			Directory.CreateDirectory(".\\AtomicProtected\\");
			moduleDef.Write(".\\AtomicProtected\\" + Path.GetFileName(this.textBox1.Text), new ModuleWriterOptions(moduleDef)
			{
				Logger = DummyLogger.NoThrowInstance
			});
			bool packer = this.Packer;
			if (packer)
			{
				context.LoadModule(".\\AtomicProtected\\" + Path.GetFileName(this.textBox1.Text));
				context.PackerPhase();
				context.SaveModule();
			}
			Process.Start(".\\AtomicProtected\\");
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000544C File Offset: 0x0000364C
		private void button7_Click_1(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005458 File Offset: 0x00003658
		private void label1_MouseDown_1(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005494 File Offset: 0x00003694
		private void label3_MouseDown_1(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000054D0 File Offset: 0x000036D0
		private void label4_MouseDown_1(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000550C File Offset: 0x0000370C
		private void label5_MouseDown_1(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Left;
			if (flag)
			{
				Form1.ReleaseCapture();
				Form1.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x04000032 RID: 50
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000033 RID: 51
		public const int HT_CAPTION = 2;

		// Token: 0x04000034 RID: 52
		private bool Strings = false;

		// Token: 0x04000035 RID: 53
		private bool Numbers = false;

		// Token: 0x04000036 RID: 54
		private bool Cflow = false;

		// Token: 0x04000037 RID: 55
		private bool Renamer = false;

		// Token: 0x04000038 RID: 56
		private bool Mutation = false;

		// Token: 0x04000039 RID: 57
		private bool Packer = false;

		// Token: 0x0400003A RID: 58
		private bool AntiDumper = false;
	}
}
