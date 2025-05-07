<template>
  <div class="p-4">
    <!-- Кнопка добавления задачи -->
    <DxButton text="Добавить задачу" type="success" styling-mode="contained" @click="openAddForm" class="mb-4" />

    <!-- Модальное окно создания задачи -->
    <DxPopup :visible.sync="popupVisible" title="Новая задача" :show-close-button="true" :width="500">
      <div class="p-4">
        <DxForm :form-data="newTask">
          <DxSimpleItem data-field="title" label-text="Заголовок" />
          <DxSimpleItem data-field="description" label-text="Описание" />
          <DxSimpleItem data-field="createdById" editor-type="dxNumberBox" label-text="ID Создателя" />
          <DxSimpleItem data-field="assignedToId" editor-type="dxNumberBox" label-text="ID Исполнителя" />
        </DxForm>
        <DxButton text="Создать" type="default" @click="createTask" class="mt-2" />
      </div>
    </DxPopup>

    <!-- Контейнер для статусов -->
    <div class="task-container">
      <div class="status-column" v-for="status in statuses" :key="status">
        <h3>{{ getStatusLabel(status) }}</h3>
        <DxDataGrid
          :data-source="getTasksByStatus(status)"
          :key-expr="'id'"
          :show-borders="true"
          :column-auto-width="true"
          :allow-column-resizing="true"
          :search-panel="{ visible: true, width: 240, placeholder: 'Поиск...' }"
          :filter-row="{ visible: true }"
          :header-filter="{ visible: true }"
          height="auto"
          :row-dragging="{
            allowReordering: false,
            group: 'tasks',
            onDragStart: onRowDragging,
            onDrop: (e) => onRowDrop(e, status)
          }"
        >
          <DxColumn dataField="title" caption="Название" />
          <DxColumn dataField="description" caption="Описание" />
          <DxColumn dataField="createdBy" caption="Создатель" />
          <DxColumn dataField="assignedTo" caption="Назначено" />
        </DxDataGrid>
      </div>
    </div>

    <!-- Зона удаления -->
    <div class="delete-zone mt-8">
      🗑️ Перетащите задачу сюда для удаления
      <DxDataGrid
        class="invisible-grid"
        :data-source="[]"
        height="60"
        :row-dragging="{
          group: 'tasks',
          onDrop: onDeleteDrop
        }"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { DxDataGrid, DxColumn } from 'devextreme-vue/data-grid';
import { DxButton } from 'devextreme-vue/button';
import { DxPopup } from 'devextreme-vue/popup';
import { DxForm, DxSimpleItem } from 'devextreme-vue/form';

const tasks = ref<any[]>([]);
const popupVisible = ref(false);
const newTask = ref({
  title: '',
  description: '',
  createdById: null,
  assignedToId: null,
});
const statuses = ref([0, 1, 2]);
let draggedTask = null;

const loadTasks = async () => {
  try {
    const response = await axios.get('https://localhost:7253/api/Task');
    tasks.value = response.data;
  } catch (err: any) {
    console.error('Ошибка при загрузке задач:', err);
    alert(err.response?.data || 'Не удалось загрузить задачи.');
  }
};

const openAddForm = () => {
  popupVisible.value = true;
};

const createTask = async () => {
  try {
    await axios.post('https://localhost:7253/api/Task', newTask.value);
    popupVisible.value = false;
    await loadTasks();
  } catch (err: any) {
    console.error('Ошибка при создании задачи:', err);
    alert(err.response?.data || 'Не удалось создать задачу.');
  }
};

const getTasksByStatus = (status: number) => {
  return tasks.value.filter(task => task.status === status);
};

const updateTaskStatus = async (taskId: number, newStatus: number) => {
  try {
    const taskToUpdate = tasks.value.find(task => task.id === taskId);
    if (taskToUpdate) {
      taskToUpdate.status = newStatus;
      await axios.put(`https://localhost:7253/api/Task/status/${taskId}`, taskToUpdate);
    }
  } catch (err: any) {
    console.error('Ошибка при обновлении статуса задачи:', err);
    alert(err.response?.data || 'Не удалось обновить статус задачи.');
  }
};

const deleteTask = async (id: number) => {
  try {
    await axios.delete(`https://localhost:7253/api/Task?id=${id}`);
    await loadTasks();
  } catch (err: any) {
    console.error('Ошибка при удалении задачи:', err);
    alert(err.response?.data || 'Не удалось удалить задачу.');
  }
};

const onDeleteDrop = async (e: any) => {
  const task = e.itemData; // <-- безопаснее, чем e.fromData[e.fromIndex]
  if (task) {
    const confirmed = confirm(`Удалить задачу "${task.title}"?`);
    if (confirmed) {
      await deleteTask(task.id);
    }
  }
};

const getStatusLabel = (status: number) => {
  switch (status) {
    case 0: return 'Новый';
    case 1: return 'В процессе';
    case 2: return 'Завершено';
    default: return 'Неизвестно';
  }
};

const onRowDragging = (e: any) => {
  draggedTask = e.itemData;
};

const onRowDrop = async (e: any, targetStatus: number) => {
  if (draggedTask && draggedTask.status !== targetStatus) {
    await updateTaskStatus(draggedTask.id, targetStatus);
    await loadTasks();
  }
  draggedTask = null;
};

onMounted(loadTasks);
</script>

<style scoped>
.task-container {
  display: flex;
  justify-content: space-between;
  gap: 16px;
}

.status-column {
  width: 32%;
}

.delete-zone {
  background-color: #ffe5e5;
  border: 2px dashed red;
  padding: 16px;
  text-align: center;
  font-weight: bold;
  border-radius: 8px;
  margin-top: 40px;
  margin-left: auto;
  margin-right: auto;
  width: 40%;
  height: 80px;
  justify-content: center;
  align-items: center;
  transition: background-color 0.3
}

.invisible-grid {
  opacity: 0;
  height: 50px;
  pointer-events: none;
}

</style>
