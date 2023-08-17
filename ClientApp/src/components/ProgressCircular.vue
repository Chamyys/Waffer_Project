<template>
  <v-progress-circular
    :size="150"
    :width="10"
    :rotate="0"
    :value="progress"
    :color="getColor()"
  >
    <span class="progress-text">{{ seconds }}</span>
  </v-progress-circular>
</template>

<script>
import { ref, onMounted } from 'vue'

export default {
  name: 'ProgressCircular',
  setup () {
    const progress = ref(0)
    const seconds = ref(0)

    const getColor = () => {
      return progress.value === 100 ? 'green' : 'blue'
    }

    const startTimer = () => {
      let count = 0
      const interval = setInterval(() => {
        count += 1000
        progress.value = (count / 5000) * 100
        seconds.value = Math.floor(count / 1000)
        if (count === 5000) {
          clearInterval(interval)
        }
      }, 1000)
    }

    onMounted(() => {
      startTimer()
    })

    return {
      progress,
      seconds,
      getColor
    }
  }
}
</script>

<style scoped>
.progress-text {
  font-size: 24px;
}
</style>
