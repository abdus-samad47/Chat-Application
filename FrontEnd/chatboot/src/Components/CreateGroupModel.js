import React, { useState } from 'react';
import './CreateGroupModel.css';

const CreateGroupModel = ({ isOpen, onClose, onSubmit, users, createdBy }) => {
    const [roomName, setRoomName] = useState('');
    const [selectedUserIds, setSelectedUserIds] = useState([]);

    const handleUserSelect = (userId) => {
        setSelectedUserIds(prevSelected => 
            prevSelected.includes(userId)
                ? prevSelected.filter(id => id !== userId) 
                : [...prevSelected, userId] 
        );
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const requestBody = {
            roomName,
            createdBy,
            users: selectedUserIds
        };
        onSubmit(requestBody);
        onClose(); 
    };

    if (!isOpen) return null;

    return (
        <div className="model-overlay">
            <div className="model-content">
                <form className='create-group-form' onSubmit={handleSubmit}>
                <h2>Create Group</h2>
                    <input
                        type="text"
                        placeholder="Group Name"
                        value={roomName}
                        onChange={(e) => setRoomName(e.target.value)}
                        required
                    /><h3>SELECT USERS</h3>
                    <div className="user-list">
                        {users.map(user => (
                            <div key={user.userId}>
                                <label>
                                    <input
                                        type="checkbox"
                                        checked={selectedUserIds.includes(user.userId)}
                                        onChange={() => handleUserSelect(user.userId)}
                                    />
                                    {user.username}
                                </label>
                            </div>
                        ))}
                    </div>
                    <button className='submit-button' type="submit">Create Group</button>
                    <button className='cancel-button' type="button" onClick={onClose}>Cancel</button>
                </form>
            </div>
        </div>
    );
};

export default CreateGroupModel;
