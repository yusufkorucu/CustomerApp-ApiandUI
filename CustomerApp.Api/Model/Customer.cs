using CustomerApp.Api.Model.Base;

namespace CustomerApp.Api.Model
{
    public class Customer : BaseEntity
    {
        public long Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
    }
}
