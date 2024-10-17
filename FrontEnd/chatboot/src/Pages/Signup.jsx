import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './login.css';

const Signup = () => {
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [phonenumber, setPhoneNumber] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [showLoginButton, setShowLoginButton] = useState(false);
  const navigate = useNavigate();

  const validateForm = () => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if(!emailRegex.test(email)){
      setError("Invalid Email");
      return false;
    }
    if(password.length < 6){
      setError("Minimum password length should be 6 characters");
      return false;
    }
    setError("");
    return true;
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if(!validateForm()) return;
    try {
      // Make API call to create a user
      const response = await axios.post('http://localhost:5268/api/Users', {
        Username: username,
        Email: email,
        PhoneNumber: phonenumber,
        PasswordHash: password
      });

      setSuccess('Signup successful! Please log in.');
      setShowLoginButton(true);
      setError('');
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
      setShowLoginButton(false);
    }
  };
  const handleLoginRedirect = () => {
    navigate('/login');
  };

  return (
    <center>
    <form className='signup-form' onSubmit={handleSubmit}>
      <h2>Signup</h2>
      <label className='label'>Username </label>
      <input
      className='input'
        type="text"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="Username"
        required
        />
      <br/>
      <br/>
      <label className='label'>Email </label>
      <input
        className='input'
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        placeholder="Email"
        required
        />
      <br />
      <br />
      <label className='label'>Phone Number </label>
      <input
        className='input'
        type="text"
        value={phonenumber}
        onChange={(e) => setPhoneNumber(e.target.value)}
        placeholder="Phone Number"
        />
      <br />
      <br />
      <label className='label'>Password </label>
      <input
        className='input'
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
        required
        />
      <br />
      <br />
      <button id='button' type="submit">Signup</button>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {success && <p style={{ color: 'green' }}>{success}</p>}
      {showLoginButton && (
        <button type="button" id='button' onClick={handleLoginRedirect}>
          Go to Login Page
        </button>
      )}
    </form>
  </center>
  );
};

export default Signup;
