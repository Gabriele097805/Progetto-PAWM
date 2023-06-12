import { ChangeEvent, useState } from "react"
import { ListItem } from "../../../models/list-item.ts"
import { Checkbox, CheckboxOnChangeData } from "@fluentui/react-components"
import { useListOperationFromParams } from "../shared.ts"

type ListCheckboxProps = {
    readonly item: ListItem
}

const ItemSelectionCheckbox = ({ item }: ListCheckboxProps) => {
    const [checkboxState, setCheckboxState] = useState(item.checked)
    const { updateStateListItem } = useListOperationFromParams()

    const onCheckboxChanged = (
        _ev: ChangeEvent<HTMLInputElement>,
        { checked }: CheckboxOnChangeData
    ) => {
        const newItem: ListItem = {
            ...item,
            checked: checked === true,
        }

        setCheckboxState(newItem.checked)
        updateStateListItem(newItem)
    }

    return (
        <Checkbox
            checked={checkboxState}
            onChange={onCheckboxChanged}
            className={"checkbox"}
        />
    )
}

export default ItemSelectionCheckbox
