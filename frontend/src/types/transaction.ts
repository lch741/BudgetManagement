export type TransactionType = 0 | 1

export interface Transaction{
    Id:number
    Name:string
    Date:string
    Amount:number
    CategoryName:string
    TransactionType:TransactionType
} 

export interface CreateTransaction{
    Name:string
    Date:string
    Amount:number
    CategoryId:number
    TransactionType:TransactionType
} 