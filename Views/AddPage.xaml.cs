using System;
using Meal_Tracker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Meal_Tracker.Views
{
    public sealed partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is EntityType entityType)
            {
                switch (entityType)
                {
                    case EntityType.Ingredient:
                        TitleText.Text = "Add Ingredient";
                        IngredientFields.Visibility = Visibility.Visible;
                        RecipeFields.Visibility = Visibility.Collapsed;
                        break;

                    case EntityType.Recipe:
                        TitleText.Text = "Add Recipe";
                        IngredientFields.Visibility = Visibility.Collapsed;
                        RecipeFields.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack) 
            {
                this.Frame.GoBack();
            }
        }

        private async void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verify that each box is the correct data type
                if (!int.TryParse(CaloriesBox.Text, out int calories))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Calories must be a valid number.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                if (!int.TryParse(ProteinBox.Text, out int protein))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Protein must be a valid number.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                if (!int.TryParse(FatBox.Text, out int fat))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Fats must be a valid number.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                if (!int.TryParse(CarbBox.Text, out int carb))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Carbs must be a valid number.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                if (string.IsNullOrEmpty(NameBox.Text))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Invalid Input",
                        Content = "Name cannot be empty.",
                        CloseButtonText = "Ok",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                    return;
                }

                // Add ingredient to database
                Ingredient newIngredient = new Ingredient { Id = 0, Name = NameBox.Text, Calories = calories, Protein = protein, Fat = fat, Carb = carb };
                await DatabaseHelper.InitializeAsync();
                int id = DatabaseHelper.AddIngredient(newIngredient);

                System.Diagnostics.Debug.WriteLine($"Added Ingredient: {id}");

                // Remove frame
                if (this.Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving ingredient: {ex.Message}");

                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"Failed to save ingredient: {ex.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }
    }
}