<template>
  <div class="text-center">
    <v-progress-circular
      :rotate="360"
      :size="90"
      :width="13"
      :model-value="value"
      color="indigo"
    >
      {{ value / 20 }}
    </v-progress-circular>
  </div>
</template>
<script>
export default {
  data () {
    return {
      interval: {},
      value: 100
    }
  },
  beforeUnmount () {
    clearInterval(this.interval)
  },
  mounted () {
    this.interval = setInterval(() => {
      if (this.value === 0) {
        return (this.value = 100)
      }
      this.value -= 20
    }, 1000)
  }
}
</script>

<style scoped>
.v-progress-circular {
  margin: 1rem;
}
</style>
