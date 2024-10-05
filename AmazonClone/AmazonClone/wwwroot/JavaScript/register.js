import { users, updateUserInformation } from './userInformation.js';

const registerForm = document.getElementById('registerForm');
const nameInput = document.getElementById('name');
const emailInput = document.getElementById('email');
const passwordInput = document.getElementById('password');
const passwordConfirmInput = document.getElementById('passwordConfirm');

registerForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const name = nameInput.value;
    const email = emailInput.value;
    const password = passwordInput.value;
    const passwordConfirm = passwordConfirmInput.value;

    if (password !== passwordConfirm) {
        alert('Passwords do not match. Please try again.');
        return;
    }

    if (password.length < 6) {
        alert('Password must be at least 6 characters long.');
        return;
    }

    // Check if the email already exists in our database
    const existingUser = users.find(u => u.email === email);

    if (existingUser) {
        alert('An account with this email already exists. Please sign in or use a different email.');
        return;
    }

    // Add the new user to the database
    const newUser = { users_name: name, email: email,phone: phone, password: password, };
    updateUserInformation(newUser);

    alert('Account created successfully! Redirecting to login page...');
    window.location.href = 'login.html';
});