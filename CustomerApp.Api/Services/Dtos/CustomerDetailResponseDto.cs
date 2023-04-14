namespace CustomerApp.Api.Services.Dtos
{
    public class CustomerDetailResponseDto
    {
        public int Id { get; set; }
        public string Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
    }
}
