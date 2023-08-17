<template>
  <div>
    <!--   <input v-model="message" type="text" placeholder="Message" />
    <button @click="sendMessage">Send</button>
    <ul>
      <li v-for="message in messages" :key="message.id">{{ message.text }}</li>
    </ul>-->
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import * as signalR from '@microsoft/signalr'
export default {
  setup () {
    const message = ref('')
    const messages = ref([])
    const connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7001/chat', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build()

    connection.on('Receive', (message) => {
      messages.value.push({ id: Date.now(), text: message })
    })

    onMounted(async () => {
      try {
        await connection.start()
      } catch (err) {
        console.error(err)
      }
    })

    // Отправка сообщения
    const sendMessage = () => {
      if (message.value) {
        connection.invoke('Send', message.value)
        message.value = ''
      }
    }

    //connection.start()

    return {
      message,
      messages,
      sendMessage
    }
  }
}
</script>
