import { fileURLToPath, URL } from 'url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vuetify from 'vite-plugin-vuetify'
import { readFileSync } from 'fs'
import { join } from 'path'

// https://github.com/johanndev/asp_vite
const baseFolder =
  process.env.APPDATA !== undefined && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`

const certificateName = 'domain'

const keyFilePath = join(baseFolder, `${certificateName}.key`)
const certFilePath = join(baseFolder, `${certificateName}.pem`)

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue({
      template: {
        compilerOptions: {
          isCustomElement: (tag) => ['md-linedivider'].includes(tag),
        },
      },
    }),
    vuetify(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7001',
        secure: false,
      },
    },

    https: {
      //сертификаты
      key: readFileSync(keyFilePath),
      cert: readFileSync(certFilePath),
    },
    watch: {
      usePolling: true,
    },
  },
})
