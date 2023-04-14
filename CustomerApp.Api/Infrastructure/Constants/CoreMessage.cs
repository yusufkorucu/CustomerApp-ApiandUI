namespace CustomerApp.Api.Infrastructure.Constants
{
    public static class CoreMessage
    {
        public static string RequestSuccessCompleted = "Request Successfully Completed";
    
        public static string CommonException = "Error on:{0}, Details {1}";

        public static string UnExpectedError = "UnExpected Error on:{0}, Details {1}";

        public static string NotValidTckno = "Kps Service Not Valid Tckno:{0}";

        public static string ExistCustomer = "Customer Already in Database";

        public static string AddedCustomerSuccessfuly = "Added Customer Successfully";

        public static string DeleteCustomerSuccessfuly = "Delete Customer Successfully";

        public static string DeleteCustomerFailed = "Delete Customer Successfully";

        public static string FailAddedCustomer = "Fail Customer Added Operation";

        public static string NotFoundCustomer = "Customer Not Found Database";

        public static string AuthenticateError = "Email or password is incorrect";
    }
}
