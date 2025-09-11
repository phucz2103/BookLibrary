import { Navigate } from 'react-router-dom';
import { TokenUtils } from '../../utils/TokenUtils';

interface ProtectedRouteProps {
    children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
    const isAuthenticated = TokenUtils.isAuthenticated();

    if (!isAuthenticated) {
        // Redirect to login if not authenticated
        return <Navigate to="/login" replace />;
    }
    // Render children if authenticated
    return <>{children}</>;
};

export default ProtectedRoute;