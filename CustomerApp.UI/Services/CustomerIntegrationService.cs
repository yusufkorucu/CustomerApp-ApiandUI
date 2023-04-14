using CustomerApp.Ui.Models;
using CustomerApp.UI.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CustomerApp.Ui.Services;

public class CustomerIntegrationService : ICustomerIntegrationService
{
    #region Field
    private readonly HttpClient _client;
    #endregion

    #region Ctor
    public CustomerIntegrationService()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7299/api/");
    }
    #endregion

    #region Methods
    public async Task<ResponseDto> Login(LoginViewModel model)
    {

        string data = JsonConvert.SerializeObject(model);

        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "Authenticate/Login", content);

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<TokenResponseDto>>(response.Content.ReadAsStringAsync().Result);

        if (customerApiresponse.IsSuccess)

            return new ResponseDto() { Status = true, Data = customerApiresponse?.Data.Token };

        return new ResponseDto() { Status = false, Data = customerApiresponse?.Message };
    }

    public async Task<ResponseDto> CreateCustomer(CustomerAddViewModel model, string token)
    {

        string data = JsonConvert.SerializeObject(model);

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "Customer/AddCustomer", content);

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<string>>(response.Content.ReadAsStringAsync().Result);

        if (customerApiresponse.IsSuccess)
            return new ResponseDto() { Status = true, Data = customerApiresponse?.Message };


        return new ResponseDto() { Status = false, Data = customerApiresponse?.Message };



        //var client = new HttpClient();
        //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7299/api/Customer/AddCustomer");
        //request.Headers.Add("Authorization", token);
        //var content = new StringContent($"{{\r\n  \"tckno\": ${model.Tckno},\r\n  \"name\": \"${model.Name}\",\r\n  \"lastName\": \"${model.LastName}\",\r\n  \"birthDate\": \"${model.BirthDate}\",\r\n  \"phone\": \"${model.Phone}\"\r\n}}", null, "application/json");
        //request.Content = content;
        //var response = await client.SendAsync(request);
        //response.EnsureSuccessStatusCode();
    }

    public async Task<ResponseDto> DeleteCustomer(int Id, string token)
    {
        var model = new CustomerDeleteViewModel();
        model.Id = Id;
        string data = JsonConvert.SerializeObject(model);

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "Customer/DeleteCustomer", content);

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<string>>(response.Content.ReadAsStringAsync().Result);

        if (customerApiresponse.IsSuccess)
            return new ResponseDto() { Status = true, Data = customerApiresponse?.Message };


        return new ResponseDto() { Status = false, Data = customerApiresponse?.Message };
    }

    public async Task<CustomerDetailResponseDto<List<CustomerDetailsDto>>> GetFilteredCustomer(CustomerGetFilteredViewModel model, string token)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var data = new List<CustomerDetailsDto>();

        HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"Customer/GetFilteredCustomers?name={model.Name}&lastName={model.LastName}&tckno={model.Tckno}");

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<List<CustomerDetailsDto>>>(response.Content.ReadAsStringAsync().Result);


        if (customerApiresponse.IsSuccess)
            return new CustomerDetailResponseDto<List<CustomerDetailsDto>>() { Status = true, Data = customerApiresponse.Data };


        return new CustomerDetailResponseDto<List<CustomerDetailsDto>>() { Status = false, Data = data };

    }

    public async Task<CustomerDetailResponseDto<CustomerDetailsDto>> GetDetailCustomer(int id, string token)
    {

        string data = JsonConvert.SerializeObject(id);
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Customer/GetCustomersById");

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<CustomerDetailsDto>>(response.Content.ReadAsStringAsync().Result);

        if (customerApiresponse.IsSuccess)

            return new CustomerDetailResponseDto<CustomerDetailsDto>() { Status = true, Data = customerApiresponse.Data };

        return new CustomerDetailResponseDto<CustomerDetailsDto>() { Status = false, Data = customerApiresponse.Data };


    }

    public async Task<CustomerDetailResponseDto<List<CustomerDetailsDto>>> GetAllCustomer(string token)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Customer/GetAllCustomer");

        var customerApiresponse = JsonConvert.DeserializeObject<CustomerApiResponseDto<List<CustomerDetailsDto>>>(response.Content.ReadAsStringAsync().Result);

        if (customerApiresponse.IsSuccess)

            return new CustomerDetailResponseDto<List<CustomerDetailsDto>>() { Status = true, Data = customerApiresponse.Data };

        return new CustomerDetailResponseDto<List<CustomerDetailsDto>>() { Status = false, Data = customerApiresponse.Data };
    }

    #endregion

}