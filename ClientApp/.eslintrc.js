module.exports = {
  env: {
    node: true,
    'vue/setup-compiler-macros': true,
  },
  extends: ['eslint:recommended', 'plugin:vue/vue3-recommended', 'prettier'],
  rules: {
    // override/add rules settings here, such as:
    'vue/script-setup-uses-vars': 'error',
    'vue/require-default-prop': 'off',
    'vue/no-unused-vars': 'warn',
    // disabled the no-undef because kept leading to error in eslint
    'vue/no-undef': 'off',
    'no-undef': 'warn',
    'no-unused-vars': 'warn',
    'vue/multi-word-component-names': 'warn',
    'vue/no-deprecated-v-bind-sync': 'warn',
    'vue/no-deprecated-v-is': 'warn',
    'vue/no-deprecated-v-on-native-modifier': 'warn',
    'vue/no-deprecated-v-on-number-modifiers': 'warn',
    'vue/no-deprecated-vue-config-keycodes': 'warn',
    'vue/no-dupe-keys': 'warn',
    'vue/no-dupe-v-else-if': 'warn',
    'vue/no-duplicate-attributes': 'warn',
    'vue/no-export-in-script-setup': 'warn',
    'vue/no-expose-after-await': 'warn',
    'vue/order-in-components': 'warn',
  },
}
