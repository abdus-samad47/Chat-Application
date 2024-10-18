import React, { useState } from 'react';
import './UpdateUser.css';


const UpdateUser = ({ isOpen, onClose, onSubmit, userInfo }) => {
    const [updateUsername, setUpdateUsername] = useState(null)
    const [updateEmail, setUpdateEmail] = useState(null);
    const [updatePhonenumber, setUpdatePhonenumber] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();
        const requestBody = {
            Username: updateUsername,
            Email: updateEmail,
            PhoneNumber: updatePhonenumber
        };
        onSubmit(requestBody);
        onClose();
    }

    if (!isOpen) return null;

    return (
        <div className="model-overlay">
            <div className="model-content">
                <form className="user-update-form" onSubmit={handleSubmit}>
                    <h3>Edit Profile</h3>
                    <label htmlFor="updateUsername">Username</label>
                    <input
                        type="text"
                        value={updateUsername}
                        onChange={(e) => setUpdateUsername(e.target.value)}
                    />
                    <label htmlFor="updateEmail">Email</label>
                    <input
                        type="email"
                        value={updateEmail}
                        onChange={(e) => setUpdateEmail(e.target.value)}
                    />
                    <label htmlFor="updatePhonenumber">Phonenumber</label>
                    <input
                        type="number"
                        value={updatePhonenumber}
                        onChange={(e) => setUpdatePhonenumber(e.target.value)}
                    />
                    <br />
                    <button className='update-button' type="submit">Update</button>
                    <br />
                    <button className='cancel-button' onClick={onClose}>Cancel</button>
                </form>
            </div>
        </div>
    )

}

export default UpdateUser;