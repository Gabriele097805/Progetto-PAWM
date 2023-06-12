import axios, { type AxiosError, type AxiosResponse } from "axios"
import { useEffect } from "react"

const checkAuthorization = async (): Promise<AxiosResponse> =>
    axios.get("/api/auth/check")

/**
 * Checks if the user is authorized and redirects to the proxy if not.
 */
export const useCheckAuthorization = () => {
    useEffect(() => {
        checkAuthorization()
            .then((_) => {})
            .catch((error: AxiosError) => {
                if (error.response?.status === 401) {
                    window.location.replace("/api/auth/proxy")
                    return
                }
            })
    }, [])
}
