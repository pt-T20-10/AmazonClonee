import { products } from '../data/products.js';
import { formatCurrency } from './utils/money.js';

document.addEventListener('DOMContentLoaded', () => {
    loadOrders();
    loadProducts();
    loadAnalytics();

    document.getElementById('addProductBtn').addEventListener('click', addNewProduct);
});

function loadOrders() {
    const orders = JSON.parse(localStorage.getItem('orders')) || [];
    const orderList = document.getElementById('orderList');
    
    if (orders.length === 0) {
        orderList.innerHTML = '<p>No orders found.</p>';
        return;
    }

    let html = '<table><tr><th>Order ID</th><th>Date</th><th>Total</th><th>Status</th></tr>';
    orders.forEach(order => {
        html += `
            <tr>
                <td>${order.orderID}</td>
                <td>${new Date(order.orderDate).toLocaleDateString()}</td>
                <td>$${formatCurrency(order.orderTotal)}</td>
                <td>
                    <select onchange="updateOrderStatus('${order.orderID}', this.value)">
                        <option value="processing" ${order.status === 'processing' ? 'selected' : ''}>Processing</option>
                        <option value="shipped" ${order.status === 'shipped' ? 'selected' : ''}>Shipped</option>
                        <option value="delivered" ${order.status === 'delivered' ? 'selected' : ''}>Delivered</option>
                    </select>
                </td>
            </tr>
        `;
    });
    html += '</table>';
    orderList.innerHTML = html;
}

function loadProducts() {
    const productList = document.getElementById('productList');
    
    let html = '<table><tr><th>ID</th><th>Name</th><th>Price</th><th>Actions</th></tr>';
    products.forEach(product => {
        html += `
            <tr>
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td>$${formatCurrency(product.priceCents)}</td>
                <td>
                    <button onclick="editProduct('${product.id}')">Edit</button>
                    <button onclick="deleteProduct('${product.id}')">Delete</button>
                </td>
            </tr>
        `;
    });
    html += '</table>';
    productList.innerHTML = html;
}

function loadAnalytics() {
    const orders = JSON.parse(localStorage.getItem('orders')) || [];
    const totalRevenue = orders.reduce((sum, order) => sum + order.orderTotal, 0);
    const totalOrders = orders.length;

    const analyticsData = document.getElementById('analyticsData');
    analyticsData.innerHTML = `
        <p>Total Revenue: $${formatCurrency(totalRevenue)}</p>
        <p>Total Orders: ${totalOrders}</p>
    `;
}

function addNewProduct() {
    // Implement a form or modal to add a new product
    console.log('Add new product functionality to be implemented');
}

// These functions need to be global to work with inline event handlers
window.updateOrderStatus = function(orderId, newStatus) {
    const orders = JSON.parse(localStorage.getItem('orders')) || [];
    const updatedOrders = orders.map(order => {
        if (order.orderID === orderId) {
            return { ...order, status: newStatus };
        }
        return order;
    });
    localStorage.setItem('orders', JSON.stringify(updatedOrders));
    loadOrders();
};

window.editProduct = function(productId) {
    // Implement edit product functionality
    console.log('Edit product functionality to be implemented for product:', productId);
};

window.deleteProduct = function(productId) {
    if (confirm('Are you sure you want to delete this product?')) {
        // Implement delete product functionality
        console.log('Delete product functionality to be implemented for product:', productId);
    }
};