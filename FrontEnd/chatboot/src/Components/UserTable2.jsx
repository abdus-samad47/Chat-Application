import React, { useState, useEffect } from "react";
import { fetchUsers2 } from "../Services/UserService2";

export const UserTable2 = (id) => {
    const [user, setUser] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadUsers = async () => {
            if (!id) {
                setError("Id is required");
                setLoading(false);
                return;
            }
            try {
                // console.log(typeof(id))
                const usersData = await fetchUsers2(id);
                console.log(usersData);
                setUser(usersData);  // Corrected this line
                // if (Array.isArray(usersData)) {
                // } else {
                //     console.error("Unexpected data format", usersData);
                //     setUser([]);  // Clear users array if data format is unexpected
                // }
            } catch (err) {
                console.error("Error fetching users", err);
                setError("Failed to load users");
            } finally {
                setLoading(false); // Data fetching is done
            }
        };

        loadUsers();
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>; // Handle error case
    }

    return (
        <div>
      <h2>Child Component</h2>
      <p><strong>User Name:</strong> {user.username}</p>
      <p><strong>Email:</strong> {user.email}</p>
      <p><strong>Phone Number:</strong> {user.phoneNumber}</p>
    </div>
    );
};
