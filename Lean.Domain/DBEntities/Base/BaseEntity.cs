using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lean.Domain.DBEntities.Base
{
    public class BaseEntity<Tkey>
    {
        [Key] public Tkey Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderIndex { get; set; }
    }
}