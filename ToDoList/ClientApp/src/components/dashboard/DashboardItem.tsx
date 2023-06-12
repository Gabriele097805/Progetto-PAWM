import { makeStyles, shorthands, tokens } from "@fluentui/react-components"
import { type ListInfo } from "../../models/list-item.ts"
import ChangeListInput from "./controls/ChangeListInput.tsx"
import DeleteListControl from "./controls/DeleteListControl.tsx"
import GoToListControl from "./controls/GoToListControl.tsx"

export const useDashboardItemStyles = makeStyles({
    container: {
        boxShadow: tokens.shadow2,
        textAlign: "center",
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-between",
        alignItems: "baseline",
        ...shorthands.padding("0.5rem"),
    },

    input: {
        minWidth: "50%",
        textAlign: "center",
    },

    buttonContainer: {
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-evenly",
    },

    button: {
        marginRight: "0.5rem",
        marginLeft: "0.5rem",
    },
})

export type DashboardItemProps = {
    readonly item: ListInfo
}

const DashboardItem = ({ item }: DashboardItemProps) => {
    const styles = useDashboardItemStyles()

    return (
        <div className={styles.container}>
            <ChangeListInput item={item} />
            <div className={styles.buttonContainer}>
                <GoToListControl item={item} />
                <DeleteListControl item={item} />
            </div>
        </div>
    )
}

export default DashboardItem
