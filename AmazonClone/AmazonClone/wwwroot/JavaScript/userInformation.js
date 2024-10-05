 let users = [
    {
        users_id: "US-2023-01-01-1234567890",
        users_name: "John Doe",
        email: "user@example.com",
        phone: "1234567890",
        password: "12345"
    },
    {
        users_id: "US-2023-01-02-9876543210",
        users_name: "Jane Smith",
        email: "johndoe@example.com",
        phone: "9876543210",
        password: "12345"
    },
    {
        users_id: "US-2023-01-03-1234567890",
        users_name: "Amazon",
        email: "amazonnhom7@gmail.com",
        phone: "1234567890",
        password: "12345"
    }
];

export function updateUserInformation(newUser) {
    // Generate user ID based on registration time and country code
    const currentTime = new Date();
    const userId = `${newUser.countryCode}-${currentTime.getFullYear()}-${currentTime.getMonth() + 1}-${currentTime.getDate()}-${newUser.phone}`;

    // Check if the user already exists by email or phone
    const existingUserIndex = users.findIndex(user => 
        user.email === newUser.email || user.phone === newUser.phone
    );

    if (existingUserIndex !== -1) {
        // Update existing user
        users[existingUserIndex] = { ...users[existingUserIndex], ...newUser, users_id: userId };
    } else {
        // Add new user with generated ID
        users.push({ ...newUser, users_id: userId });
    }

    // In a real application, you would save this to a database
    console.log('User information updated:', users);
}
export {users};