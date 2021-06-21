using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Account
{

        public class SortViewModel
        {
            public SortState IdSort { get; private set; } // значение для сортировки по имени
            public SortState AdderssSort { get; private set; }    // значение для сортировки по возрасту
            public SortState DateSort { get; private set; }   // значение для сортировки по компании
            public SortState Current { get; private set; }     // текущее значение сортировки

            public SortViewModel(SortState sortOrder)
            {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
                AdderssSort = sortOrder == SortState.AddressAsc ? SortState.AddressDesc : SortState.AddressAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
                Current = sortOrder;
            }
        }
        public enum SortState
        {
            IdAsc,
            IdDesc,
            AddressAsc,
            AddressDesc,
            DateAsc,
            DateDesc
        }
    
}
