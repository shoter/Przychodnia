using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Models.Navigation
{
    public class NavigationSectionViewModel
    {
        public List<NavigationSectionViewModel> Children { get; set; } = new List<NavigationSectionViewModel>();
        public bool HaveChilds { get { return Children.Count > 0; } }

        public string Name { get; set; }
        public string Url { get; set; } = "#";
         public string Icon { get; set; }

        public void Add(NavigationSectionViewModel child)
        {
            Children.Add(child);
        }
    }
}