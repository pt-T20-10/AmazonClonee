import { getProduct } from "../../data/products.js";
import dayjs from 'https://unpkg.com/dayjs@1.11.10/esm/index.js';
import { getDeliveryOption, calculateDeliveryDate } from "../../data/deliveryOptions.js";
import { renderPaymentSummary } from "./paymentSummary.js";
import { renderCheckoutHeader } from "./checkoutHeader.js";
import { cart } from "../../data/cart.js";

   
export function renderOrderSummary(){
  let deliveryOptionId = cart.deliveryOptionId;
  let deliveryOption = getDeliveryOption(deliveryOptionId);
  let dateString = calculateDeliveryDate(deliveryOption);
  let cartSummaryHTML = '';
  cart.cartItems.forEach((cartItem) => {

    const productId = cartItem.productId;
    const matchingProduct = getProduct(productId);
   ;
    

    cartSummaryHTML += `
        <div class="cart-item-container  
        js-cart-item-container
        js-cart-item-container-${matchingProduct.id}">
          <div class="delivery-date">
            Delivery date: ${dateString}
          </div>

          <div class="cart-item-details-grid">
            <img class="product-image"
              src="${matchingProduct.image}">

            <div class="cart-item-details">
              <div class="product-name">
                ${matchingProduct.name}
              </div>
              <div class="product-price">
                ${matchingProduct.getPrice()}
              </div>
              <div class="product-quantity">
                <span>
                  Quantity: <span class="quantity-label 
                  js-quantity-label-${matchingProduct.id}">${cartItem.quantity}</span>
                </span>
                <span class="update-quantity-link link-primary js-update-link"
                data-product-id="${matchingProduct.id}">
                  Update
                </span>
                <input class="quantity-input js-save-quantity-input 
                js-quantity-input-${matchingProduct.id}">
                <span class="save-quantity-link link-primary js-save-quantity-link"
                data-product-id="${matchingProduct.id}">
                  Save 
                </span>
                <span class="delete-quantity-link link-primary 
                            js-delete-link" 
                            data-product-id="${matchingProduct.id}">
                  Delete
                </span>
              </div>
            </div>
          </div>
        </div>
    `;
  });

  document.querySelector('.js-order-summary').innerHTML = cartSummaryHTML;

  // Event delegation: Attach a single event listener to the parent container
  
  async function changeQuantity(productId) {
    const container = document.querySelector(`.js-cart-item-container-${productId}`);
    container.classList.remove('is-editing-quantity');
  
    const quantityInput = document.querySelector(`.js-quantity-input-${productId}`);
    const newQuantity = Number(quantityInput.value);
  
    if (newQuantity < 0 || newQuantity >= 1000) {
      alert('Quantity must be at least 0 and less than 1000');
      return;
    }

    // Check if the new quantity is zero, and if so, remove the product
    if (newQuantity === 0) {
      cart.removeFromCart(productId);
      renderPaymentSummary();
      renderCheckoutHeader();
      renderOrderSummary();
      return; // Exit the function to prevent further processing
    }
  
    // Đợi cập nhật số lượng hoàn tất
    await cart.updateQuantity(productId, newQuantity);
  
    const quantityLabel = document.querySelector(`.js-quantity-label-${productId}`);
    quantityLabel.innerHTML = newQuantity;
  
    // Sau khi cập nhật xong, render lại
    renderCheckoutHeader();
    renderPaymentSummary();
    renderOrderSummary();
  }

  document.querySelector('.js-order-summary').addEventListener('click', (event) => {
    // Handle Save Quantity
    if (event.target.classList.contains('js-save-quantity-link')) {
      const productId = event.target.dataset.productId;
      changeQuantity(productId);
      renderCheckoutHeader();
      renderPaymentSummary();
      renderOrderSummary();
    }
    // Handle Delete
    if (event.target.classList.contains('js-delete-link')) {
      const productId = event.target.dataset.productId;
      cart.removeFromCart(productId); 
      renderPaymentSummary();
      renderCheckoutHeader();
      renderOrderSummary(); 
    }

    // Handle Update (show input for editing)
    if (event.target.classList.contains('js-update-link')) {
      const productId = event.target.dataset.productId;
      const container = document.querySelector(`.js-cart-item-container-${productId}`);
      container.classList.add('is-editing-quantity');
    }
  });

  // For quantity input, use the 'keydown' event delegation
  document.querySelector('.js-order-summary').addEventListener('keydown', (event) => {
    if (event.target.classList.contains('js-save-quantity-input')) {
      if (event.key === 'Enter') {
        // Thay vì lấy productId từ event.target.dataset.productId (vì input không có dataset)
        // Ta sẽ lấy productId từ attribute 'data-product-id' của input
        const productId = event.target.closest('.js-cart-item-container').querySelector('.js-save-quantity-link').dataset.productId;
        changeQuantity(productId); // Cập nhật số lượng sản phẩm
        renderCheckoutHeader(); // Render lại phần header
        renderPaymentSummary(); // Render lại phần thanh toán
      }
    }
  });

  // Handle Delivery Option changes
  document.querySelector('.js-order-summary').addEventListener('click', (event) => {
    if (event.target.classList.contains('js-delivery-option')) {
      const { deliveryOptionId } = event.target.dataset;
      cart.updateDeliveryOption(deliveryOptionId);
      renderCheckoutHeader(); // Render lại phần header
      renderPaymentSummary();
    
    }
  });
}

// Function to change quantity





