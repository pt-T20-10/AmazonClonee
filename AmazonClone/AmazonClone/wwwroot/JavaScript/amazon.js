import {products} from "../data/products.js";
import {cart} from "../data/cart.js"
let productsHTML='';
let filteredProducts = products;

function renderProducts() {
  const productsGrid = document.querySelector('.js-products-grid');
  productsHTML = '';
  
  if (filteredProducts.length === 0) {
    productsHTML = '<div class="no-results" style="text-align: center; font-size: 1.5em; color: #666; margin-top: 20px; left:50%; display: flex; align-items: center; height: 50vh; justify-content: center;"><p>No products found. The item may be out of stock or unavailable.</p></div>';
    productsGrid.classList.add('no-results');
    productsGrid.classList.remove('products-grid');
  } else {
    productsGrid.classList.remove('no-results');
    productsGrid.classList.add('products-grid');
    filteredProducts.forEach((product) => { 
      productsHTML +=`
      <div class="product-container">
      <div class="product-image-container">
        <img class="product-image"
          src="${product.image}">
      </div>

      <div class="product-name limit-text-to-2-lines">
        ${product.name}
      </div>

      <div class="product-rating-container">
        <img class="product-rating-stars"
          src="${product.getStarUrl()}">

        <div class="product-rating-count link-primary">
          ${product.rating.count}
        </div>
      </div>

      <div class="product-price">
      ${product.getPrice()}


      </div>

      <div class="product-quantity-container">
        <select class="js-quantity-selector-${product.id}">
          <option selected value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
          <option value="5">5</option>
          <option value="6">6</option>
          <option value="7">7</option>
          <option value="8">8</option>
          <option value="9">9</option>
          <option value="10">10</option>
        </select>
      </div>
          ${product.extraInfoHTML()}
      <div class="product-spacer"></div>

      <div class="added-to-cart">
        <img src="images/icons/checkmark.png">
        Added
      </div>

      <button class="add-to-cart-button   
        button-primary js-add-to-card"
        data-product-id="${product.id}"> 
        Add to Cart
      </button>
    </div>
      `;
    });
  }
  
  productsGrid.innerHTML = productsHTML;
}

function updateUserInterface() {
  const loggedInUser = localStorage.getItem('loggedInUser');
  const userEmailElement = document.getElementById('userName');
  const logoutBtn = document.getElementById('logoutBtn');
  const loginBtn = document.getElementById('loginBtn');

  if (loggedInUser) {
    const user = JSON.parse(loggedInUser);
    userEmailElement.textContent = `Hello, ${user.users_name}`;
    logoutBtn.style.display = 'inline-block';
    loginBtn.style.display = 'none';
  } else {
    userEmailElement.textContent = '';
    logoutBtn.style.display = 'none';
    loginBtn.style.display = 'inline-block';
  }

  // Add event listener to login button
  loginBtn.addEventListener('click', (event) => {
    event.preventDefault();
    window.location.href = 'login.html';
  });

  // Add event listener to logout button
  logoutBtn.addEventListener('click', (event) => {
    event.preventDefault();
    localStorage.removeItem('loggedInUser');
    updateUserInterface();
  });
}

function isUserLoggedIn() {
  return localStorage.getItem('loggedInUser') !== null;
}

function requireLogin(event, targetUrl) {
  if (!isUserLoggedIn()) {
    event.preventDefault();
    alert('Please log in to access this feature.');
    window.location.href = 'login.html';
  } else {
    window.location.href = targetUrl;
  }
}

document.addEventListener('DOMContentLoaded', () => {
  updateUserInterface();

  // Add event listeners for header buttons
  const ordersLink = document.querySelector('.orders-link');
  const cartLink = document.querySelector('.cart-link');
  const trackingLink = document.querySelector('.tracking-link');

  if (ordersLink) {
    ordersLink.addEventListener('click', (event) => {
      requireLogin(event, 'orders.html');
    });
  }

  if (cartLink) {
    cartLink.addEventListener('click', (event) => {
      requireLogin(event, 'checkout.html');
    });
  }

  if (trackingLink) {
    trackingLink.addEventListener('click', (event) => {
      requireLogin(event, 'tracking.html');
    });
  }

  // ... existing event listeners and code ...
});

// Initial render
renderProducts();

// Add event listeners for search
document.getElementById('searchInput').addEventListener('input', (event) => {
  const searchQuery = event.target.value.toLowerCase();
  filteredProducts = products.filter(product => 
    product.name.toLowerCase().includes(searchQuery) || 
    product.keywords.some(keyword => keyword.toLowerCase().includes(searchQuery)) ||
    (product.type && product.type.toLowerCase().includes(searchQuery)) // Add a check for type
  );
  renderProducts(); // Call renderProducts without arguments
});

document.getElementById('searchInput').addEventListener('keypress', (event) => {
  if (event.key === 'Enter') {
    const searchQuery = event.target.value.toLowerCase();
    filteredProducts = products.filter(product => 
      product.name.toLowerCase().includes(searchQuery) || 
      product.keywords.some(keyword => keyword.toLowerCase().includes(searchQuery)) ||
      (product.type && product.type.toLowerCase().includes(searchQuery)) // Add a check for type
    );
    renderProducts(); // Call renderProducts without arguments
  }
});

document.getElementById('searchButton').addEventListener('click', () => {
  const searchQuery = document.getElementById('searchInput').value;
  renderProducts(searchQuery);
});

export function updateCartQuantity(){
    const cartQuantity = cart.calculateCartQuantity();  
    document.querySelector('.js-cart-quantity').innerHTML = cartQuantity;
   }
 
   updateCartQuantity();

  document.querySelectorAll('.js-add-to-card').forEach((button) => {
    button.addEventListener('click', () => {
      // dataset give all the data attributes 
        const productId = button.dataset.productId;
        cart.addToCart(productId);
        updateCartQuantity();
      });
  });

document.querySelectorAll('.back-to-top').forEach(function(element) {
  element.addEventListener('click', function(e) {
    e.preventDefault();
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  });
});
