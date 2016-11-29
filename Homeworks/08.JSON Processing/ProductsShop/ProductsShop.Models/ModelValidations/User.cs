namespace ProductsShop.Models
{
    public partial class User
    {
        private bool LastNameIsValid(string lastName)
        {
            if (lastName.Length >= 3)
            {
                return true;
            }

            return false;
        }
    }
}
