using CM.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CM.Domain.Entities.UserEntities
{
    public class Role : EntityBase
    {
        [StringLength(100)]
        public string Title { get; set; }
    }
}
