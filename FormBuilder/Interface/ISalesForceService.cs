using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface ISalesForceService
    {
        Task<(bool success, string error)> CreateSalesforceAccountAndContact(string companyName, string phoneNumber, string useCase);
        Task RevokeToken(string accessToken);
    }

}
