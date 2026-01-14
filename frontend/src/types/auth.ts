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
  Username: string
  Email: string
  Token: string
}