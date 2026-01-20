export interface LoginRequest {
  Username: string
  Password: string
}


export interface RegisterRequest {
  Username: string
  Email: string
  Password: string
}

export interface User{
    Username: string
    Email: string
}

export interface AuthResponse {
  username: string
  email: string
  token: string
}