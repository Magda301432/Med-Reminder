namespace Med_Reminder;
//using Android.Content;
using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;
using Med_Reminder;
using System;
using System.Text;
public partial class RegistrationPage : ContentPage
{

    private MyAppDbContext dbContext;
    public RegistrationPage()
	{
		InitializeComponent();
        dbContext = new MyAppDbContext();
    }
    private void RegisterButton_Clicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string firstName = FirstNameEntry.Text;
        string lastName = LastNameEntry.Text;
        string gender = GenderPicker.SelectedItem?.ToString();
        int age = Convert.ToInt32(AgeEntry.Text);
        double weight = Convert.ToDouble(WeightEntry.Text);

        // Tworzenie nowego obiektu danych osobowych
        var newUser = new DaneOsobowe()
        {
            AdresEmail = email,
            HasloSzyfrowane = Encoding.UTF8.GetBytes(password),
            Imie = firstName,
            Nazwisko = lastName,
            Plec = gender,
            Wiek = age,
            Waga = weight
        };

        // Dodanie nowego użytkownika do bazy danych
        dbContext.__dane_osobowe.Add(newUser);
        dbContext.SaveChanges();

        // Wyświetlenie komunikatu o sukcesie
        DisplayAlert("Rejestracja", "Rejestracja zakończona sukcesem!", "OK");
        Navigation.PushAsync(new LoginPage());
    }
}