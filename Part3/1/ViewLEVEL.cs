// –––––––––––––––– View LEVEL ––––––––––––––––

public class FeaturedProductsViewModel
{
	public FeaturedProductsViewModel(IEnumerable<ProductViewModel> products)
	{
		this.Products = products;
	}

    public IEnumerable<ProductViewModel> Products { get; }
}

public class ProductViewModel
{
	private static readonly CultureInfo PriceCulture = new CultureInfo("en-US");

    public ProductViewModel(string name, decimal unitPrice)
    {
		this.SummaryText = string.Format(PriceCulture, "{0} ({1:C})", name, unitPrice;
	}

    public string SummaryText {get; }
	
}

public ViewResult Index()
{
    var vm = new FeaturedProductsViewModel(new [] 
    { 
        new ProductViewModel ("Chocolate", 34.95m),
        new ProductViewModel ("Asparagus", 39.80m)
    });

    return this.View(vm);
}

#region Listing 3.4 HomeController
public class HomeController : Controller // ---- Start code Listing 3.4 ----
{
	private readonly IProductService productService;

	public HomeController(IProductService productService)
	{
		if (productService == null)
		throw new ArgumentNullException("productService");
		
		this.productService = productService;
	}

	public ViewResult Index()
	{
		IEnumerable<DiscountedProduct> featuredProducts = this.productService.GetFeaturedProducts();

		var vm = new FeaturedProductsViewModel(
			from product in featuredProducts
			select new ProductViewModel(product));

			return this.View(vm);
	}
} // ---- End code Listing 3.4 ----
#endregion