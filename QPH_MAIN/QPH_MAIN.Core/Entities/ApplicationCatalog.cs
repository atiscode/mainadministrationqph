namespace QPH_MAIN.Core.Entities
{
    public partial class ApplicationCatalog : BaseEntity
    {
        public int id_application { get; set; }
        public int id_catalog { get; set; }
        public virtual Application application { get; set; }
        public virtual Catalog catalog { get; set; }
    }
}
