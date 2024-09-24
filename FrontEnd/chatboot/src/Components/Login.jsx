import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './login.css';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const navigate = useNavigate();
// 
  const handleSubmit = async (event) => {
    event.preventDefault();
    console.log('form submitted');
    try {
      // POST request to the login endpoint
      console.log('making api call')
      const response = await axios.post('http://localhost:5268/api/Users/Verify', {
        Username: username,
        Password: password
      });
      
      const token = response.data.token;
      console.log(token)
      
      localStorage.setItem('jwtToken', null)
      localStorage.setItem('jwtToken', token)
      setSuccess('Login successful!');
      
      // Assuming a successful response includes user data
      localStorage.setItem('user', JSON.stringify(response.data.userDTO));
      const {userId} = response.data.userDTO;
      console.log(userId);

      // Redirect or handle successful login
      navigate(`/chat/${userId}`); 
    } catch (err) {
      // Handle errors
      console.error('Login error:', err.response ? err.response.data : err.message);

      // Display error message based on response
      
      setError(err.response && err.response.data ? err.response.data : 'Login failed. Please check your credentials.');
    }
  };

  return (
    <center>
    <form onSubmit={handleSubmit}>
      <h2>Login</h2>
      <div>
        <label htmlFor="username">Username: </label>
        <input
          id="username"
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="Username"
          required
          />
      </div>
      <br />
      <div>
        <label htmlFor="password">Password: </label>
        <input
          id="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Password"
          required
          />
      </div>
      <br />
      <button type="submit">Login</button>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {success && <p style={{ color: 'green' }}>{success}</p>}
    </form>
  </center>
  );
};

export default Login;
