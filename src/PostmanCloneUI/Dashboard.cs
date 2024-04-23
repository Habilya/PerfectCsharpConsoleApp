namespace PostmanCloneUI;

public partial class Dashboard : Form
{
	public Dashboard()
	{
		InitializeComponent();
	}

	private async void callAPI_Click(object sender, EventArgs e)
	{
		try
		{
			systemStatusLabel.Text = "Calling API...";

			await Task.Delay(2000);

			systemStatusLabel.Text = "Ready";
		}
		catch (Exception ex)
		{
			resultsText.Text = "Error: " + ex.Message;
			systemStatusLabel.Text = "Error";
		}
	}
}
