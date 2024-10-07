export function formatCurrency(priceCents) {
    return (Math.round(priceCents) / 100). toFixed(2);
} 
document.addEventListener("DOMContentLoaded", function () {
    var prices = document.querySelectorAll(".price");

    prices.forEach(function (priceElem) {
        var priceCents = parseInt(priceElem.dataset.price, 10);
        var formattedPrice = formatCurrency(priceCents);  // Gọi hàm formatCurrency từ money.js
        priceElem.innerText = formattedPrice;
    });
});
