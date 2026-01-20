export type TransactionType = 1 | 2

export interface Transaction{
  id: number
  name: string
  date: string
  amount: number
  categoryName: string
  transactionType: TransactionType
} 

export interface CreateTransaction{
  name: string
  date: string
  amount: number
  categoryId: number
  transactionType: TransactionType
} 

export interface TransactionQuery
{
  name?: string
  transactionType?: TransactionType
  categoryId?: number
  startDate?: string
  endDate?: string
  minAmount?: number
  maxAmount?: number
  isDescendingByDate?: boolean
  isDescendingByAmount?: boolean
}