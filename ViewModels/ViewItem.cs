using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meal_Tracker.ViewModels
{
    public class ViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat {  get; set; }
        public int Carb { get; set; }
    }
}
