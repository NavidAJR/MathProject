using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathProject.DataLayer.Entities
{
    
    public class MathExpression
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public float Result { get; set; }
    }
}
