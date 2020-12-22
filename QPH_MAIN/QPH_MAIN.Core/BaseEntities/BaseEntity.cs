using System;

namespace QPH_MAIN.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? added_date { get; set; }
        public int? added_user { get; set; }
        public DateTime? edit_date { get; set; }
        public int? edit_user { get; set; }
    }
}