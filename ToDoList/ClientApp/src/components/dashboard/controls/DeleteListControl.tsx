import { Button } from "@fluentui/react-components"
import { useUpdateList } from "../../../state/user-lists.ts"
import {
    type DashboardItemProps,
    useDashboardItemStyles,
} from "../DashboardItem.tsx"

const DeleteListControl = ({ item }: DashboardItemProps) => {
    const styles = useDashboardItemStyles()
    const { deleteListById } = useUpdateList()

    const deleteList = () => {
        deleteListById(item.id)
    }

    return (
        <Button
            appearance="secondary"
            onClick={deleteList}
            className={styles.button}
        >
            Delete List
        </Button>
    )
}

export default DeleteListControl
