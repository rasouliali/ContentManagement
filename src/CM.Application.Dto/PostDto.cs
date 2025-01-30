using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Application.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public required string PostData { get; set; }
        public string ImageFileName { get; set; }
        public int? Capacity { get; set; }
        public int RegisterCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? TutorialDate { get; set; }
        public bool IsSelected { get; set; }
        public virtual List<PostCommentDto> PostComments { get; set; }
    }
}
