import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import './UserTable.css';

const ChatPage = () => {
    const {userId} = useParams();
    const [users, setUsers] = useState([]);
    const [selectedUserId, setSelectedUserId] = useState(null);
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const roomId = null;
    const sender = Number(userId);

    useEffect(() => {
        const fetchUsers = async () => {
            const response = await axios.get('http://localhost:5268/api/Users/');
            setUsers(response.data);
        };
        fetchUsers();
    }, []);

    const handleUserSelect = async (userId) => {
        setSelectedUserId(userId);
        try{
            const response = await axios.get(`http://localhost:5268/api/ChatMessages/conversation?receiverId=${userId}&senderId=${sender}`);
            setMessages(response.data);
        }
        catch(err){
            console.log("No Response")
        }
    };

    const sendMessage = async () => {
        if (!newMessage || !selectedUserId) return;

        const message = {
            messageText: newMessage,
            senderId: sender,
            receiverId: selectedUserId,
            chatRoomId: roomId,
        };

        await axios.post('http://localhost:5268/api/ChatMessages/', message);
        setMessages([...messages, message]);
        setNewMessage('');
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
                <h3>Chat <span className='logout'><button>Logout</button></span></h3>
                <div className="messages" style={{ height: '400px', overflowY: 'scroll', border: '1px solid #ccc', padding: '10px' }}>
                    {messages.map((msg, index) => (
                        <div key={index}>{msg.senderId === sender? "You: " : "User: "} {msg.messageText}</div>
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
