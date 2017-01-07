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
        public int Level { get; set; } = 1;

        public string LevelClass
        {
            get
            {
                if (Level == 2)
                    return "second";
                if (Level == 3)
                    return "third";
                return "second";
            }
        }
        

        public void Add(NavigationSectionViewModel child)
        {
            Children.Add(child);
            child.Level = Level + 1;
        }
    }
}