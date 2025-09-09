import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from './pages/login'
import RegisterPage from './pages/register'
import { ToastContainer } from 'react-toastify'

const App: React.FC = () => {
  return (
    <BrowserRouter>
        <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                 {/* <Route path="/" element={<HomePage />} />  */}
        </Routes>
        <ToastContainer position="top-right" autoClose={3000} />
    </BrowserRouter>
  );
};

export default App;
