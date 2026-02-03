<script setup lang="ts">
import { ref,onMounted } from 'vue';
import { createCategory, deleteCategory, getCategories } from '../api/category.api';
import type {Category} from '../types/category'
import { useToast } from 'vue-toastification';

const categories = ref<Category[]>([])
const newName = ref("")
const toast = useToast()

async function fetchCategories() {
  const res = await getCategories();
  categories.value = res.data;
}

async function handleCreate(){
  try{
    await createCategory(newName.value)
     toast.success('Category created')
     await fetchCategories()
     newName.value = ''
  }catch(err){
    toast.error('Create failed')
    throw err
  }
}

async function handleDelete(id: number) {
  try {
    await deleteCategory(id)
    toast.success('Deleted')
    await fetchCategories()
  } catch (err) {
    toast.error('Delete failed')
    throw err
  }
}


onMounted(fetchCategories)
</script>

<template>
  <div class="min-h-screen bg-gray-100 p-6">
    <div class="max-w-3xl mx-auto">
        <h1 class="text-2xl font-bold mb-6">Categories</h1>
      <div class="bg-white rounded shadow mb-4">
        <div class="flex gap-2">
          <input 
            v-model="newName" 
            placeholder="Category name" 
            class="flex-1 border rounded px-3 py-2"
          />
          <button 
            @click="handleCreate"
            class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700" 
          >
            Add
          </button>
        </div>

        <ul>
          <li v-for="cat in categories"::key="cat.id" class="px-4 py-2 border-b">
            <span>{{ cat.name }}</span>
            <button @click="handleDelete(cat.id)" class="text-red-500 hover:underline">Delete</button>
          </li>
        </ul>

        <div v-if="categories.length===0" class="p-4 text-gray-400 text-center">
          No categories yet.
        </div>
      </div>
    </div>
  </div>
</template>

