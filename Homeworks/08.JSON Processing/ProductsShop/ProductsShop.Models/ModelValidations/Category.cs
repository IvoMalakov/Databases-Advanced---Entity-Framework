namespace ProductsShop.Models
{
    public partial class Category
    {
        private bool NameIsValid(string name)
        {
            if ((name.Length < 3) || (name.Length > 15))
            {
                return false;
            }

            return true;
        }
    }
}
