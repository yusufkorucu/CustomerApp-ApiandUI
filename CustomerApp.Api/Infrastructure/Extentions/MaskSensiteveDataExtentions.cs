using CustomerApp.Api.Services.Dtos;

namespace CustomerApp.Api.Infrastructure.Extentions
{
    public static class MaskSensiteveDataExtentions
    {

        public static CustomerDetailResponseDto MaskedData(this CustomerDetailResponseDto value)
        {
            value.Tckno = "*******" + value.Tckno.Substring(value.Tckno.Length - 4);

            value.Name = value.Name.Substring(0, 2) + "*****";

            value.LastName = value.LastName.Substring(0, 2) + "*****";

            value.BirthDate = "**/**/" + value.BirthDate.Substring(5, 4);

            return value;
        }


        public static List<CustomerDetailResponseDto> MaskedListData(this List<CustomerDetailResponseDto> valueList)
        {
            var response = new List<CustomerDetailResponseDto>();
            foreach (var value in valueList)
            {
                value.Tckno = "*******" + value.Tckno.Substring(value.Tckno.Length - 4);

                value.Name = value.Name.Substring(0, 2) + "*****";

                value.LastName = value.LastName.Substring(0, 2) + "*****";

                value.BirthDate = "**/**/" + value.BirthDate.Substring(5, 4);

                response.Add(value);
            }


            return response;
        }
    }
}
