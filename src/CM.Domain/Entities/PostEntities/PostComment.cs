using CM.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Domain.Entities.PostEntities
{
    public class PostComment : EntityBaseWithUserData
    {
        [StringLength(4000)]
        public required string Comment { get; set; }
        [ForeignKey("CurrentPost")]
        public int PostId { get; set; }
        [ForeignKey("CurrentPostComment")]
        public int? CommentId { get; set; }
        public PostComment CurrentPostComment { get; set; }
        public Post CurrentPost { get; set; }
    }
}
