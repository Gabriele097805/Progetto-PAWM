import axios from "axios"

export type User = {
    id: number
    name: string
    url: string
    avatarUrl: string
}

export const getUser = async (): Promise<User | undefined> => {
    const response = await axios.get<User>("/api/user")
    return response.data
}
