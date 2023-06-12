import axios from "axios"
import { type ListItem, type List } from "../models/list-item.ts"
import { type ListId } from "../state/list-atom.ts"

export const getListItems = async (listId: number): Promise<List> => {
    const response = await axios.get<List | undefined>(
        `/api/ToDoListItems/${listId}`
    )
    return response.data ?? []
}

export const addListItem = async (listId: ListId, listItem: ListItem) => {
    const response = await axios.post<List>(
        `/api/ToDoListItems/${listId}`,
        listItem
    )
    return response.data
}

export const findListItem = async (id: number): Promise<ListItem> => {
    const response = await axios.get<ListItem>(
        `/api/ToDoListItems/find/${String(id)}`
    )
    return response.data
}

export const updateListItem = async (listItem: ListItem) => {
    const response = await axios.put<List>(
        `/api/ToDoListItems/${listItem.id}`,
        listItem
    )
    return response.data
}

export const deleteListItem = async (id: number) => {
    await axios.delete(`/api/ToDoListItems/${id}`)
}

export const clearList = async (listId: number) => {
    await axios.delete(`/api/ToDoListItems/${listId}/clear`)
}
