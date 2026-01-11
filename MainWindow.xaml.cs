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
        }


        // Ingredients Buttons
        private void IngredientsButtonClick(object sender, RoutedEventArgs e)
        {
            this.RestoreButtonStates();

            IngredientsButton.Visibility = Visibility.Collapsed;
            IngredientsActionPanel.Visibility = Visibility.Visible;
        }

        // Recipes Buttons
        private void RecipesButtonClick(object sender, RoutedEventArgs e)
        {
            this.RestoreButtonStates();

            RecipesButton.Visibility = Visibility.Collapsed;
            RecipesActionPanel.Visibility = Visibility.Visible;
        }

        // Navigate to add x
        private void AddIngredientsClick(object sender, RoutedEventArgs e)
        {
            RestoreButtonColours();
            IngredientsAdd.Background = new SolidColorBrush(Microsoft.UI.Colors.DarkSlateGray);
            MainFrame.Navigate(typeof(Meal_Tracker.Views.AddPage), EntityType.Ingredient);
        }

        private void AddRecipeClick(object sender, RoutedEventArgs e)
        {
            RestoreButtonColours();
            RecipeAdd.Background = new SolidColorBrush(Microsoft.UI.Colors.DarkSlateGray);
            MainFrame.Navigate(typeof(Meal_Tracker.Views.AddPage), EntityType.Recipe);
        }

        // View buttons
        private void ViewIngredientsClick(object sender, RoutedEventArgs e) 
        {
            RestoreButtonColours();
            IngredientsView.Background = new SolidColorBrush(Microsoft.UI.Colors.DarkSlateGray);
            MainFrame.Navigate(typeof(Meal_Tracker.Views.ViewPage), EntityType.Ingredient);
        }
        
        private void ViewRecipeClick(object sender, RoutedEventArgs e) 
        {
            RestoreButtonColours();
            RecipeView.Background = new SolidColorBrush(Microsoft.UI.Colors.DarkSlateGray);
            MainFrame.Navigate(typeof(Meal_Tracker.Views.ViewPage), EntityType.Recipe);
        }

        // General Restore method
        private void RestoreButtonStates()
        {
            // Collapse all action panels
            IngredientsActionPanel.Visibility = Visibility.Collapsed;
            RecipesActionPanel.Visibility = Visibility.Collapsed;

            // Set all main buttons to visible
            IngredientsButton.Visibility = Visibility.Visible;
            RecipesButton.Visibility = Visibility.Visible;

        }

        private void RestoreButtonColours() 
        {
            IngredientsAdd.ClearValue(Button.BackgroundProperty);
            IngredientsView.ClearValue(Button.BackgroundProperty);
            RecipeAdd.ClearValue(Button.BackgroundProperty);
            RecipeView.ClearValue(Button.BackgroundProperty);
        }
    }
}
    
