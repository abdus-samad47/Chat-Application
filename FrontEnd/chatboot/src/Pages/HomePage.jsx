import React from "react";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
    const navigate = useNavigate();
    const handleLoginRedirect = () => {
        navigate('/login');
      };
      const handleSignupRedirect = () => {
        navigate('/signup');
      };
    return (
    <>
    <h1>Home Page</h1>;
    <button onClick={handleLoginRedirect}>Login</button>
    <button onClick={handleSignupRedirect}>Signup</button>

    </>
    )
    };
export default HomePage;