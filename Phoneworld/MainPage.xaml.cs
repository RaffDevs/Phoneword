namespace Phoneworld;

public partial class MainPage : ContentPage
{
    private string transltedNumber;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCall(object? sender, EventArgs e)
    {
        if (await this.DisplayAlert(
                "Dial a Number",
                $"Would you like to call {transltedNumber} ?",
                "Yes",
                "No")
           )
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(transltedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid!", "Ok");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Dial Unavaiable", "Api was not avaiable", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Unable to call", $"Unexpected error: {ex.Message}", "Ok");
            }
        }
    }

    private void OnTranslate(object? sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        transltedNumber = PhoneWordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(transltedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + transltedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        await this.DisplayAlert("OnTap", "Tapped", "OK");
    }
}