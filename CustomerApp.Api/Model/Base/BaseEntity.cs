namespace CustomerApp.Api.Model.Base
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
