import type {   AxiosError } from "axios";
import axios from "axios";
import type { AuthError, LoginApiResponse, LoginCre, RefreshTokenRequest, ResgisterCre } from "../types/auth";

const API_BASE_URL = "https://localhost:5001/auth/";

export const authService = {
    async login(credentials : LoginCre) : Promise<LoginApiResponse>{
        try{
            const response = await axios.post<LoginApiResponse>(`${API_BASE_URL}/login`,credentials)
            return response.data;
        }catch(error){
            const axiosError = error as AxiosError<AuthError>
            console.error('Lỗi khi đăng nhập:', axiosError);
      throw new Error(axiosError.response?.data.message || 'Đăng nhập thất bại');
        }
    },

    async register(resgisterData : ResgisterCre) : Promise<void> {
        try{
            const response = await axios.post<void>(`${API_BASE_URL}/register`,resgisterData)
            return response.data;
        }catch(error){
            const axiosError = error as AxiosError<AuthError>
            console.error('Lỗi khi đăng ký:', axiosError);
      throw new Error(axiosError.response?.data.message || 'Đăng ký thất bại');
        }
    },

    async refreshToken() : Promise<LoginApiResponse>{
        try{
            const accessToken = authService.getAccessToken();
            const refreshToken = authService.getRefreshToken();
            if (!accessToken || !refreshToken) {
            throw new Error('Thiếu accessToken hoặc refreshToken');
             }
            const response = await axios.post<LoginApiResponse>(`${API_BASE_URL}/LoginApiResponse`,{accessToken,refreshToken} as RefreshTokenRequest,)
            return response.data;
        }catch(error){
            const axiosError = error as AxiosError<AuthError>
            console.error("lỗi token:" ,axiosError);
            throw new Error (axiosError.response?.data.message || 'Lỗi token');
        }
    },

    saveToken({accessToken, refreshToken} : LoginApiResponse) : void {
        localStorage.setItem('accessToken',accessToken);
        localStorage.setItem('refreshToken',refreshToken);
    },

    getAccessToken() : string | null {
       return localStorage.getItem('accessToken');
    },

    getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
    },

  clearTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  },
}