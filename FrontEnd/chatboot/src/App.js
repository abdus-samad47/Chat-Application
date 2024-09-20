import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './Components/Login';
import Signup from './Components/Signup';
// import UserPage from './Pages/UserPage';
import HomePage from './Pages/HomePage';
// import UserPage from './Pages/FetchUserById';
import ChatPage from './Components/UserChatPage';

function App() {
  // const userId = parseInt(5);
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/chat/:userId" element={<ChatPage />} />
          {/* // { <Route path="/users" element={<UserPage />} /> } */}
          {/* <Route path="/fetch" element={<UserPage id={userId}/>} /> */}
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/" element={<HomePage/>} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
