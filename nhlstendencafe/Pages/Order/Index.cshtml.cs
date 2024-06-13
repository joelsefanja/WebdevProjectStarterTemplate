using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nhlstendencafe.Models;
using nhlstendencafe.Repositories;
using nhlstendencafe.SessionExtensions;
using System.Collections.Generic;
using System.Linq;

namespace nhlstendencafe.Pages.Order
{
    public class OrderModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public Models.Order Order { get; set; }
        public IEnumerable<Category> CategoriesWithProducts { get; set; }

        public OrderModel(IHttpContextAccessor httpContextAccessor, OrderRepository orderRepository, ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            Order = GetOrderFromSession();
        }

        public void OnGet()
        {
            // Load categories from database
            CategoriesWithProducts = _categoryRepository.GetCategoriesWithProducts();
        }

        public IActionResult OnPostAddToOrder(int productId, int quantity)
        {
            // add to database
            var product = _productRepository.GetProductById(productId);
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

            var existingItem = Order.Items.FirstOrDefault(item => item.Product.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Order.Items.Add(new OrderItem { Product = product, Quantity = quantity });
            }

            Order.TotalPrice += product.Price * quantity;

            SaveOrderToSession();
            //_orderRepository.SaveOrder(Order); // Save the order to the database

            return RedirectToPage("/Order");
        }

        public IActionResult OnPostRemoveFromOrder(int productId)
        {
            var itemToRemove = Order.Items.FirstOrDefault(item => item.Product.ProductId == productId);
            if (itemToRemove != null)
            {
                Order.Items.Remove(itemToRemove);

                // Recalculate the total price
                var product = _productRepository.GetProductById(productId);
                Order.TotalPrice -= product.Price * itemToRemove.Quantity;
            }

            SaveOrderToSession();
            //_orderRepository.SaveOrder(Order); // Update the order in the database

            return RedirectToPage("/Order");
        }

        private Models.Order GetOrderFromSession()
        {
            var session = _httpContextAccessor?.HttpContext?.Session;
            var order = session?.GetObject<Models.Order>("Order") ?? new Models.Order();
            return order;
        }

        private void SaveOrderToSession()
        {
            var session = _httpContextAccessor?.HttpContext?.Session;
            session?.SetObject("Order", Order);
        }
    }
}
