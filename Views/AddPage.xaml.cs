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
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

    }
}