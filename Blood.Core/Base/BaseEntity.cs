using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blood.Core.Utils;

namespace Blood.Core.Base
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset? DeletedTime { get; set; }
    }
}
