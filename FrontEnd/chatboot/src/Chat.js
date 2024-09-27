// import React, { useEffect, useState } from 'react';
// import { HubConnectionBuilder } from '@microsoft/signalr';

// const Chat = () => {
//     const [messages, setMessages] = useState('');
//     const [connection, setConnection] = useState(null);

//     useEffect(() => {
//         const newConnection = new HubConnectionBuilder()
//             .withUrl('https://localhost:7154/chathub')
//             .withAutomaticReconnect()
//             .build();

//         setConnection(newConnection);
//     }, []);

//     useEffect(() => {
//         if (connection) {
//             connection.start()
//                 .then(() => {
//                     console.log('Connected to SignalR hub');

//                     connection.on('ReceiveMessage', (message) => {
//                         setMessages((prevMessages) => [...prevMessages, message]);
//                     });
//                 })
//                 .catch((error) => console.error('Connection failed: ', error));
//         }
//     }, [connection]);

//     const sendMessage = async () => {
//         await connection.invoke('SendMessage', userId, messageText);
//     };

//     return (
//         <div className="chat-window" style={{ flex: 1, padding: '10px' }}>
//             <h3>Chat <span>{userId}</span></h3>
//             <div className="messages" style={{ height: '400px', overflowY: 'scroll', border: '1px solid #ccc', padding: '10px' }}>
//                 {messages.map((msg, index) => (
//                     <div key={index}>{msg.senderId === sender ? "You: " : "User: "} {msg.messageText}</div>
//                 ))}
//             </div>
//             <input
//                 type="text"
//                 value={newMessage}
//                 onChange={(e) => setNewMessage(e.target.value)}
//                 placeholder='Enter message here'
//             />
//             <button onClick={sendMessage} style={{ width: '15%' }}>Send</button>
//         </div>
//     );
// };

// export default Chat;
