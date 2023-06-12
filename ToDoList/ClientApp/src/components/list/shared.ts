import { useParams } from "react-router-dom"
import {
    type ListId,
    useListsState,
    useListStateOperations,
} from "../../state/list-atom.ts"

/**
 * Tries to parse the list id from the url params.
 */
export const useListIdParams = (): ListId => {
    const { id } = useParams()

    if (!id) {
        // eslint-disable-next-line functional/no-throw-statements
        throw new Error("No list id in the url params")
    }

    return parseInt(id)
}

export const useListOperationFromParams = () => {
    const listId = useListIdParams()
    return useListStateOperations(listId)
}

export const useListsStateFromParams = () => {
    const id = useListIdParams()
    return useListsState(id)
}
