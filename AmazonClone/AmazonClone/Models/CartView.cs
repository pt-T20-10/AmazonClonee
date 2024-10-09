using AmazonClone.Areas.Admin.Models;

namespace AmazonClone.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; }
        public List<DeliveryOption> DeliveryOptions { get; set; }
        public string SelectedDeliveryOptionId { get; set; }
        public decimal TotalPrice { get; set; } // Chỉnh sửa ở đây
        public decimal ShippingCost { get; set; } // Chỉnh sửa ở đây
        public decimal EstimatedTax { get; set; } // Chỉnh sửa ở đây
        public decimal OrderTotal { get; set; } // Chỉnh sửa ở đây
    }


    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }

}
