using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_domain.Entities.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now; 
        }

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdateDate { get; set; }  


    }
}
