import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import LoginPage from './pages/Auth/login'
import RegisterPage from './pages/Auth/register'
import { ToastContainer } from 'react-toastify'
import ForgotPasswordPage from './pages/Auth/forgotPassword'
import ResetPasswordPage from './pages/Auth/resetpassword'

const App: React.FC = () => {
  return (
    <BrowserRouter>
        <Routes>
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                <Route path="/reset-password" element={<ResetPasswordPage />} />
                 {/* <Route path="/" element={<HomePage />} />  */}
        </Routes>
        <ToastContainer position="top-right" autoClose={3000} />
    </BrowserRouter>
  );
};

export default App;
