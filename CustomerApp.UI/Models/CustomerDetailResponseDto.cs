namespace CustomerApp.Ui.Models
{
    public class CustomerDetailResponseDto<T>
    {
        public bool Status { get; set; }

        public T Data { get; set; }
    }


    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string Tckno { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
    }
}
