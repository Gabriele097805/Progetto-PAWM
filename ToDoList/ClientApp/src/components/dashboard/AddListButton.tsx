import { Button } from "@fluentui/react-components"
import {
    bundleIcon,
    AddCircleFilled,
    AddCircleRegular,
} from "@fluentui/react-icons"
import { useSetRecoilState } from "recoil"
import { addList } from "../../services/user-todo-lists-service.ts"
import { useTooltipMessage } from "../../state/app-message-atom.ts"
import { userLists } from "../../state/user-lists.ts"

const AddIcon = bundleIcon(AddCircleFilled, AddCircleRegular)

const AddListButton = () => {
    const { setTooltipMessage } = useTooltipMessage()
    const setLists = useSetRecoilState(userLists)

    const onAddList = () => {
        addList({
            name: "New List",
        })
            .then((list) => {
                setLists(list)
            })
            .catch((error) => {
                console.error(error)
                setTooltipMessage("Failed to add list.", "error")
            })
    }

    return (
        <div className={"button-container"}>
            <Button appearance="outline" icon={<AddIcon />} onClick={onAddList}>
                Add Item
            </Button>
        </div>
    )
}

export default AddListButton
