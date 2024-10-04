import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import CreateGroupModel from './CreateGroupModel';
import './UserTable.css';

const ChatPage = () => {
    const { userId } = useParams();
    const [users, setUsers] = useState([]);
    const [groups, setGroups] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState(null);
    const [selectedGroupId, setSelectedGroupId] = useState(null);
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const sender = Number(userId);
    const token = sessionStorage.getItem('jwtToken');
    const userjson = sessionStorage.getItem('user');
    const userInfo = JSON.parse(userjson);
    const senderId = userInfo.userId
    const [connection, setConnection] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    // Fetch users on component mount
    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await axios.get('http://localhost:5268/api/Users/', {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });
                console.log("Fetched users:", response.data.$values);
                setUsers(response.data.$values);
            } catch (err) {
                console.error("Error fetching users:", err.response ? err.response.data : err.message);
            }
        };

        const fetchGroups = async () => {
            try {
                const response = await axios.get('http://localhost:5268/api/ChatRoom', {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });
                console.log("Fetched groups:", response.data);
                setGroups(response.data.$values);
            } catch (err) {
                console.error("Error fetching groups:", err.response ? err.response.data : err.message);
            }
        };

        if (token) {
            fetchUsers();
            fetchGroups();
        }
    }, [token]);

    // Initialize SignalR connection
    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5268/chathub',{
                accessTokenFactory: () => sessionStorage.getItem('jwtToken'),
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);

        // Start the connection and set up listeners
        const startConnection = async () => {
            try {
                await newConnection.start();
                console.log('Connected to SignalR hub');

                console.log("SignalR Connection State:", newConnection.state);

                console.log("signalR connectionId: ", newConnection.connectionId);
                
                if (newConnection.state === HubConnectionState.Connected) {
                    console.log('Connection is established.');
                } else {
                    console.error('Connection not established:', newConnection.state);
                }

                // Listen for incoming messages
                newConnection.on('ReceiveMessage', (message) => {
                    try{
                        const receiver = Number(message.receiverId);
                        if ((receiver === selectedUserId && message.senderId === senderId) || (receiver === senderId && message.senderId === selectedUserId)){
                            console.log("Message Received: ", message)
                            setMessages((prevMessages) => [...prevMessages, message]);
                        }
                    }
                    catch(error){
                        console.log("Error catching receiving message ", error);
                    }
                });
            } catch (error) {
                console.error('Connection failed: ', error);
            }
        };

        startConnection();

        // Cleanup on unmount
        return () => {
            if(newConnection){
                newConnection.stop();
            }
        };
    }, [selectedUserId, senderId]);

    // Handle user selection and fetch conversation
    const handleUserSelect = async (userId) => {
        setSelectedUserId(userId);
        try {
            const response = await axios.get(`http://localhost:5268/api/ChatMessages/conversation?receiverId=${userId}&senderId=${sender}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setMessages(response.data.$values);
        } catch (err) {
            console.error("Error fetching conversation:", err);
        }
    };

    const handleGroupSelect = async (selectedGroupId) => {
        const response = await axios.get()
    }

    // Send a message via SignalR
    const sendMessage = async () => {
        if (!newMessage || !selectedUserId || !connection) return;

        const message = {
            messageText: newMessage,
            senderId: senderId,
            receiverId: `${selectedUserId}`,
            sentAt: new Date().toISOString() 
        };

            console.log(message.senderId);
            console.log(message.receiverId);

        // Ensure the connection is started
        if (connection.state === HubConnectionState.Disconnected) {
            try {
                await connection.start();
            } catch (error) {
                console.error("Error starting connection:", error);
                return;
            }
        }

        // Send message through SignalR
        try {
            console.log(message);
            await connection.invoke('SendMessage', message);
            console.log("Message sent:", newMessage);
            setNewMessage('');
        } catch (error) {
            console.error("Error sending message:", error);
        }
    };

    const handleCreateGroup = async (requestBody) => {
        try {
            const response = await axios.post('http://localhost:5268/api/ChatRoom', requestBody, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
    
            console.log("Group created successfully:", response.data);
        } catch (error) {
            console.error("Error creating group:", error);
        }
    };

    const handleLogout = () => {
        sessionStorage.removeItem('jwtToken');
        sessionStorage.removeItem('user');
        window.location.href = '/login';
    };

    return (
        <div className="chat-page" style={{ display: 'flex' }}>
            <div className="sidebar" style={{ width: '22%', borderRight: '1px solid #ccc', padding: '10px' }}>
                <h3>Users</h3>
                <div className='list-of-users'>
                {users.map(user => (
                    <div key={user.userId} onClick={() => handleUserSelect(user.userId)} style={{ cursor: 'pointer' }}>
                        {user.username}
                    </div>
                ))}
                </div>
                <h3>Chat Groups</h3>
                <div className='list-of-groups'>
                <button onClick={() => setIsModalOpen(true)}>+ Create Group</button>
                <div className="group-list">
                        {groups.map(group => (
                            <div key={group.id} style={{ padding: '5px', border: '1px solid #ccc', marginTop: '5px' }}>
                                {group.roomName}
                            </div>
                        ))}
                    </div>
                </div>
            </div>
            <div className="chat-window" style={{ flex: 1, padding: '10px' }}>
                <h3>Chat <span>{userInfo.username}</span> <span className='logout'><button onClick={handleLogout}>Logout</button></span></h3>
                <div className="messages" style={{ height: '400px', overflowY: 'scroll', border: '1px solid #ccc', padding: '10px' }}>
                    {messages.map((msg, index) => (
                        <div key={index}>{msg.senderId === sender ? "You: " : "User: "} {msg.messageText}</div>
                    ))}
                </div>
                <input
                    type="text"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    placeholder='Enter message here'
                />
                <button onClick={sendMessage} style={{ width: '15%' }}>Send</button>
            </div>
            <CreateGroupModel 
                isOpen={isModalOpen} 
                onClose={() => setIsModalOpen(false)} 
                onSubmit={handleCreateGroup}
                users={users}
                createdBy={userInfo.userId}
            />
        </div>
    );
};

export default ChatPage;
