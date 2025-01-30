using CM.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Domain.Entities.PostEntities
{
    public class SelectedPost : EntityBaseWithUserData
    {
        [ForeignKey("CurrentPost")]
        public int PostId { get; set; }
        public Post CurrentPost { get; set; }
    }
}
