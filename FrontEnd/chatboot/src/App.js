import './App.css';
import { Navigate, BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './Pages/Login';
import Signup from './Pages/Signup';
// import UserPage from './Pages/UserPage';
import HomePage from './Pages/HomePage';
// import UserPage from './Pages/FetchUserById';
import ChatPage from './Components/UserChatPage';

const ProtectedRoute = ({children}) => {
  const token = sessionStorage.getItem('jwtToken')
  return token? children : <Navigate to={'/'} replace />
};

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<HomePage/>} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/login" element={<Login />} />
          <Route path="/chat/:userId" element={ <ProtectedRoute> <ChatPage /> </ProtectedRoute>} />
          {/* // { <Route path="/users" element={<UserPage />} /> } */}
          {/* <Route path="/fetch" element={<UserPage id={userId}/>} /> */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
