<script setup lang="ts">
import { ref,onMounted } from 'vue';
import { getTransactions, getTransactionById, deleteTransaction, createTransaction,updateTransaction } from '../api/transaction.api';
import type { Transaction,CreateTransaction,TransactionQuery } from "../types/transaction";
import { useToast } from 'vue-toastification';
import type { Category } from '../types/category';
import { getCategories } from '../api/category.api';
import { useRouter } from 'vue-router'

const router = useRouter()
const transactions = ref<Transaction[]>([])
const categories = ref<Category[]>([])
const toast = useToast()
const form = ref<CreateTransaction>({
  name:'',
  date: '',
  amount: 0,
  categoryId: 1,
  transactionType: 1,
})
const query = ref<TransactionQuery>({
  name: '',
  transactionType: undefined,
  categoryId: undefined,
  startDate: '',
  endDate: '',
  minAmount: undefined,
  maxAmount: undefined,
  isDescendingByDate: undefined,
  isDescendingByAmount: undefined
})

function goToCategories(){
  router.push('/categories')
}

function resetQuery(){
  query.value = {
    name: '',
    transactionType: undefined,
    categoryId: undefined,
    startDate: '',
    endDate: '',
    minAmount: undefined,
    maxAmount: undefined,
    isDescendingByDate: undefined,
    isDescendingByAmount: undefined
  }
  fetchTransactionsAndCategories()
}

function cleanQuery(query: TransactionQuery) {
  const result: any = {}

  Object.entries(query).forEach(([key, value]) => {
    if (value !== undefined && value !== null && value !== '') {
      result[key] = value
    }
  })
  return result
}

async function fetchTransactionsAndCategories() {
try{
  const res = await getTransactions(cleanQuery(query.value));
  transactions.value = res.data;
  const catRes = await getCategories();
  categories.value = catRes.data;
}catch(err){
    console.error(err)
    toast.error('Failed to load transactions or categories')
    throw err
  }
}

async function handleDelete(id: number) {
  if(!confirm('Delete this transaction?')){
    toast.warning('Delete canceled')
    return
  }
  try {
    await deleteTransaction(id)
    toast.success('Deleted')
    await fetchTransactionsAndCategories()
  } catch (err) {
    console.error(err)
    toast.error('Delete failed')
    throw err
  }
}

async function handleCreate(){
  if (!form.value.name||!form.value.date||!form.value.amount) {
    toast.warning('Please fill all fields')
    return
  }
  try{
     await createTransaction(form.value)
     toast.success('Transaction created')
     await fetchTransactionsAndCategories()
     form.value.name = ''
     form.value.date = ''
     form.value.amount = 0
     form.value.categoryId = 1
     form.value.transactionType = 1
  }catch(err){
    console.error(err)
    toast.error('Create failed')
    throw err
  }
}

onMounted(fetchTransactionsAndCategories)
</script>

 
<template>
  <div class="min-h-screen bg-gray-100 p-6">
    <div class="max-w-4xl mx-auto">
        <div class="mb-6 flex items-center justify-between">
          <h1 class="text-2xl font-bold">Transactions</h1>
            <button
              @click="goToCategories"
              class="rounded bg-emerald-600 px-3 py-2 text-white hover:bg-emerald-700"
            >
              Manage Categories
            </button>
        </div>

        <div class="bg-white p-4 rounded shadow mb-6 grid grid-cols-4 gap-3">

          <input v-model="query.name" placeholder="Search name"
            class="border rounded px-2 py-1" />

          <select v-model="query.transactionType" class="border rounded px-2 py-1">
            <option :value="null">All</option>
            <option :value="1">Income</option>
            <option :value="2">Expense</option>
          </select>

          <select v-model="query.categoryId" class="border rounded px-2 py-1">
            <option :value="null">All Categories</option>
            <option v-for="c in categories" :key="c.id" :value="c.id">
              {{ c.name }}
            </option>
          </select>

          <input type="date" v-model="query.startDate" class="border rounded px-2 py-1" />
          <input type="date" v-model="query.endDate" class="border rounded px-2 py-1" />

          <input type="number" v-model.number="query.minAmount" placeholder="Min"
            class="border rounded px-2 py-1" />

          <input type="number" v-model.number="query.maxAmount" placeholder="Max"
            class="border rounded px-2 py-1" />

          <select v-model="query.isDescendingByDate" class="border rounded px-2 py-1">
            <option :value="null">Date Default</option>
            <option :value="true">Newest</option>
            <option :value="false">Oldest</option>
          </select>

          <button @click="fetchTransactionsAndCategories"
            class="bg-blue-600 text-white rounded px-3 py-1">
            Apply
          </button>

          <button @click="resetQuery"
            class="bg-gray-300 rounded px-3 py-1">
            Reset
          </button>

        </div>

        <div class="bg-white p-4 rounded shadow mb-6 flex flex-row items-stretch gap-3">
          <input
            v-model="form.name"
            placeholder="name"
            type="text"
            class="h-10 w-full rounded border border-gray-300 px-3 focus:outline-none focus:ring-2 focus:ring-blue-600"
          />
          <input
            v-model="form.date"
            type="date"
            class="h-10 w-full rounded border border-gray-300 px-3 focus:outline-none focus:ring-2 focus:ring-blue-600"
          />
          <input
            v-model.number="form.amount"
            type="number"
            placeholder="Amount"
            class="h-10 w-full rounded border border-gray-300 px-3 focus:outline-none focus:ring-2 focus:ring-blue-600"
          />

          <select
            v-model="form.categoryId"
            class="h-10 w-full rounded border border-gray-300 bg-white px-3 focus:outline-none focus:ring-2 focus:ring-blue-600"
          >
            <option disabled value="">Select Category</option>
            <option v-for="cat in categories" :key="cat.id" :value="cat.id">
              {{ cat.name }}
            </option>
          </select>

          <select
            v-model="form.transactionType"
            class="h-10 w-full rounded border border-gray-300 bg-white px-3 focus:outline-none focus:ring-2 focus:ring-blue-600"
          >
            <option :value="1">Income</option>
            <option :value="2">Expense</option>
          </select>

          <button
            @click="handleCreate"
            class="h-10 w-full rounded bg-blue-600 px-4 text-white hover:bg-blue-700"
          >
            Add
          </button>
        </div>

        <div class="bg-white rounded shadow">
          <ul>
            <li
              v-for="t in transactions"
              :key="t.id"
              class="px-4 py-2 border-b flex items-center"
            >
              <div>
                <div class="font-medium">{{ t.name }}</div>
                <div class="text-sm text-gray-500">
                  {{ t.date }} · {{ t.categoryName }} ·
                  <span :class="t.transactionType === 1 ? 'text-green-600' : 'text-red-600'">
                    {{ t.transactionType === 1 ? '+' : '-' }}{{ t.amount }}
                  </span>
                </div>
              </div>

              <button
                class="ml-auto text-red-600 hover:text-red-800 text-sm"
                @click="handleDelete(t.id)"
              >
                Delete
              </button>
            </li>
          </ul>

          <div v-if="transactions.length === 0" class="p-4 text-center text-gray-400">
            No transactions yet.
          </div>
        </div>
    </div>
  </div>
</template>
