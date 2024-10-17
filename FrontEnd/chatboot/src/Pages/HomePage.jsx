import React from "react";
import { useNavigate } from "react-router-dom";
import './HomePage.css';

const HomePage = () => {
  const navigate = useNavigate();
  const handleLoginRedirect = () => {
    navigate('/login');
  };
  const handleSignupRedirect = () => {
    navigate('/signup');
  };
  return (
    <div id="home-page">
      <h1 id="homepage-heading">Home Page</h1>
      <button id="homepage-button" onClick={handleLoginRedirect}>Login</button>
      <button id="homepage-button" onClick={handleSignupRedirect}>Signup</button>
    </div>
  )
};
export default HomePage;