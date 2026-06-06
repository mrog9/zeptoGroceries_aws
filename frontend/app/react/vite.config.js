import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig(({command, mode, isSsrBuild, isPreview}) => {
  
  if (command === "serve") {

    return {
      plugins: [react()],
      host: true,
      port: 5173
    }

  }else {

    return {
      plugins: [react()],
      server: {
        host: "0.0.0.0"
      }
    }
  }
})
