using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScraper.Common.Data.Entities
{
    public class CastMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public Guid ShowId { get; set; }

        public Show Show { get; set; }

        public Person Person { get; set; }
    }
}
