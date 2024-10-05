import { getProduct } from '../data/products.js';
import { formatCurrency } from './utils/money.js';
import { getDeliveryOption } from '../data/deliveryOptions.js';
import { cart } from '../data/cart.js';

function updateUserInterface() {
  const loggedInUser = localStorage.getItem('loggedInUser');
  const userEmailElement = document.getElementById('userName');
  const logoutBtn = document.getElementById('logoutBtn');
  const loginBtn = document.getElementById('loginBtn');

  if (loggedInUser) {
      const user = JSON.parse(loggedInUser);
      userEmailElement.textContent = `Hello, ${user.users_name}`; // Display username
      logoutBtn.style.display = 'inline-block'; // Show logout button
      loginBtn.style.display = 'none'; // Hide login button
  } else {
      userEmailElement.textContent = ''; // Clear username
      logoutBtn.style.display = 'none'; // Hide logout button
      loginBtn.style.display = 'inline-block'; // Show login button
  }
}
updateUserInterface();
function updateCartQuantity(){
  const cartQuantity = cart.calculateCartQuantity();  
  document.querySelector('.js-cart-quantity').innerHTML = cartQuantity;
 }


document.addEventListener('DOMContentLoaded', () => {
  renderOrders();
});

function renderOrders() {
  const orders = JSON.parse(localStorage.getItem('orders')) || [];
  const orderContainer = document.querySelector('.order-container');

  let ordersHTML = '';

  orders.forEach((order, index) => {
    let orderItemsHTML = '';
    order.orderItems.forEach(item => {
      const product = getProduct(item.productId);
      orderItemsHTML += `
        <div class="order-item">
          <img src="${product.image}" alt="${product.name}" class="product-image">
          <div class="order-item-details">
            <div class="product-name">${product.name}</div>
            <div class="product-quantity">Quantity: ${item.quantity}</div>
            <div class="product-delivery-date">Arriving on: ${calculateDeliveryDate(order.orderDate, getDeliveryOption(order.deliveryOptionId))}</div>
            <div class="product-actions">
              <button class="buy-again-button" data-product-id="${product.id}">
                <img class="buy-again-icon" src="images/icons/buy-again.png">
                Buy it again
              </button>
              <button class="track-package-button" data-order-id="${order.orderID}">Track package</button>
            </div>
          </div>
        </div>
      `;
    });

    ordersHTML += `
      <div class="order-container">
        <div class="order-header">
          <div class="order-header-left-section">
            <div class="order-date">
              <div class="order-header-label">Order Placed:</div>
              <div>${formatDate(order.orderDate)}</div>
            </div>
            <div class="order-total">
              <div class="order-header-label">Total:</div>
              <div>$${formatCurrency(order.orderTotal)}</div>
            </div>
          </div>
          <div class="order-header-right-section">
            <div class="order-header-label">Order ID:</div>
            <div>${order.orderID}</div>
          </div>
        </div>
        <div class="order-details-grid">
          ${orderItemsHTML}
        </div>
        <button class="delete-order-button" data-order-index="${index}">Return Order</button>
      </div>
    `;
  });

  orderContainer.innerHTML = ordersHTML;

  // Add event listeners for buttons
  document.querySelectorAll('.buy-again-button').forEach(button => {
    button.addEventListener('click', handleBuyAgain);
  });

  document.querySelectorAll('.track-package-button').forEach(button => {
    button.addEventListener('click', handleTrackPackage);
  });

  document.querySelectorAll('.delete-order-button').forEach(button => {
    button.addEventListener('click', handleDeleteOrder);
  });
}

function handleBuyAgain(event) {
  const productId = event.currentTarget.dataset.productId;
  cart.addToCart(productId);
  alert('Product added to cart!');
}

function handleTrackPackage(event) {
  window.location.href = 'tracking.html';
}

function handleDeleteOrder(event) {
  const orderIndex = event.currentTarget.dataset.orderIndex;
  const orders = JSON.parse(localStorage.getItem('orders')) || [];
  orders.splice(orderIndex, 1);
  localStorage.setItem('orders', JSON.stringify(orders));
  renderOrders();
}

function formatDate(dateString) {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
}

function calculateDeliveryDate(orderDate, deliveryOption) {
  const date = new Date(orderDate);
  date.setDate(date.getDate() + deliveryOption.deliveryDays);
  return date.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
}

updateCartQuantity();