import { Button } from "@fluentui/react-components"
import { useListOperationFromParams } from "../shared.ts"

const ClearButton = () => {
    const { clearStateList } = useListOperationFromParams()
    const onClearList = () => {
        clearStateList()
    }

    return (
        <div className={"button-container"}>
            <Button onClick={onClearList}>Clear</Button>
        </div>
    )
}

export default ClearButton
