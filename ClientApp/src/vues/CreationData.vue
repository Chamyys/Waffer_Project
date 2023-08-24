@@ -0,0 +1,38 @@
<template>
  <div>
    <div
      style="
        width: 50%;
        height: 90px;
        float: left;
        padding-left: 15em;
        padding-right: 1em;
        text-align: right;
      "
    >
      <h2>{{ text }}</h2>
    </div>
    <div
      style="
        width: 50%;
        height: 90px;
        float: right;
        text-align: left;
        padding-left: 1em;
        padding-right: 15em;
      "
    >
      <h2>{{ date }}</h2>
    </div>
  </div>
</template>
<script>
export default {
  setup () {
    return {
      date: new Date().toLocaleString('ru'),
      text: 'Компонент Создан: '
    };
  }
};
</script>
