@@ -0,0 +1,33 @@
<template>
  <v-select
    id="select-id"
    v-model="selectedColor"
    label="Выводить по:"
    :items="['1', '2', '3', '4', '5']"
    @change="onChange($event)"
  >
    {{ item.Text }}
  </v-select>
</template>

<script>
export default {
  data() {
    return {
      selectedColor: 5,
    }
  },
  watch: {
    selectedColor: function (newValue) {
      this.updateColor(newValue)
    },
  },
  methods: {
    updateColor(color) {
      this.$emit('updateColor', {
        selectedColor: color,
      })
    },
  },
}
</script>
