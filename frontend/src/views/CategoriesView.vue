<script setup lang="ts">
import { ref,onMounted } from 'vue';
import { createCategory, deleteCategory, getCategories, updateCategory } from '../api/category.api';
import type {Category} from '../types/category'
import { useToast } from 'vue-toastification';

const categories = ref<Category[]>([])
const newName = ref('')
const toast = useToast()
const editingId = ref<number|null>(null)
const editingName = ref('')

async function fetchCategories() {
  try{
    const res = await getCategories();
    categories.value = res.data;
  }catch(err){
    console.error(err)
    toast.error('Failed to load categories')
    throw err
  }
}

async function handleCreate(){
  if (newName.value.trim()==='') {
    toast.warning('Please fill name')
    return
  }
  try{
     await createCategory(newName.value)
     toast.success('Category created')
     await fetchCategories()
     newName.value = ''
  }catch(err){
    console.error(err)
    toast.error('Create failed')
    throw err
  }
}

async function handleDelete(id: number) {
  if(!editingName.value.trim){
    toast.warning('Category name cannot be empty')
    return
  }
  try {
    await deleteCategory(id)
    toast.success('Deleted')
    await fetchCategories()
  } catch (err) {
    console.error(err)
    toast.error('Delete failed')
    throw err
  }
}

async function handleUpdate(id: number) {
  try {
    await updateCategory(id,editingName.value)
    toast.success('Category updated')
    await fetchCategories()
    cancelEdit()
  } catch (err) {
    console.error(err)
    toast.error('Updated failed')
    throw err
  }
}

function startEdit(cat:{id:number;name:string}){
  editingId.value = cat.id
  editingName.value = cat.name
}

function cancelEdit(){
  editingId.value = null
  editingName.value = ''
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
          <li v-for="cat in categories":key="cat.id" class="px-4 py-2 border-b flex items-center">
            <template v-if="editingId !== cat.id">
              <span class="flex-1">{{ cat.name }}</span>
              <button @click="startEdit(cat)" class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-700 ml-auto">Edit</button>
              <button @click="handleDelete(cat.id)" class="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-700 ml-auto">Delete</button>
            </template>
            <template v-else>
              <input 
                v-model="editingName" 
                class="flex-1 border rounded px-3 py-2"
              />
              <button @click="handleUpdate(cat.id)" class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-700 ml-auto">Save</button>
              <button @click="cancelEdit" class="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-700 ml-auto">Cancel</button>
            </template>
          </li>
        </ul>

        <div v-if="categories.length===0" class="p-4 text-gray-400 text-center">
          No categories yet.
        </div>
      </div>
    </div>
  </div>
</template>

