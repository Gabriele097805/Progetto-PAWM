import { FluentProvider, webDarkTheme } from "@fluentui/react-components"
import React from "react"
import ReactDOM from "react-dom/client"
import "./App.css"
import { RecoilRoot } from "recoil"
import { type RouteObject } from "react-router"
import { createBrowserRouter, RouterProvider } from "react-router-dom"
import Dashboard from "./components/dashboard/Dashboard.tsx"
import List from "./components/list/List.tsx"
import { useCheckAuthorization } from "./services/authorization-services.ts"

const routes: RouteObject[] = [
    {
        path: "/",
        element: <Dashboard />,
    },
    {
        path: "list/:id",
        element: <List />,
    },
]

const router = createBrowserRouter(routes)

const RootComponent = () => {
    useCheckAuthorization()

    return (
        <RecoilRoot>
            <FluentProvider theme={webDarkTheme}>
                <RouterProvider router={router} />
            </FluentProvider>
        </RecoilRoot>
    )
}

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <RootComponent />
    </React.StrictMode>
)
