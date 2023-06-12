import { useEffect, useState } from "react"
import { atom, useRecoilState } from "recoil"
import { ListInfo, ListItem } from "../models/list-item.ts"
import {
    addListItem,
    clearList,
    getListItems,
    updateListItem,
} from "../services/todo-list-service.ts"
import { findList } from "../services/user-todo-lists-service.ts"
import { useTooltipMessage } from "./app-message-atom.ts"

export type ListId = number

const listsAtom = atom<Map<ListId, ListInfo>>({
    key: "listAtomFamily",
    default: new Map(),
})

export const useListsState = (listId: ListId) => {
    const { setTooltipMessage } = useTooltipMessage()
    const [map, setMap] = useRecoilState(listsAtom)
    const [list, setList] = useState<ListInfo>()

    useEffect(() => {
        if (map.has(listId)) {
            setList(map.get(listId))
            return
        }

        getListItems(listId)
            .then((list) =>
                findList(listId).then(
                    (listInfo) =>
                        ({
                            ...listInfo,
                            items: list,
                        } as ListInfo)
                )
            )
            .then((list) => {
                const newMap = new Map([...map, [listId, list]])
                setMap(newMap)
            })
            .catch((error) => {
                console.log(error)
                setTooltipMessage(
                    `There was an error while getting the list with id: '${listId}'.`,
                    "error"
                )
            })
    }, [map])

    const setListState = (listId: ListId, list: ListInfo) => {
        const newMap = new Map([...map, [listId, list]])
        setMap(newMap)
    }

    return {
        list,
        setList: setListState,
    }
}

export const useListStateOperations = (listId: ListId) => {
    const { setTooltipMessage } = useTooltipMessage()
    const { list: listInfo, setList: setListInfo } = useListsState(listId)

    const addStateListItem = (newItem: ListItem) => {
        addListItem(listId, newItem)
            .then((newList) => {
                if (listInfo) {
                    setListInfo(listId, {
                        ...listInfo,
                        items: newList,
                    })
                }
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage(
                    "There was an error while trying to add a new item.",
                    "error"
                )
            })
    }

    const clearStateList = () => {
        clearList(listId)
            .then(() => {
                if (listInfo) {
                    setListInfo(listId, {
                        ...listInfo,
                        items: [],
                    })
                }
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage(
                    "There was an error while trying to clear the list.",
                    "error"
                )
            })
    }

    const updateStateListItem = (item: ListItem) => {
        updateListItem(item)
            .then(() => {
                if (listInfo) {
                    const newItems =
                        listInfo.items?.map((i) =>
                            i.id === item.id ? item : i
                        ) ?? []
                    setListInfo(listId, {
                        ...listInfo,
                        items: newItems,
                    })
                }
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage(
                    "There was an error while trying to update the item.",
                    "error"
                )
            })
    }

    return {
        addStateListItem,
        clearStateList,
        updateStateListItem,
    }
}
