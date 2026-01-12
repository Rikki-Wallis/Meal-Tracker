using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;

// EntityType enum
public enum EntityType { 
    Ingredient,
    Recipe
}


namespace Meal_Tracker
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        Button selectedButton;

        public MainWindow()
        {
            this.InitializeComponent();

            // Select Date On Calander
            DateTimeOffset currentDate = DateTimeOffset.Now;
            MealCalander.SelectedDates.Clear();
            MealCalander.SelectedDates.Add(currentDate);
            MealCalander.SetDisplayDate(currentDate);

        }


        // Ingredients Button
        private void IngredientsButtonClick(object sender, RoutedEventArgs e)
        {
            // Change colours of buttons appropriately
            this.RestoreButtonColours();
            var brush = (SolidColorBrush)Application.Current.Resources["ThemeBrush"];
            IngredientsButton.Background = brush;

            // Navigate to ViewPage for ingredients
            MainFrame.Navigate(typeof(Meal_Tracker.Views.ViewPage), EntityType.Ingredient);
        }

        // Recipes Button
        private void RecipesButtonClick(object sender, RoutedEventArgs e)
        {
            // Change colours of buttons appropriately
            this.RestoreButtonColours();
            var brush = (SolidColorBrush)Application.Current.Resources["ThemeBrush"];
            RecipesButton.Background = brush;
            
            // Navigate to ViewPage for ingredients
            MainFrame.Navigate(typeof(Meal_Tracker.Views.AddPage), EntityType.Recipe);

        }

        // Current Date Button

        private void DateButtonLoaded(object sender, RoutedEventArgs e)
        {
            CurrentDateButtonClicked(sender, e);
        }


        private void CurrentDateButtonClicked(object sender, RoutedEventArgs e)
        { 
            // Change colours of buttons appropriately
            this.RestoreButtonColours();
            var brush = (SolidColorBrush)Application.Current.Resources["ThemeBrush"];
            CurrentDateButton.Background = brush;

            // Navigate to page
        }

        private void CalanderDateChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            // Extract Date
            var selectedDate = args.AddedDates[0];

            // Change Date Button To Current Date
            CurrentDateButton.Content = selectedDate.ToString("d");
        }

        // Helper Functions
        private void RestoreButtonColours() 
        {
            IngredientsButton.ClearValue(Button.BackgroundProperty);
            RecipesButton.ClearValue(Button.BackgroundProperty);
            CurrentDateButton.ClearValue(Button.BackgroundProperty);
        }
    }
}
    
