import { getEmptyItem } from "../../../models/list-item.ts"
import { Button } from "@fluentui/react-components"
import {
    bundleIcon,
    AddCircleFilled,
    AddCircleRegular,
} from "@fluentui/react-icons"
import { useListOperationFromParams } from "../shared.ts"

const AddIcon = bundleIcon(AddCircleFilled, AddCircleRegular)

const AddItemButton = () => {
    const { addStateListItem } = useListOperationFromParams()

    const addSomethingToDo = () => {
        const newItem = getEmptyItem()
        addStateListItem(newItem)
    }

    return (
        <div className={"button-container"}>
            <Button
                appearance="outline"
                icon={<AddIcon />}
                onClick={addSomethingToDo}
            >
                Add Item
            </Button>
        </div>
    )
}

export default AddItemButton
