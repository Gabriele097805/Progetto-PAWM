import ComponentList from "./ComponentList.tsx"
import AddItemButton from "./controls/AddItemButton.tsx"
import ClearButton from "./controls/ClearButton.tsx"
import UserComponent from "../shared/UserComponent.tsx"
import Title from "./Title.tsx"
import AppMessageViewer from "../shared/AppMessageViewer.tsx"

const List = () => {
    return (
        <>
            <UserComponent />
            <Title />
            <ComponentList />
            <AddItemButton />
            <ClearButton />
            <AppMessageViewer />
        </>
    )
}

export default List
