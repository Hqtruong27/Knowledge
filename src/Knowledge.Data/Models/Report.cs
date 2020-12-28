using Knowledge.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge.Data.Models
{
    public class Report : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? KnowledgeBaseId { get; set; }

        public int? CommentId { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ReportUserId { get; set; }
        public bool IsProcessed { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
