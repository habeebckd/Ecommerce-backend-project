using E_Commerce.Dto.Address;

namespace E_Commerce.Service.Address
{
    public interface IAddressServices
    {
        Task<bool> AddnewAddress(int userId, AddNewAddressDto address);
        Task<List<GetAddressDto>> GetAddress(int userId);
        Task<bool> RemoveAddress(int addId);
    }
}
