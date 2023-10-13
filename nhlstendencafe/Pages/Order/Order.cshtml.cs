using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;

namespace nhlstendencafe.Pages.Order
{
    public class OrderModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Models.Order Order { get; set; }

        public IEnumerable<Category> CategoriesWithProducts { get; set; }
        public CategoryRepository categoryRepository = new CategoryRepository();
        public ProductRepository productRepository = new ProductRepository();


        public void OnGet()
        {
            // Initialize the order and load categories and products from the database
            Order = GetOrderFromSession();
            

            // Load categories
            CategoriesWithProducts = categoryRepository.GetCategoriesWithProducts();
        }

        public IActionResult OnPostAddToOrder(int productId, int quantity)
        {
            var product = productRepository.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            if (Order == null)
            {
                Order = new Models.Order();
            }

            if (Order.Items == null)
            {
                Order.Items = new List<OrderItem>();
            }

            var existingItem = Order.Items.FirstOrDefault(item => item.product != null && item.product.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Order.Items.Add(new OrderItem { product = product, Quantity = quantity });
            }

            Order.TotalPrice += product.Price;

            SaveOrderToSession();

            return RedirectToPage("/Order");
        }


        public IActionResult OnPostRemoveFromOrder(int productId)
        {
            // Remove the product from the order
            var itemToRemove = Order.Items.FirstOrDefault(item => item.product.ProductId == productId);
            if (itemToRemove != null)
            {
                Order.Items.Remove(itemToRemove);

                // Recalculate the total price
                var product = productRepository.GetProductById(productId);
                Order.TotalPrice -= product.Price;
            }

            SaveOrderToSession();

            return RedirectToPage("/Order");
        }

        private Models.Order GetOrderFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            // Retrieve the order from session or create a new one if not found
            var order = session.GetObject<Models.Order>("Order");
            if (order == null)
            {
                order = new Models.Order();
                session.SetObject("Order", order);
            }

            return order;
        }
        
        public int GetItemQuantity(int productId)
        {
            var item = Order.Items.FirstOrDefault(i => i.product.ProductId == productId);
            return item?.Quantity ?? 0;
        }

        private void SaveOrderToSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            // Save the order to session
            session.SetObject("Order", Order);
        }
    }
}
