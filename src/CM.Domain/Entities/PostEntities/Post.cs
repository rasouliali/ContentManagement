using CM.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Domain.Entities.PostEntities
{
    public class Post : EntityBaseWithUserData
    {
        [StringLength(4000)]
        public required string PostData { get; set; }
        [StringLength(50)]
        public required string ImageFileName { get; set; }
        public int? Capacity { get; set; }
        public DateTime? TutorialDate { get; set; }
        public virtual List<PostComment> PostComments { get; set; }
        public virtual List<SelectedPost> SelectedPosts { get; set; }
        [NotMapped]
        public long RegisterCount { get; set; }
    }
}
