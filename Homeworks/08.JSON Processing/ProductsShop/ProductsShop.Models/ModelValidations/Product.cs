namespace ProductsShop.Models
{
    public partial class Product
    {
        private bool NameIsValid(string name)
        {
            if (name.Length >= 3)
            {
                return true;
            }

            return false;
        }
    }
}
