namespace SM.Domain.Services
{
    public interface IShopAccountAcl
    {
        (string name, string mobile) GetAccountBy(long id);
    }
}