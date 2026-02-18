<script setup lang="ts">
import { ref,onMounted } from 'vue';
import { getTransactions, getTransactionById, deleteTransaction, createTransaction,updateTransaction } from '../api/transaction.api';
import type { Transaction,CreateTransaction,TransactionQuery } from "../types/transaction";
import { useToast } from 'vue-toastification';

const transactions = ref<Transaction[]>([])
const toast = useToast()
const form = ref<CreateTransaction>({
  name:'',
  date: '',
  amount: 0,
  categoryId: 1,
  transactionType: 1,
})

async function fetchTransactions() {
try{
  const res = await getTransactions();
  transactions.value = res.data;
}catch(err){
    console.error(err)
    toast.error('Failed to load transactions')
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
    await fetchTransactions()
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
     await fetchTransactions()
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

onMounted(fetchTransactions)
</script>

 
<template>
  <div class="min-h-screen bg-gray-100 p-6">
    <div class="max-w-4xl mx-auto">
      <h1 class="text-2xl font-bold mb-6 text-center">Transactions</h1>

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
            Add Transaction
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
