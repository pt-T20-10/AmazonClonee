import { getDeliveryOption } from "./deliveryOptions.js";
import { getProduct } from "./products.js";

class Cart {
  cartItems;
  #localStorageKey;
  deliveryOptionId;

  constructor(localStorageKey) {
    this.#localStorageKey = localStorageKey;
    this.deliveryOptionId = '1'; // Initialize with free shipping option
    this.#loadFromStorage();
  }

  #loadFromStorage() {
    try {
      const savedData = JSON.parse(localStorage.getItem(this.#localStorageKey)) || {};
      this.cartItems = savedData.cartItems || [];
      this.deliveryOptionId = savedData.deliveryOptionId || '1'; // Set to default '1' if not found
    } catch (e) {
      this.cartItems = [];
      this.deliveryOptionId = '1'; // Set to default '1' in case of error
    }
  }

  saveToStorage() {
    const dataToSave = {
      cartItems: this.cartItems,
      deliveryOptionId: this.deliveryOptionId, // Also save delivery option
    };
    localStorage.setItem(this.#localStorageKey, JSON.stringify(dataToSave));
  }

  addToCart(productId) {
    let matchingItem = this.cartItems.find(cartItem => cartItem.productId === productId);

    if (matchingItem) {
      matchingItem.quantity += 1;
    } else {
      this.cartItems.push({
        productId: productId,
        quantity: 1,
      });
    }
    this.saveToStorage();
  }
  removeFromCart(productId) {
    this.cartItems = this.cartItems.filter(cartItem => cartItem.productId !== productId);
    this.saveToStorage();
  }
  updateDeliveryOption(deliveryOptionId) {
    this.deliveryOptionId = deliveryOptionId; // Save the selected delivery option
    this.saveToStorage();
  }

  getDeliveryOption() {
    return getDeliveryOption(this.deliveryOptionId); // Return the selected delivery option object
  }

  calculateCartQuantity() {
    let cartQuantity = this.cartItems.reduce((total, cartItem) => total + cartItem.quantity, 0);

    return cartQuantity;
  }

  updateQuantity(productId, newQuantity) {
    let matchingItem = this.cartItems.find(cartItem => cartItem.productId === productId);
    
    if (matchingItem) {
      if (newQuantity <= 0) {
        this.cartItems = this.cartItems.filter(cartItem => cartItem.productId !== productId);
      } else {
        matchingItem.quantity = newQuantity;
      }
      this.saveToStorage();
    }
  }

  calculateTotalPriceCents() {
    let total = 0;
    this.cartItems.forEach(item => {
      const product = getProduct(item.productId);
      total += product.priceCents * item.quantity;
    });
    const deliveryOption = this.getDeliveryOption();
    total += deliveryOption.priceCents;
    return total;
  }

  clearCart() {
    this.cartItems = [];
    this.saveToStorage();
  }
}

export const cart = new Cart('cart-oop');
