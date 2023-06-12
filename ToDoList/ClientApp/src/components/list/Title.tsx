import { makeStyles } from "@fluentui/react-components"
import GoToDashboard from "./controls/GoToDashboard.tsx"
import { useListsStateFromParams } from "./shared.ts"

const useStyles = makeStyles({
    container: {
        textAlign: "center",
        marginBottom: "1rem",
    },
})

const Title = () => {
    const styles = useStyles()
    const { list } = useListsStateFromParams()

    const title = list?.name ?? "To-do List"

    return (
        <div className={styles.container}>
            <h1>{title}</h1>
            <GoToDashboard />
        </div>
    )
}

export default Title
