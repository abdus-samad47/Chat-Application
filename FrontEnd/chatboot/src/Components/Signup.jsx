import React, { useState } from 'react';
import axios from 'axios';

const Signup = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [phonenumber, setPhoneNumber] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      // Make API call to create a user
      const response = await axios.post('http://localhost:5268/api/Users', {
        Username: username,
        Email: email,
        PhoneNumber: phonenumber,
        PasswordHash: password
      });

      // Handle successful response
      setSuccess('Signup successful! Please log in.');
      setError('');
      // Optionally clear the form or redirect
      setUsername('');
      setEmail('');
      setPhoneNumber('');
      setPassword('');
    } catch (err) {
      // Handle errors
      if (err.response && err.response.data) {
        setError(err.response.data);
      } else {
        setError('Signup failed. Please try again.');
      }
      setSuccess('');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Signup</h2>
      <label>Username: </label>
      <input
        type="text"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="Username"
        required
      />
      <br/>
      <label>Email: </label>
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="Email"
        required
      />
      <br />
      <label>Phone Number: </label>
      <input
        type="text"
        value={phonenumber}
        onChange={(e) => setPhoneNumber(e.target.value)}
        placeholder="Phone Number"
      />
      <br />
      <label>Password: </label>
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
        required
      />
      <br />
      <button type="submit">Signup</button>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {success && <p style={{ color: 'green' }}>{success}</p>}
    </form>
  );
};

export default Signup;
