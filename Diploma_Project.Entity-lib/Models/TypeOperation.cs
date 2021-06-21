using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma_Project.Entity_lib.Models
{
    public class TypeOperation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  List<Operation> OperationWithProducts { get; set; } = new List<Operation>();
    }
}
