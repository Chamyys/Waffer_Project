<template>
  <v-sheet
    width="300"
    class="mx-auto"
  >
    <v-form @submit.prevent>
      <v-text-field
        v-model="Id"
        :rules="rules"
        label="Введите id для удаления"
      />
      <v-btn
        type="remove"
        block
        class="mt-2"
        @click="remove(id)"
      >
        Удалить
      </v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
export default {
  // Определяем пользовательские события
  props: {
    measurersDelete: Array,
    technologistsDelete: Array
  },
  emits: ['remove'],
  setup (props, { emit }) {
    let deletedRole = '';
    let deletedIndex = '';
    let isCurrentUserExists = false;

    // eslint-disable-next-line vue/no-setup-props-destructure
    const workers = props.measurersDelete.concat(props.technologistsDelete);
    const Id = ref();
    const router = useRouter();
    // eslint-disable-next-line vue/no-setup-props-destructure
    console.log(props.measurers);
    const remove = async () => {
      const workerToDelete = findWorkerById();
      emit('remove', {
        // доделать эмит
        role: deletedRole,
        currentIndex: deletedIndex,
        isUserExists: isCurrentUserExists,
        message: 'Пользователь успешно удален'
      });
      if (workerToDelete) {
        try {
          const response = await axios.post(
            'https://localhost:3000/api/WorkerData/Delete',
            workerToDelete
          );

          deletedIndex = -1;
          isCurrentUserExists = false;
          console.log(response);

          closeComponent();
        } catch (error) {
          console.error(error);
        }
      }
    };
    const closeComponent = () => {
      router.push('/Admin/AdminChooseConfig');
    };
    const findIndex = (role, workerToDelete) => {
      let currentRoleArray;
      if (role === 'Измеритель') {
        currentRoleArray = props.measurersDelete;
      } else {
        currentRoleArray = props.technologistsDelete;
      }
      return currentRoleArray.indexOf(workerToDelete);
    };

    const findWorkerById = () => {
      const workerToDelete = workers.filter(
        (worker) => worker.id === Id.value
      )[0];
      if (workerToDelete) {
        deletedRole = workerToDelete.role;
        deletedIndex = findIndex(deletedRole, workerToDelete);
        isCurrentUserExists = true;
        workers.splice(workers.indexOf(workerToDelete), 1);
        return workerToDelete;
      } else {
        isCurrentUserExists = false;
        return false;
      }
    };
    return {
      Id,
      remove
    };
  }
};
</script>
