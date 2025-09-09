import type { AxiosInstance, AxiosError } from "axios";
import axios from "axios";
import { authService } from "./authService";

const API_BASE_URL = "https://localhost:5001/auth/";

const api: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(
    (config) => {
    const accessToken = authService.getAccessToken();
    if(accessToken){
        config.headers.Authorization = `Bear ${accessToken}`
    }
    return config;
    },
    (error) => Promise.reject(error),
);

// Interceptor cho response: Xử lý lỗi 401 và làm mới token
api.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && originalRequest && !originalRequest._retry) {
      originalRequest._retry = true; // Đánh dấu để tránh loop vô hạn
      try {
        const newTokens = await authService.refreshToken();
        originalRequest.headers.Authorization = `Bearer ${newTokens.accessToken}`;
        return api(originalRequest); // Thử lại request với token mới
      } catch (refreshError) {
        authService.clearTokens();
        window.location.href = '/login'; // Chuyển hướng đến login
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  },
);