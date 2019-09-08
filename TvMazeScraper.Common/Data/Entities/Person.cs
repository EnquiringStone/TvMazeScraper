using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScraper.Common.Data.Entities
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public IList<CastMember> CastMembers { get; set; }
    }
}
