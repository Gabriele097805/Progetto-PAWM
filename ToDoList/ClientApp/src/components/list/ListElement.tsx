import DescriptionInput from "./controls/DescriptionInput.tsx"
import ItemSelectionCheckbox from "./controls/ItemSelectionCheckbox.tsx"
import { ListItem } from "../../models/list-item.ts"

type ListElementProps = {
    readonly item: ListItem
}

const ListElement = ({ item }: ListElementProps) => {
    return (
        <div className="flex flex-horizontal stack-element">
            <ItemSelectionCheckbox item={item} />
            <DescriptionInput item={item} />
        </div>
    )
}

export default ListElement
