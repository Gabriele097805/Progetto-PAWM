/// <reference types="vitest" />
import { defineConfig } from "vite"
import type { UserConfig as VitestUserConfigInterface } from 'vitest/config';
import react from "@vitejs/plugin-react"
import { VitePWA } from "vite-plugin-pwa"

const vitestConfig: VitestUserConfigInterface = {
    test: {
        environment: "jsdom",
        setupFiles: ["./__tests__/setup.ts"],
        testMatch: ["**/__tests__/**/*.test.ts?(x)"],
        globals: true
    }
};

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        react(),
        VitePWA({
            registerType: "autoUpdate",
            injectRegister: "auto",
            workbox: {
                globPatterns: ['**/*.{js,css,html,ico,png,svg,json,vue,txt,woff2}']
            },
            manifest: {
                name: "ToDo List",
                short_name: "ToDo",
                theme_color: "#cccccc",
                icons: [
                    {
                        src: "/assets/icon.png",
                        sizes: "192x192",
                        type: "image/png",
                    },
                    {
                        src: "/assets/icon-512.png",
                        sizes: "512x512",
                        type: "image/png",
                    },
                ],
            },
        }),
    ],
    server: {
        proxy: {
            "/api": {
                target: "http://localhost:5267",
                changeOrigin: false,
            },
        },
    },
    test: vitestConfig.test
})
