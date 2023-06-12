import { Button } from "@fluentui/react-components"
import { useNavigate } from "react-router-dom"
import {
    type DashboardItemProps,
    useDashboardItemStyles,
} from "../DashboardItem.tsx"

const GoToListControl = ({ item: { id: itemId } }: DashboardItemProps) => {
    const styles = useDashboardItemStyles()
    const navigate = useNavigate()

    const goToList = () => {
        navigate(`/list/${itemId}`)
    }

    return (
        <Button
            appearance="primary"
            className={styles.button}
            onClick={goToList}
        >
            Go to List
        </Button>
    )
}

export default GoToListControl
