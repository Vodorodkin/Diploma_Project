using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Home
{

        public class SortViewModel
        {
            public SortState NameSort { get; private set; } // значение для сортировки по имени
            public SortState AgeSort { get; private set; }    // значение для сортировки по возрасту
            public SortState DateSort { get; private set; }   // значение для сортировки по компании
            public SortState Current { get; private set; }     // текущее значение сортировки

            public SortViewModel(SortState sortOrder)
            {
                NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
                AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
                DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
                Current = sortOrder;
            }
        }
        public enum SortState
        {
            NameAsc,
            NameDesc,
            AgeAsc,
            AgeDesc,
            DateAsc,
            DateDesc
        }
    
}
