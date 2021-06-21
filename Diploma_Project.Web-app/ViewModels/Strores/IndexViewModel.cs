using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Diploma_Project.Web_app.ViewModels.Strores
{
    public class IndexViewModel : BaseViewModel
    {
        public IEnumerable<Store> Stores { get; set; }
        public string SelectedName { get; set; }
        public PaginationViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
