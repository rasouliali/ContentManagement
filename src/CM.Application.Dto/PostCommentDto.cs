using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Application.Dto
{
    public class PostCommentDto
    {
        public int Id { get; set; }
        public required string Comment { get; set; }
        public required int PostId { get; set; }
        public int? CommentId { get; set; }
        public PostCommentDto CurrentPostComment { get; set; }
        public string? CurrentUserFullname { get; set; }
        //public PostDto CurrentPost { get; set; }
    }
}
