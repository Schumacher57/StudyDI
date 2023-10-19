// –––––––––––––––– Model LEVEL ––––––––––––––––

#region  Listing 3.5 interface IProductService
public interface IProductService // ---- Code Listing 3.5 ----
{
    IEnumerable<DiscountedProduct> GetFeaturedProducts();
}
#endregion

#region  Listing 3.6 class DiscountedProduct
public class DiscountedProduct // ---- Code Listing 3.6 ----
{
    public string Name { get; }
    public decimal UnitPrice { get; }

    public DiscountedProduct(string name, decimal unitPrice)
    {
        if (name == null) throw new ArgumentNullException("name");

        this.Name = name;
        this.UnitPrice = unitPrice;
    }
} // ---- Code Listing 3.7 ----

public interface IProductRepository
{
    IEnumerable<Product> GetFeaturedProducts();
}
#endregion

#region  Listing 3.8 class Product
public class Product // ---- Start code Listing 3.8 ----
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsFeatured { get; set; }

    public DiscountedProduct ApplyDiscountFor(IUserContext user)
    {
        bool preferred = user.IsInRole(Role.PreferredCustomer);
        decimal discount = preferred ? .95m : 1.00m;

        return new DiscountedProduct(name: this.Name, unitPrice: this.UnitPrice * discount);
    }
} // ---- End code Listing 3.8 ----
#endregion

#region Listing 3.9 class ProductService

public class ProductService : IProductService // ---- Code Listing 3.9 ----
{
    private readonly IProductRepository repository;
    private readonly IUserContext userContext;

    public ProductService(IProductRepository repository, IUserContext userContext)
    {
        if (repository == null)
            throw new ArgumentNullException("repository");
        if (userContext == null)
            throw new ArgumentNullException("userContext");

        this.repository = repository;
        this.userContext = userContext;
    }

    public IEnumerable<DiscountedProduct> GetFeaturedProducts()
    {
        return from product in this.repository
               .GetFeaturedProducts()
               select product.ApplyDiscountFor(this.userContext);
    }
}
#endregion

public interface IUserContext // ---- Code snippet after Listing 3.9 ----
{
    bool IsInRole(Role role);
}
public enum Role { PreferredCustomer }

