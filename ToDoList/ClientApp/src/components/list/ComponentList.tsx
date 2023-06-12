import { useMemo } from "react"
import ListElement from "./ListElement.tsx"
import { type ListItem } from "../../models/list-item.ts"
import { useListsStateFromParams } from "./shared.ts"

const ComponentList = () => {
    const { list: listInfo } = useListsStateFromParams()

    const componentsList = useMemo(() => {
        return (
            listInfo?.items?.map((i: ListItem) => (
                <ListElement key={i.id} item={i} />
            )) ?? []
        )
    }, [listInfo])

    return <div className="flex">{componentsList}</div>
}

export default ComponentList
