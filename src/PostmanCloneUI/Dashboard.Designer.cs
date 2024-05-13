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
			statusStrip = new StatusStrip();
			systemStatusLabel = new ToolStripStatusLabel();
			httpVerbSelection = new ComboBox();
			callData = new TabControl();
			bodyTab = new TabPage();
			resultsTab = new TabPage();
			resultsText = new TextBox();
			bodyText = new TextBox();
			statusStrip.SuspendLayout();
			callData.SuspendLayout();
			bodyTab.SuspendLayout();
			resultsTab.SuspendLayout();
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
			apiLabel.Location = new Point(25, 78);
			apiLabel.Name = "apiLabel";
			apiLabel.Size = new Size(53, 32);
			apiLabel.TabIndex = 1;
			apiLabel.Text = "API:";
			// 
			// apiText
			// 
			apiText.BorderStyle = BorderStyle.FixedSingle;
			apiText.Location = new Point(194, 78);
			apiText.Name = "apiText";
			apiText.Size = new Size(544, 39);
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
			// httpVerbSelection
			// 
			httpVerbSelection.DropDownStyle = ComboBoxStyle.DropDownList;
			httpVerbSelection.FormattingEnabled = true;
			httpVerbSelection.Items.AddRange(new object[] { "GET", "POST" });
			httpVerbSelection.Location = new Point(84, 78);
			httpVerbSelection.Name = "httpVerbSelection";
			httpVerbSelection.Size = new Size(104, 40);
			httpVerbSelection.TabIndex = 7;
			// 
			// callData
			// 
			callData.Controls.Add(bodyTab);
			callData.Controls.Add(resultsTab);
			callData.Location = new Point(25, 124);
			callData.Name = "callData";
			callData.SelectedIndex = 0;
			callData.Size = new Size(829, 520);
			callData.TabIndex = 8;
			// 
			// bodyTab
			// 
			bodyTab.Controls.Add(bodyText);
			bodyTab.Location = new Point(4, 41);
			bodyTab.Name = "bodyTab";
			bodyTab.Padding = new Padding(3);
			bodyTab.Size = new Size(821, 475);
			bodyTab.TabIndex = 0;
			bodyTab.Text = "Body";
			bodyTab.UseVisualStyleBackColor = true;
			// 
			// resultsTab
			// 
			resultsTab.Controls.Add(resultsText);
			resultsTab.Location = new Point(4, 41);
			resultsTab.Name = "resultsTab";
			resultsTab.Padding = new Padding(3);
			resultsTab.Size = new Size(821, 475);
			resultsTab.TabIndex = 1;
			resultsTab.Text = "Results";
			resultsTab.UseVisualStyleBackColor = true;
			// 
			// resultsText
			// 
			resultsText.BorderStyle = BorderStyle.FixedSingle;
			resultsText.Dock = DockStyle.Fill;
			resultsText.Location = new Point(3, 3);
			resultsText.Multiline = true;
			resultsText.Name = "resultsText";
			resultsText.ReadOnly = true;
			resultsText.ScrollBars = ScrollBars.Both;
			resultsText.Size = new Size(815, 469);
			resultsText.TabIndex = 6;
			// 
			// bodyText
			// 
			bodyText.BorderStyle = BorderStyle.FixedSingle;
			bodyText.Dock = DockStyle.Fill;
			bodyText.Location = new Point(3, 3);
			bodyText.Multiline = true;
			bodyText.Name = "bodyText";
			bodyText.ScrollBars = ScrollBars.Both;
			bodyText.Size = new Size(815, 469);
			bodyText.TabIndex = 6;
			// 
			// Dashboard
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			ClientSize = new Size(884, 677);
			Controls.Add(callData);
			Controls.Add(httpVerbSelection);
			Controls.Add(statusStrip);
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
			callData.ResumeLayout(false);
			bodyTab.ResumeLayout(false);
			bodyTab.PerformLayout();
			resultsTab.ResumeLayout(false);
			resultsTab.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label formHeader;
		private Label apiLabel;
		private TextBox apiText;
		private Button callAPI;
		private StatusStrip statusStrip;
		private ToolStripStatusLabel systemStatusLabel;
		private ComboBox httpVerbSelection;
		private TabControl callData;
		private TabPage bodyTab;
		private TabPage resultsTab;
		private TextBox bodyText;
		private TextBox resultsText;
	}
}
