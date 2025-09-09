export interface LoginCre {
    username : string;
    password : string;
}

export type UserRole = 'User' | 'Administrator' | 'Manager'
export interface UserInfor{
    id : string;
    email : string;
    role : UserRole;
    fullname : string;
    phone : string;
    isActive : boolean;
}

export interface LoginApiResponse{
    accessToken : string;
    refreshToken : string;
    role : string;
    userId : string;
    success : boolean;
}

export interface ResgisterCre{
    Fullname : string
    Email : string
    UserName : string
    Address : string
    PhoneNumber : string
    DOB : Date
    Password : string
    ConfirmPassword : string
}

export interface RefreshTokenRequest {
  accessToken: string;
  refreshToken: string;
}

export interface AuthError {
  message: string;
}

