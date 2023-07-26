<script>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
export default {
  setup() {
    const GenerateNewWafelString = 'Создать новую пластину'
    const waferNumbers = ref(['1', '2', '3', '4', '5', '6', GenerateNewWafelString])
    const monitors = ref(['Первый монитор', 'второй монитор', 'Создать Монитор'])
    const selectedWafer = ref()
    const selectedMonitor = ref()
    const measurementRecording = ref()
    const dialog = ref(false)
    const delegateDialog = ref(false)
    const router = useRouter()
    const delegateWafferNumber = ref()
    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`
    const createNewWaferBtnAction = () =>{
        dialog.value = true
        selectedWafer.value = ""
    }
    const createNewWafer = () =>{
        dialog.value = false ; 
        router.push('/Technologist/WelcomeBack')
    }
    const delegateCreateNewWafer = () =>{
        dialog.value = false ; 
        delegateDialog.value = true;
    }
    const delegateConfirm = () =>{
        delegateDialog.value = false ; 
       
    }
      // Вызов функции при определенном значении selectedWafer
      watch(selectedWafer, (newVal, oldVal) => {
      if (newVal === 'Создать новую пластину') {
        createNewWaferBtnAction();
      }
    });

    return {
      waferNumbers,
      selectedWafer,
      selectedMonitor,
      measurementRecording,
      technologistName,
      monitors,
      dialog,
      createNewWafer,
      delegateCreateNewWafer,
      delegateConfirm,
      delegateDialog,
      delegateWafferNumber
    }
  },
}
</script>

<template>



     <v-dialog v-model="dialog" width="50em">
      <v-card>
        <v-card-text>
         Вы можете делегировать создание пластины измерителям или создать ее сами
        </v-card-text>
        <v-card-actions>
          <v-btn color="red" block @click="createNewWafer()"
            >Создать лично</v-btn
          >
        </v-card-actions>
         <v-card-actions>
          <v-btn color="green" block @click="delegateCreateNewWafer()"
            >Делегировать</v-btn
            >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="delegateDialog" width="50em">
      <v-card>
        <v-card-text>
            Введите номер пластины
        </v-card-text>
        <v-card-actions>
           
                 <v-text-field v-model="delegateWafferNumber"></v-text-field>
           
        </v-card-actions>
         <v-card-actions>
          <v-btn color="green" block @click="delegateConfirm()"
            >Подтвердить</v-btn
            >
        </v-card-actions>
      </v-card>
    </v-dialog>






  <v-sheet width="500" class="mx-auto">
    <v-form disabled>
      <v-text-field v-model="technologistName"></v-text-field>
    </v-form>
    <v-autocomplete
      v-model="selectedWafer"
      label="Номер пластины"
      :items="waferNumbers"
      
    ></v-autocomplete>
    <v-autocomplete
      v-model="selectedMonitor"
      label="Монитор для измерения"
      :items="monitors"
    ></v-autocomplete>
    <v-form>
      <v-text-field
        v-model="measurementRecording"
        label="Технологический этап"
      ></v-text-field>
    </v-form>
    <router-link :to="'/Technologist/WelcomeBack'">Назад</router-link>
  </v-sheet>
</template>
