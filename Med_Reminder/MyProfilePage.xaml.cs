using System.Text;

namespace Med_Reminder;


public partial class MyProfilePage : ContentPage
{
    private readonly IUserProfileRepository _userProfileRepository;

    public MyProfilePage()
    {
        InitializeComponent();

        _userProfileRepository = new UserProfileRepository(new MyAppDbContext());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Sprawd�, czy u�ytkownik jest zalogowany
        if (App.IsUserLoggedIn)
        {
            await LoadUserData();
        }
        else
        {
            // Przekieruj u�ytkownika do strony logowania, je�li nie jest zalogowany
            await Navigation.PushAsync(new LoginPage());
        }
    }

    private async Task LoadUserData()
    {
        // Pobierz ID aktualnie zalogowanego u�ytkownika
        int userId = App.CurrentUserId;

        // Pobierz dane u�ytkownika z repozytorium
        DaneOsobowe daneOsobowe = await _userProfileRepository.GetDaneOsoboweAsync(userId);

        // Wy�wietl dane u�ytkownika na stronie
        if (daneOsobowe != null)
        {
            ImieLabel.Text = "Imi�: " + daneOsobowe.Imie;
            NazwiskoLabel.Text = "Nazwisko: " + daneOsobowe.Nazwisko;
            EmailLabel.Text = "Email: " + daneOsobowe.AdresEmail;
            PlecLabel.Text = "P�e�: " + daneOsobowe.Plec;
            WiekLabel.Text = "Wiek: " + daneOsobowe.Wiek.ToString();
            WagaLabel.Text = "Waga: " + daneOsobowe.Waga.ToString();
        }
    }

    private async void SaveChangesButton_Clicked(object sender, EventArgs e)
{
    // Pobierz zmienione dane z p�l formularza
    string newName = NewNameEntry.Text;
    string newEmail = NewEmailEntry.Text;
    string newPassword = NewPasswordEntry.Text;
    double newWeight = Convert.ToDouble(NewWeightEntry.Text);

    // Pobierz obecne dane u�ytkownika
    DaneOsobowe currentUser = await _userProfileRepository.GetDaneOsoboweAsync(App.CurrentUserId);

    // Zaktualizuj dane u�ytkownika
    if (!string.IsNullOrEmpty(newName))
        currentUser.Nazwisko = newName;

    if (!string.IsNullOrEmpty(newEmail))
        currentUser.AdresEmail = newEmail;

    if (!string.IsNullOrEmpty(newPassword))
        currentUser.HasloSzyfrowane = Encoding.UTF8.GetBytes(newPassword);

    currentUser.Waga = newWeight;

    // Zapisz zmiany do bazy danych
    _userProfileRepository.UpdateDaneOsobowe(currentUser);
    await _userProfileRepository.SaveChanges();

    // Wyczy�� zawarto�� p�l formularza
    NewNameEntry.Text = string.Empty;
    NewEmailEntry.Text = string.Empty;
    NewPasswordEntry.Text = string.Empty;
    NewWeightEntry.Text = string.Empty;

    // Wy�wietl komunikat o sukcesie
    await DisplayAlert("Zapisano zmiany", "Twoje dane zosta�y zaktualizowane.", "OK");

    // Ponownie wczytaj dane u�ytkownika, aby od�wie�y� widok
    await LoadUserData();
}

}
