namespace PostmanCloneUI
{
	partial class Dashboard
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			formHeader = new Label();
			apiLabel = new Label();
			apiText = new TextBox();
			callAPI = new Button();
			resultsText = new TextBox();
			statusStrip = new StatusStrip();
			systemStatusLabel = new ToolStripStatusLabel();
			resultsLabel = new Label();
			statusStrip.SuspendLayout();
			SuspendLayout();
			// 
			// formHeader
			// 
			formHeader.AutoSize = true;
			formHeader.Location = new Point(25, 22);
			formHeader.Name = "formHeader";
			formHeader.Size = new Size(174, 32);
			formHeader.TabIndex = 0;
			formHeader.Text = "Postman Clone";
			// 
			// apiLabel
			// 
			apiLabel.AutoSize = true;
			apiLabel.Location = new Point(25, 81);
			apiLabel.Name = "apiLabel";
			apiLabel.Size = new Size(53, 32);
			apiLabel.TabIndex = 1;
			apiLabel.Text = "API:";
			// 
			// apiText
			// 
			apiText.BorderStyle = BorderStyle.FixedSingle;
			apiText.Location = new Point(84, 78);
			apiText.Name = "apiText";
			apiText.Size = new Size(628, 39);
			apiText.TabIndex = 2;
			// 
			// callAPI
			// 
			callAPI.Location = new Point(744, 78);
			callAPI.Name = "callAPI";
			callAPI.Size = new Size(110, 39);
			callAPI.TabIndex = 3;
			callAPI.Text = "Go";
			callAPI.UseVisualStyleBackColor = true;
			callAPI.Click += callAPI_Click;
			// 
			// resultsText
			// 
			resultsText.BorderStyle = BorderStyle.FixedSingle;
			resultsText.Location = new Point(25, 191);
			resultsText.Multiline = true;
			resultsText.Name = "resultsText";
			resultsText.ReadOnly = true;
			resultsText.ScrollBars = ScrollBars.Both;
			resultsText.Size = new Size(829, 416);
			resultsText.TabIndex = 4;
			// 
			// statusStrip
			// 
			statusStrip.Items.AddRange(new ToolStripItem[] { systemStatusLabel });
			statusStrip.Location = new Point(0, 647);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new Size(884, 30);
			statusStrip.TabIndex = 5;
			statusStrip.Text = "statusStrip1";
			// 
			// systemStatusLabel
			// 
			systemStatusLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			systemStatusLabel.Name = "systemStatusLabel";
			systemStatusLabel.Size = new Size(62, 25);
			systemStatusLabel.Text = "Ready";
			// 
			// resultsLabel
			// 
			resultsLabel.AutoSize = true;
			resultsLabel.Location = new Point(25, 147);
			resultsLabel.Name = "resultsLabel";
			resultsLabel.Size = new Size(88, 32);
			resultsLabel.TabIndex = 6;
			resultsLabel.Text = "Results";
			// 
			// Dashboard
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(884, 677);
			Controls.Add(resultsLabel);
			Controls.Add(statusStrip);
			Controls.Add(resultsText);
			Controls.Add(callAPI);
			Controls.Add(apiText);
			Controls.Add(apiLabel);
			Controls.Add(formHeader);
			Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(6);
			Name = "Dashboard";
			Text = "Postman Clone";
			statusStrip.ResumeLayout(false);
			statusStrip.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label formHeader;
		private Label apiLabel;
		private TextBox apiText;
		private Button callAPI;
		private TextBox resultsText;
		private StatusStrip statusStrip;
		private Label resultsLabel;
		private ToolStripStatusLabel systemStatusLabel;
	}
}
