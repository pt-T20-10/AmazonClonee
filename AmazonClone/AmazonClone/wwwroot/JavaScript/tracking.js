import { getProduct } from "../data/products.js";
import { getDeliveryOption } from "../data/deliveryOptions.js";
import { formatCurrency } from "./utils/money.js";
import { cart } from "../data/cart.js";

function updateCartQuantity(){
  const cartQuantity = cart.calculateCartQuantity();  
  document.querySelector('.js-cart-quantity').innerHTML = cartQuantity;
}

updateCartQuantity();

document.addEventListener('DOMContentLoaded', () => {
  renderAllOrderTracking();
});

function renderAllOrderTracking() {
  const orders = JSON.parse(localStorage.getItem('orders')) || [];
  let trackingHTML = '';

  if (orders.length === 0) {
    trackingHTML = '<p>No orders found.</p>';
  } else {
    orders.forEach(order => {
      trackingHTML += generateOrderTrackingHTML(order);
    });
  }

  document.querySelector('.js-order-tracking-container').innerHTML = trackingHTML;
}

function generateOrderTrackingHTML(order) {
  let orderItemsHTML = '';
  order.orderItems.forEach(item => {
    const product = getProduct(item.productId);
    orderItemsHTML += `
      <div class="order-item">
        <img src="${product.image}" alt="${product.name}" class="product-image">
        <div class="order-item-details">
          <div class="product-name">${product.name}</div>
          <div class="product-quantity">Quantity: ${item.quantity}</div>
          <div class="product-price">Price: $${formatCurrency(product.priceCents)}</div>
        </div>
      </div>
    `;
  });

  const deliveryOption = getDeliveryOption(order.deliveryOptionId);
  const estimatedDeliveryDate = calculateDeliveryDate(order.orderDate, deliveryOption);

  return `
    <div class="order-tracking">
      <h2>Order ID: ${order.orderID}</h2>
      <div class="order-info">
        <p><strong>Order Date:</strong> ${formatDate(order.orderDate)}</p>
        <p><strong>Estimated Delivery:</strong> ${estimatedDeliveryDate}</p>
        <p><strong>Total:</strong> $${formatCurrency(order.orderTotal)}</p>
      </div>

      <span class="order-status">Order Placed</span>

      <div class="progress-bar-container">
        <div class="progress-bar" style="width: 50%;"></div>
      </div>
      <div class="progress-labels">
        <span>Processing</span>
        <span>Shipped</span>
        <span>Delivered</span>
      </div>
      <h3>Order Items:</h3>
      <div class="order-items">
        ${orderItemsHTML}
      </div>
    </div>
  `;
}

function formatDate(dateString) {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
}

function calculateDeliveryDate(orderDate, deliveryOption) {
  const date = new Date(orderDate);
  date.setDate(date.getDate() + deliveryOption.deliveryDays);
  return formatDate(date);
}
