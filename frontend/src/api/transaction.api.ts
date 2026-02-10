import api from './axios'
import type { Transaction,CreateTransaction,TransactionQuery } from "../types/transaction";



export function getTransactions(query?:TransactionQuery){
    return api.get<Transaction[]>('/Transaction',{params:query})
}

export function getTransactionById(id:number){
    return api.get<Transaction>(`/Transaction/${id}`)
}

export function deleteTransaction(id:number){
    return api.delete(`/Transaction/${id}`)
}

export function createTransaction(Transaction:CreateTransaction){
    return api.post('/Transaction',{Transaction})
}

export function updateTransaction(id:number,Transaction:CreateTransaction){
    return api.put(`/Transaction/${id}`,{Transaction})
}