using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma_Project.Web_app.ViewModels.Strores
{

        public class SortViewModel
        {
            public SortState NameSort { get; private set; } // значение для сортировки по имени
            public SortState AdderssSort { get; private set; }    // значение для сортировки по возрасту
            public SortState OwnerSort { get; private set; }   // значение для сортировки по компании
            public SortState Current { get; private set; }     // текущее значение сортировки

            public SortViewModel(SortState sortOrder)
            {
                NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
                AdderssSort = sortOrder == SortState.AddressAsc ? SortState.AddressDesc : SortState.AddressAsc;
                OwnerSort = sortOrder == SortState.OwnerAsc ? SortState.OwnerDesc : SortState.OwnerAsc;
                Current = sortOrder;
            }
        }
        public enum SortState
        {
            NameAsc,
            NameDesc,
            AddressAsc,
            AddressDesc,
            OwnerAsc,
            OwnerDesc
        }
    
}
