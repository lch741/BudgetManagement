export interface LoginRequest {
  username: string
  password: string
}

export interface RegisterRequest {
  username: string
  email: string
  password: string
}

export interface User {
  username: string
  email: string
}

export interface AuthResponse {
  username: string
  email: string
  token: string
}
