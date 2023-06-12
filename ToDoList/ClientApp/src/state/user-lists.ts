import { atom, useSetRecoilState } from "recoil"
import {
    getLocalStorageItem,
    setLocalStorageItem,
} from "../local-storage-helpers"
import { debounce } from "lodash"
import {
    deleteList,
    getLists,
    updateList,
} from "../services/user-todo-lists-service.ts"
import { ListInfo, UserLists } from "../models/list-item.ts"
import { useTooltipMessage } from "./app-message-atom.ts"
import { type ListId } from "./list-atom.ts"

const debounceTime = 1000

export const userLists = atom<UserLists>({
    key: "user-lists",
    default: [],
    effects: [
        ({ setSelf }) => {
            getLists()
                .then((lists) => {
                    setLocalStorageItem("user-lists", lists)
                    setSelf(lists)
                })
                .catch((error) => {
                    console.log(error)

                    setSelf(
                        getLocalStorageItem<UserLists>("user-lists", []) ?? []
                    )
                })
        },
        ({ onSet }) => {
            const setting = (newValue: UserLists) => {
                setLocalStorageItem("list", newValue)
            }

            const debouncedSetting = debounce(setting, debounceTime)

            onSet(debouncedSetting)
        },
    ],
})

export const useUpdateList = () => {
    const { setTooltipMessage } = useTooltipMessage()
    const setLists = useSetRecoilState(userLists)

    const deleteListById = (listId: ListId) => {
        deleteList(listId)
            .then((result) => {
                setLists(result)
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage(
                    `Error deleting list of id '${listId}'`,
                    "error"
                )
            })
    }

    const updateListItem = (list: ListInfo) => {
        updateList(list, list.id)
            .then((result) => {
                setLists(result)
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage(
                    `Error deleting list of id '${list.id}'`,
                    "error"
                )
            })
    }

    return {
        deleteListById,
        updateListItem,
    }
}
