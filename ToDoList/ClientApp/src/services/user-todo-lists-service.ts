import axios from "axios"
import { ListInfo, UserLists } from "../models/list-item.ts"

export const getLists = async (): Promise<UserLists> => {
    const response = await axios.get<UserLists | undefined>(
        "/api/ToDoList/lists"
    )
    return response.data ?? []
}

type AddListRequest = {
    readonly name: string
}

export const addList = async (list: AddListRequest) => {
    const response = await axios.post<UserLists>("/api/ToDoList/add-list", list)
    return response.data
}

export const findList = async (id: number) => {
    const response = await axios.get<ListInfo>(
        `/api/ToDoList/find-list?id=${String(id)}`
    )
    return response.data
}

export const updateList = async (list: ListInfo, id: number) => {
    const response = await axios.put<UserLists>(
        `/api/ToDoList/update-list?id=${id}`,
        list
    )
    return response.data
}

export const deleteList = async (id: number) => {
    const response = await axios.delete<UserLists>(
        `/api/ToDoList/delete-list?id=${id}`
    )
    return response.data
}
