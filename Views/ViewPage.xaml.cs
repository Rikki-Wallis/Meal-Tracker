using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Meal_Tracker.ViewModels;

namespace Meal_Tracker.Views
{

    public sealed partial class ViewPage : Page
    {   
        private EntityType _entityType;
        private ObservableCollection<ViewItem> _items = new();


        public ViewPage()
        {
            InitializeComponent();
            ResultsList.ItemsSource = _items;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _entityType = (EntityType)e.Parameter;

            await DatabaseHelper.InitializeAsync();

            LoadItems();
        }

        private void LoadItems(string search = "")
        {
            _items.Clear();

            if (_entityType == EntityType.Ingredient) 
            {
                // Just Fetch all items for now
                List<Ingredient> ingredients = DatabaseHelper.GetIngredients();
                foreach (var ingredient in ingredients)
                {
                    _items.Add(new ViewItem { Id = ingredient.Id, Name = ingredient.Name, Calories = ingredient.Calories, Protein = ingredient.Protein, Fat = ingredient.Fat, Carb = ingredient.Carb });
                }
            }
        }

        private void TextChanged(object sender, RoutedEventArgs e) 
        { 
        
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        { 
        
        }

    }
}
