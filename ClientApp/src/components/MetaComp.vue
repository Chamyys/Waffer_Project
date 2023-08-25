<template>
  <meta
    http-equiv="Content-Security-Policy"
    :content="content"
  >
</template>

<script>
export default {
  // eslint-disable-next-line vue/multi-word-component-names, vue/no-reserved-component-names
  name: 'MetaComp',
  props: ['content']
};
</script>
