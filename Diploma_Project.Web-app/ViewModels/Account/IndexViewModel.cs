using Diploma_Project.Entity_lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Account
{
    public class IndexViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public string SelectedName { get; set; }
        public PaginationViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
