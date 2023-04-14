namespace CustomerApp.Api.Model.Base
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
