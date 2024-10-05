import { cart } from "../../data/cart.js";
import { getProduct } from "../../data/products.js";
import { formatCurrency } from "../utils/money.js"; 
import {calculateDeliveryDate, deliveryOptions} from "../../data/deliveryOptions.js";
import { renderOrderSummary } from "./orderSummary.js";
function deliveryOptionsHTML() {
  let html = '';

  deliveryOptions.forEach((deliveryOption) => {
    const dateString = calculateDeliveryDate(deliveryOption);
    const priceString = deliveryOption.priceCents === 0 ? 'FREE' : `$${formatCurrency(deliveryOption.priceCents)} -`;

    const isChecked = deliveryOption.id === cart.deliveryOptionId;

    html += `
      <div class="delivery-option js-delivery-option"
          data-delivery-option-id="${deliveryOption.id}">
        <input type="radio"
          ${isChecked ? 'checked' : ''}
          class="delivery-option-input"
          name="delivery-option">
        <div>
          <div class="delivery-option-date">
            ${dateString}
          </div>
          <div class="delivery-option-price">
            ${priceString} Shipping
          </div>
        </div>
      </div>
    `;
  });
  return html;
}

export function renderPaymentSummary() {
  let productPriceCents = 0;
  cart.cartItems.forEach((cartItem) => {
    const product = getProduct(cartItem.productId);
    productPriceCents += product.priceCents * cartItem.quantity;
  });

  const paymentSummaryHTML = `
    <div class="delivery-options js-delivery-options">
      <div class="delivery-options-title">
        Choose a delivery option:
      </div>
      ${deliveryOptionsHTML()}
    </div>
    <div class="payment-summary-title">
      Order Summary
    </div>
    <div class="payment-summary-row">
        <div>Items (${cart.calculateCartQuantity()}):</div>
        <div class="payment-summary-money">$${formatCurrency(productPriceCents)}</div>
      </div>

      <div class="payment-summary-row">
        <div>Shipping &amp; handling:</div>
        <div class="payment-summary-money">$${formatCurrency(cart.getDeliveryOption().priceCents)}</div>
      </div>

      <div class="payment-summary-row subtotal-row">
        <div>Total before tax:</div>
        <div class="payment-summary-money">$${formatCurrency(productPriceCents + cart.getDeliveryOption().priceCents)}</div>
      </div>

      <div class="payment-summary-row">
        <div>Estimated tax (10%):</div>
        <div class="payment-summary-money">$${formatCurrency((productPriceCents + cart.getDeliveryOption().priceCents) * 0.1)}</div>
      </div>

      <div class="payment-summary-row total-row">
        <div>Order total:</div>
        <div class="payment-summary-money">$${formatCurrency(productPriceCents + cart.getDeliveryOption().priceCents + (productPriceCents + cart.getDeliveryOption().priceCents) * 0.1)}</div>
      </div>

      <button class="place-order-button button-primary js-place-order-button">
        Place your order
      </button>
   
  `;
  

  document.querySelector('.js-payment-summary').innerHTML = paymentSummaryHTML;

  // Add event listeners for delivery options
  document.querySelectorAll('.js-delivery-option').forEach((element) => {
    element.addEventListener('click', () => {
      const { deliveryOptionId } = element.dataset;
      cart.updateDeliveryOption(deliveryOptionId);
      cart.saveToStorage(); // Ensure it saves the updated delivery option
      renderPaymentSummary(); // Re-render payment summary to reflect changes
      renderOrderSummary();
    });
  });

  // Add event listener for "Place your order" button
  document.querySelector('.js-place-order-button').addEventListener('click', () => {
    if (cart.cartItems.length === 0) {
      alert('Your cart is empty! Please add items to your cart before placing an order.');
      return;
    }
    placeOrder();
  });
}

function placeOrder() {
  // Create an order object
  const order = {
    orderID: generateOrderId(),
    orderDate: new Date().toISOString(),
    orderItems: cart.cartItems.map(item => ({
      productId: item.productId,
      quantity: item.quantity
    })),
    orderTotal: cart.calculateTotalPriceCents(),
    deliveryOptionId: cart.deliveryOptionId
  };
  
  // Save the order to localStorage
  saveOrder(order);
  
  // Clear the cart
  cart.clearCart();
  
  // Redirect to the orders page
  window.location.href = 'orders.html';
}

function generateOrderId() {
  return Math.random().toString(36).substr(2, 9);
}

function saveOrder(order) {
  const orders = JSON.parse(localStorage.getItem('orders')) || [];
  orders.push(order);
  localStorage.setItem('orders', JSON.stringify(orders));
}

renderPaymentSummary();
