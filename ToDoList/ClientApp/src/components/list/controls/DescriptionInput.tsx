import { debounce } from "lodash"
import React from "react"
import { Input } from "@fluentui/react-components"
import { ListItem } from "../../../models/list-item.ts"
import { useListOperationFromParams } from "../shared.ts"

type SomethingToDoProps = {
    readonly item: ListItem
}

const DescriptionInput = ({ item }: SomethingToDoProps) => {
    const { updateStateListItem } = useListOperationFromParams()

    const activityRecord = (
        e: Readonly<React.KeyboardEvent<HTMLInputElement>>
    ) => {
        const newItem = {
            ...item,
            text: (e.target as HTMLInputElement).value,
        }

        updateStateListItem(newItem)
    }

    const debouncedActivityRecord = debounce(activityRecord, 500)

    return <Input defaultValue={item.text} onKeyUp={debouncedActivityRecord} />
}

export default DescriptionInput
