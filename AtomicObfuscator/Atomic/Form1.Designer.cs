namespace Atomic
{
	// Token: 0x02000013 RID: 19
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005548 File Offset: 0x00003748
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005580 File Offset: 0x00003780
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Atomic.Form1));
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.checkBox1 = new global::System.Windows.Forms.CheckBox();
			this.checkBox2 = new global::System.Windows.Forms.CheckBox();
			this.checkBox3 = new global::System.Windows.Forms.CheckBox();
			this.checkBox4 = new global::System.Windows.Forms.CheckBox();
			this.checkBox5 = new global::System.Windows.Forms.CheckBox();
			this.checkBox6 = new global::System.Windows.Forms.CheckBox();
			this.checkBox7 = new global::System.Windows.Forms.CheckBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.textBox2 = new global::System.Windows.Forms.TextBox();
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.button3 = new global::System.Windows.Forms.Button();
			this.button4 = new global::System.Windows.Forms.Button();
			this.button5 = new global::System.Windows.Forms.Button();
			this.button6 = new global::System.Windows.Forms.Button();
			this.button7 = new global::System.Windows.Forms.Button();
			this.button8 = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label5 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.textBox1.AllowDrop = true;
			this.textBox1.BackColor = global::System.Drawing.Color.White;
			this.textBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.ForeColor = global::System.Drawing.Color.Black;
			this.textBox1.Location = new global::System.Drawing.Point(21, 100);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new global::System.Drawing.Size(385, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Drag your program into here. Must be .EXE or .DLL file";
			this.textBox1.Visible = false;
			this.textBox1.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
			this.textBox1.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox1.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox1.Location = new global::System.Drawing.Point(53, 237);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new global::System.Drawing.Size(70, 24);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Packer";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.Visible = false;
			this.checkBox1.CheckedChanged += new global::System.EventHandler(this.checkBox1_CheckedChanged);
			this.checkBox2.AutoSize = true;
			this.checkBox2.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox2.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox2.Location = new global::System.Drawing.Point(54, 87);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new global::System.Drawing.Size(154, 24);
			this.checkBox2.TabIndex = 4;
			this.checkBox2.Text = "Number Protection";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.Visible = false;
			this.checkBox2.CheckedChanged += new global::System.EventHandler(this.checkBox2_CheckedChanged);
			this.checkBox3.AutoSize = true;
			this.checkBox3.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox3.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox3.Location = new global::System.Drawing.Point(54, 207);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new global::System.Drawing.Size(88, 24);
			this.checkBox3.TabIndex = 5;
			this.checkBox3.Text = "Mutation";
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.Visible = false;
			this.checkBox3.CheckedChanged += new global::System.EventHandler(this.checkBox3_CheckedChanged);
			this.checkBox4.AutoSize = true;
			this.checkBox4.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox4.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox4.Location = new global::System.Drawing.Point(53, 177);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new global::System.Drawing.Size(87, 24);
			this.checkBox4.TabIndex = 6;
			this.checkBox4.Text = "Renamer";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.Visible = false;
			this.checkBox4.CheckedChanged += new global::System.EventHandler(this.checkBox4_CheckedChanged);
			this.checkBox5.AutoSize = true;
			this.checkBox5.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox5.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox5.Location = new global::System.Drawing.Point(54, 147);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new global::System.Drawing.Size(112, 24);
			this.checkBox5.TabIndex = 7;
			this.checkBox5.Text = "Control Flow";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox5.Visible = false;
			this.checkBox5.CheckedChanged += new global::System.EventHandler(this.checkBox5_CheckedChanged);
			this.checkBox6.AutoSize = true;
			this.checkBox6.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox6.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox6.Location = new global::System.Drawing.Point(54, 117);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new global::System.Drawing.Size(139, 24);
			this.checkBox6.TabIndex = 8;
			this.checkBox6.Text = "String Protection";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.checkBox6.Visible = false;
			this.checkBox6.CheckedChanged += new global::System.EventHandler(this.checkBox6_CheckedChanged);
			this.checkBox7.AutoSize = true;
			this.checkBox7.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.checkBox7.ForeColor = global::System.Drawing.Color.Black;
			this.checkBox7.Location = new global::System.Drawing.Point(320, 87);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new global::System.Drawing.Size(100, 24);
			this.checkBox7.TabIndex = 9;
			this.checkBox7.Text = "Anti Dump";
			this.checkBox7.UseVisualStyleBackColor = true;
			this.checkBox7.Visible = false;
			this.checkBox7.CheckedChanged += new global::System.EventHandler(this.checkBox7_CheckedChanged);
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Segoe UI", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.Color.Black;
			this.label2.Location = new global::System.Drawing.Point(223, 121);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(125, 20);
			this.label2.TabIndex = 13;
			this.label2.Text = "Last Saved: Never";
			this.label2.Visible = false;
			this.textBox2.AllowDrop = true;
			this.textBox2.BackColor = global::System.Drawing.SystemColors.Control;
			this.textBox2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.ForeColor = global::System.Drawing.Color.Black;
			this.textBox2.Location = new global::System.Drawing.Point(115, 136);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new global::System.Drawing.Size(195, 20);
			this.textBox2.TabIndex = 21;
			this.textBox2.Text = "Enter Your Key";
			this.button1.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button1.ForeColor = global::System.Drawing.Color.Black;
			this.button1.Location = new global::System.Drawing.Point(26, 42);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(61, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "Dashboard";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new global::System.EventHandler(this.button1_Click_1);
			this.button2.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button2.ForeColor = global::System.Drawing.Color.Black;
			this.button2.Location = new global::System.Drawing.Point(160, 42);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(61, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Obfuscate";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Visible = false;
			this.button2.Click += new global::System.EventHandler(this.button2_Click);
			this.button3.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button3.ForeColor = global::System.Drawing.Color.Black;
			this.button3.Location = new global::System.Drawing.Point(93, 42);
			this.button3.Name = "button3";
			this.button3.Size = new global::System.Drawing.Size(61, 24);
			this.button3.TabIndex = 4;
			this.button3.Text = "Options";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Visible = false;
			this.button3.Click += new global::System.EventHandler(this.button3_Click);
			this.button4.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button4.ForeColor = global::System.Drawing.Color.Black;
			this.button4.Location = new global::System.Drawing.Point(227, 42);
			this.button4.Name = "button4";
			this.button4.Size = new global::System.Drawing.Size(89, 24);
			this.button4.TabIndex = 11;
			this.button4.Text = "Save Options";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Visible = false;
			this.button4.Click += new global::System.EventHandler(this.button4_Click);
			this.button5.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button5.ForeColor = global::System.Drawing.Color.Black;
			this.button5.Location = new global::System.Drawing.Point(322, 42);
			this.button5.Name = "button5";
			this.button5.Size = new global::System.Drawing.Size(61, 24);
			this.button5.TabIndex = 12;
			this.button5.Text = "Help";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Visible = false;
			this.button5.Click += new global::System.EventHandler(this.button5_Click);
			this.button6.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button6.ForeColor = global::System.Drawing.Color.Black;
			this.button6.Location = new global::System.Drawing.Point(115, 162);
			this.button6.Name = "button6";
			this.button6.Size = new global::System.Drawing.Size(93, 24);
			this.button6.TabIndex = 22;
			this.button6.Text = "Login";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new global::System.EventHandler(this.button6_Click_1);
			this.button7.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button7.ForeColor = global::System.Drawing.Color.Black;
			this.button7.Location = new global::System.Drawing.Point(214, 162);
			this.button7.Name = "button7";
			this.button7.Size = new global::System.Drawing.Size(94, 24);
			this.button7.TabIndex = 23;
			this.button7.Text = "Close";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new global::System.EventHandler(this.button7_Click_1);
			this.button8.FlatStyle = global::System.Windows.Forms.FlatStyle.System;
			this.button8.ForeColor = global::System.Drawing.Color.Black;
			this.button8.Location = new global::System.Drawing.Point(130, 132);
			this.button8.Name = "button8";
			this.button8.Size = new global::System.Drawing.Size(149, 24);
			this.button8.TabIndex = 24;
			this.button8.Text = "Obfuscate";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Visible = false;
			this.button8.Click += new global::System.EventHandler(this.button8_Click_1);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(90, 114);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(258, 39);
			this.label1.TabIndex = 25;
			this.label1.Text = "Final version: The developer had to focus on college.\r\nThank you for purchasing AtomicObfuscator\r\nYou have helped us a lot, and we appreciate it.\r\n";
			this.label1.Visible = false;
			this.label1.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.label1_MouseDown_1);
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(93, 194);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(131, 13);
			this.label3.TabIndex = 26;
			this.label3.Text = "Your Subscription ends on";
			this.label3.Visible = false;
			this.label3.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.label3_MouseDown_1);
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(93, 213);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(142, 13);
			this.label4.TabIndex = 27;
			this.label4.Text = "You started supporting us on";
			this.label4.Visible = false;
			this.label4.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.label4_MouseDown_1);
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(127, 274);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(180, 13);
			this.label5.TabIndex = 28;
			this.label5.Text = "Thank you again - The Atomic Team";
			this.label5.Visible = false;
			this.label5.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.label5_MouseDown_1);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Control;
			base.ClientSize = new global::System.Drawing.Size(432, 296);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button8);
			base.Controls.Add(this.button7);
			base.Controls.Add(this.button6);
			base.Controls.Add(this.button5);
			base.Controls.Add(this.button4);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.checkBox7);
			base.Controls.Add(this.checkBox6);
			base.Controls.Add(this.checkBox5);
			base.Controls.Add(this.checkBox4);
			base.Controls.Add(this.checkBox3);
			base.Controls.Add(this.checkBox2);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.textBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Form1";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AtomicObfuscator";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400003B RID: 59
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400003C RID: 60
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x0400003D RID: 61
		private global::System.Windows.Forms.CheckBox checkBox1;

		// Token: 0x0400003E RID: 62
		private global::System.Windows.Forms.CheckBox checkBox2;

		// Token: 0x0400003F RID: 63
		private global::System.Windows.Forms.CheckBox checkBox3;

		// Token: 0x04000040 RID: 64
		private global::System.Windows.Forms.CheckBox checkBox4;

		// Token: 0x04000041 RID: 65
		private global::System.Windows.Forms.CheckBox checkBox5;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.CheckBox checkBox6;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.CheckBox checkBox7;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.TextBox textBox2;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.Button button4;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.Button button5;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Button button6;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Button button7;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.Button button8;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.Label label5;
	}
}
