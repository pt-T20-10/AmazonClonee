import { users } from '../JavaScript/userInformation.js';

document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.querySelector('#loginForm'); // Moved inside DOMContentLoaded
    console.log('loginForm:', loginForm); // Check if loginForm is null
    const identifierInput = document.getElementById('identifier');
    const passwordInput = document.getElementById('password');

    if (!loginForm) {
        console.error('Login form not found!'); // Log an error if loginForm is null
        return; // Exit if the form is not found
    }

    loginForm.addEventListener('submit', (e) => {
        e.preventDefault();
        const identifier = identifierInput.value;
        const password = passwordInput.value;

        console.log('Login attempt:', identifier, password); // Add this line for debugging

        // Check if the identifier (email or phone) exists in our database
        const user = users.find(u => u.email === identifier || u.phone === identifier);

        if (user && user.password === password) {
            console.log('Login successful'); // Add this line for debugging
            localStorage.setItem('loggedInUser', JSON.stringify(user));
            window.location.href = 'amazon.html';
        } else {
            console.log('Login failed'); // Add this line for debugging
            alert('Invalid credentials. Please try again.');
        }
    });

    console.log('Login form event listener attached'); // Add this line for debugging
});

// Check if user is already logged in
const loggedInUser = localStorage.getItem('loggedInUser');
if (loggedInUser) {
    window.location.href = 'amazon.html'; // Redirect to main Amazon page if already logged in
}