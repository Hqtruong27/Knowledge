using Knowledge.Backend.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge.Backend.Data.Models
{
    public class Comment : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(500)]
        [Required]
        public string Content { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        public int KnowledgeBaseId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string OwnwerUserId { get; set; }
        public DateTime CreateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? LastUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
