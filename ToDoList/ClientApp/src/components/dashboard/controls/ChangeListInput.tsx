import { Input } from "@fluentui/react-components"
import { debounce } from "lodash"
import { KeyboardEvent } from "react"
import { useUpdateList } from "../../../state/user-lists.ts"
import {
    type DashboardItemProps,
    useDashboardItemStyles,
} from "../DashboardItem.tsx"

const ChangeListInput = ({ item }: DashboardItemProps) => {
    const styles = useDashboardItemStyles()
    const { updateListItem } = useUpdateList()

    const updateListName = ({
        target,
    }: Readonly<KeyboardEvent<HTMLInputElement>>) => {
        const newItem = {
            ...item,
            name: (target as HTMLInputElement).value,
        }

        updateListItem(newItem)
    }

    const debouncedUpdateListName = debounce(updateListName, 500)

    return (
        <Input
            defaultValue={item.name}
            onKeyUp={debouncedUpdateListName}
            className={styles.input}
        />
    )
}

export default ChangeListInput
