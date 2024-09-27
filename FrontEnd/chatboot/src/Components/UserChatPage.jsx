import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import './UserTable.css';

const ChatPage = () => {
    const { userId } = useParams();
    const [users, setUsers] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState(null);
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const sender = Number(userId);
    const token = sessionStorage.getItem('jwtToken');
    const userjson = sessionStorage.getItem('user');
    const userInfo = JSON.parse(userjson);
    const [connection, setConnection] = useState(null);

    // Fetch users on component mount
    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await axios.get('http://localhost:5268/api/Users/', {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });
                setUsers(response.data);
            } catch (err) {
                console.error("Error fetching users:", err.response ? err.response.data : err.message);
            }
        };

        if (token) {
            fetchUsers();
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

                // Listen for incoming messages
                newConnection.on('ReceiveMessage', (message) => {
                    setMessages((prevMessages) => [...prevMessages, message]);
                });
            } catch (error) {
                console.error('Connection failed: ', error);
            }
        };

        startConnection();

        // Cleanup on unmount
        return () => {
            newConnection.stop();
        };
    }, []);

    // Handle user selection and fetch conversation
    const handleUserSelect = async (userId) => {
        setSelectedUserId(userId);
        try {
            const response = await axios.get(`http://localhost:5268/api/ChatMessages/conversation?receiverId=${userId}&senderId=${sender}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            setMessages(response.data);
        } catch (err) {
            console.error("Error fetching conversation:", err);
        }
    };

    // Send a message via SignalR
    const sendMessage = async () => {
        if (!newMessage || !selectedUserId || !connection) return;

        const message = {
            messageText: newMessage,
            senderId: sender,
            receiverId: selectedUserId,
            sentAt: new Date().toISOString() 
        };

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
            await connection.invoke('SendMessage', selectedUserId, newMessage);
            console.log("Message sent:", newMessage);
            setNewMessage('');
        } catch (error) {
            console.error("Error sending message:", error);
        }
    };

    const handleLogout = () => {
        sessionStorage.removeItem('jwtToken');
        sessionStorage.removeItem('user');
        window.location.href = '/login';
    };

    return (
        <div className="chat-page" style={{ display: 'flex' }}>
            <div className="sidebar" style={{ width: '20%', borderRight: '1px solid #ccc', padding: '10px' }}>
                <h3>Users</h3>
                {users.map(user => (
                    <div key={user.userId} onClick={() => handleUserSelect(user.userId)} style={{ cursor: 'pointer' }}>
                        {user.username}
                    </div>
                ))}
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
        </div>
    );
};

export default ChatPage;
