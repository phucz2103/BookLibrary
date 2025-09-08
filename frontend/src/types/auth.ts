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
    token : string;
    refreshToke : string;
    expToken : number;
    userInfor : UserInfor;
}

export interface ResgisterCre{
    Fullname : string
    Email : string
    UserName : string
    Address : string
    PhoneNumber : string
    DOB : Date
}

export interface RefreshTokenRequest {
  refreshToken: string;
}

export interface AuthError {
  message: string;
}

