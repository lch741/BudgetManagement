<script lang="ts">
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
     toast.success('Category created')
     await fetchTransactions()
     form.value.name = ''
     form.value.date = ''
     form.value.amount = 0
     form.value.categoryId = 1
     form.value.transactionType = 1
  }catch(err){
    toast.error('Create failed')
    throw err
  }
}

onMounted(fetchTransactions)
</script>

 
<template>
    <div>

    </div>
</template>