﻿using PostmanCloneLibrary;

namespace PostmanCloneUI;

public partial class Dashboard : Form
{
	private readonly IApiAccess api = new ApiAccess();

	public Dashboard()
	{
		InitializeComponent();
		httpVerbSelection.SelectedItem = "GET";
	}

	private async void callAPI_Click(object sender, EventArgs e)
	{
		systemStatusLabel.Text = "Calling API...";
		resultsText.Text = "";

		// Validate the API URL
		if (api.IsValidUrl(apiText.Text) == false)
		{
			systemStatusLabel.Text = "Invalid URL";
			return;
		}

		try
		{
			resultsText.Text = await api.CallApiAsync(apiText.Text);
			callData.SelectedTab = resultsTab;

			systemStatusLabel.Text = "Ready";
		}
		catch (Exception ex)
		{
			resultsText.Text = "Error: " + ex.Message;
			systemStatusLabel.Text = "Error";
		}
	}
}
